using GubingTickets.DataAccessLayer.Interfaces;
using GubingTickets.Models.ApiModels;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;
using GubingTickets.Models.Enums;
using GubingTickets.Models.Event;
using GubingTickets.Utilities.Barcode;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations.Base;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using GubingTickets.Web.ApplicationLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GubingTickets.Utilities.Extensions;
using System.IO;
using Newtonsoft.Json;
using GubingTickets.Models.Gifts;
using System.Data.SqlClient;

namespace GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations
{
    public class TicketRequestsLayer : BaseApplicationLayer<TicketRequestsLayer>, ITicketRequestsLayer
    {
        private readonly ITicketRequestsDataLayer _TicketRequestsDataLayer;
        public TicketRequestsLayer(ITicketRequestsDataLayer ticketRequestsDataLayer) : base()
        {
            _TicketRequestsDataLayer = ticketRequestsDataLayer;
        }


        public async Task<BaseResponse<IEnumerable<GiftItem>>> GetReservedGiftItems()
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    var remainingTicketDetails = await _TicketRequestsDataLayer.GetReservedGiftItems();

                    return remainingTicketDetails.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<IEnumerable<GiftItem>>($"Error when loading");
                }
            });
        }

        public async Task<BaseResponse<IEnumerable<GiftItem>>> ReserveGiftItem(GiftRegItemReserve giftRegItemReserve)
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    var remainingTicketDetails = await _TicketRequestsDataLayer.ReserveGiftItem(giftRegItemReserve);

                    return remainingTicketDetails.GetSuccessResponse();
                }
                catch (SqlException ex) when (ex.Number == 2627)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<IEnumerable<GiftItem>>($"Gift already reserved");
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<IEnumerable<GiftItem>>($"Error when reserving gift");
                }
            });
        }

        public async Task<BaseResponse<LoadRequestTicketsModel>> LoadEventTicketsData(int eventId)
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    EventDetail eventDetails = await _TicketRequestsDataLayer.GetEventDetails(eventId);

                    if (eventDetails == null)
                        return ResponseCode.DataNotFound.GetFailureResponse<LoadRequestTicketsModel>($"Failed to load event data.");

                    IEnumerable<TicketSalesUser> ticketSalesUsers = await _TicketRequestsDataLayer.GetTicketSalesUsers(eventId);

                    if (ticketSalesUsers == null || !ticketSalesUsers.Any())
                        return ResponseCode.DataNotFound.GetFailureResponse<LoadRequestTicketsModel>($"{(ticketSalesUsers == null ? "Failed to load ticket sales users." : "No available ticket sales users.")}");

                    IEnumerable<EventTicketLevel> ticketLevels = await _TicketRequestsDataLayer.GetEventTicketLevels(eventId);

                    if (ticketLevels == null || !ticketLevels.Any())
                        return ResponseCode.DataNotFound.GetFailureResponse<LoadRequestTicketsModel>($"{(ticketLevels == null ? "Failed to load ticket levels ." : "No available ticket levels.")}");

                    LoadRequestTicketsModel response = new LoadRequestTicketsModel
                    {
                        Event = eventDetails,
                        Users = ticketSalesUsers,
                        TicketLevels = ticketLevels
                    };

                    return response.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<LoadRequestTicketsModel>($"Unexpected error on {nameof(LoadEventTicketsData)} - {ex.GetType().Name}");
                }
            });
        }

        public async Task<BaseResponse<RemaingTicketsResponse>> GetRemainingEventTickets(int eventId)
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    var remainingTicketDetails = await _TicketRequestsDataLayer.GetRemainingTickets(eventId);

                    if (remainingTicketDetails == null)
                        return ResponseCode.DataNotFound.GetFailureResponse<RemaingTicketsResponse>($"Failed to load event tickets data.");

                    return remainingTicketDetails.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<RemaingTicketsResponse>($"Unexpected error on {nameof(GetRemainingEventTickets)} - {ex.GetType().Name}");
                }
            });
        }

        public async Task<BaseResponse<ValidCodeResponse>> ValidateTicket(ValidateTicketRequest request)
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    TicketSalesUser salesUser = await _TicketRequestsDataLayer.GetTicketSalesUserById(request.UserId);

                    if (salesUser == null)
                        return ResponseCode.SalesUserNotFound.GetFailureResponse<ValidCodeResponse>($"Invalid User or Password.");

                    if (!salesUser.Password.Equals(request.Password))
                        return ResponseCode.InvalidPassword.GetFailureResponse<ValidCodeResponse>($"Invalid User or Password.");


                    ValidCodeResponse validCodeResponse = await _TicketRequestsDataLayer.ValidateTicket(request.EventId, request.TicketCode, request.UserId);

                    if (validCodeResponse == null)
                        return ResponseCode.TicketCodeDoesNotExist.GetFailureResponse<ValidCodeResponse>($"Failed to check.");

                    if(validCodeResponse.IsAlreadyScanned)
                        return ResponseCode.TicketValidationFailed.GetFailureResponse<ValidCodeResponse>($"Ticket ref {(request.TicketCode)} already used.");

                    return validCodeResponse.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<ValidCodeResponse>($"Unexpected error on {nameof(ValidateTicket)} - {ex.GetType().Name}");
                }
            });
        }

        public async Task<BaseResponse<string>> OverrideTicketSales(int eventId, int tables, int tickets, bool delete)
        {
            return await RequestHandler(async () =>
            {
                try
                {
                    await _TicketRequestsDataLayer.OverrideTicketSales(eventId, tables, tickets, delete);

                    return "Success".GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<string>($"Unexpected error when updating. - {ex.GetType().Name}");
                }
            });
        }

        public async Task<BaseResponse<PurchaseTicketsResponse>> PurchaseEventTickets(PurchaseTicketsRequest request)
        {
            return await RequestHandler(async () =>
            {
                try
                {

                    TicketSalesUser salesUser = await _TicketRequestsDataLayer.GetTicketSalesUserById(request.UserId);

                    if (salesUser == null)
                        return ResponseCode.SalesUserNotFound.GetFailureResponse<PurchaseTicketsResponse>($"Invalid User or Password.");

                    if (!salesUser.Password.Equals(request.Password))
                        return ResponseCode.InvalidPassword.GetFailureResponse<PurchaseTicketsResponse>($"Invalid User or Password.");

                    EventDetail eventDetail = await _TicketRequestsDataLayer.GetEventDetails(request.EventId);

                    if (eventDetail == null)
                        return ResponseCode.DataNotFound.GetFailureResponse<PurchaseTicketsResponse>($"Failed to find event.");

                    IEnumerable<EventTicketLevel> ticketLevels = await _TicketRequestsDataLayer.GetEventTicketLevels(request.EventId);

                    if (ticketLevels == null || !ticketLevels.Any())
                        return ResponseCode.DataNotFound.GetFailureResponse<PurchaseTicketsResponse>($"{(ticketLevels == null ? "Failed to load ticket levels ." : "No available ticket levels.")}");

                    Patron patron = new Patron
                    {
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        EmailAddress = request.EmailAddress,
                        MobileNumber = request.MobileNumber
                    };

                    IGubingBarcode barcode = new GubingBarcode();

                    List<TicketDetail> ticketDetails = new List<TicketDetail>();
                    
                    foreach (int level in request.TicketLevels)
                    {

                        BarcodeImage barcodeImage = barcode.Encode($"G{eventDetail.EventIdString} {6.RandomString(false)}",
                            new BarcodeEncodingOptions
                            {
                                BarcodeType = BarcodeType.QRCode,
                                ImageFormat = ImageFormat.Png,
                                Height = 125,
                                Width = 125,
                                Margin = 0,
                                ErrorCorrection = ErrorCorrection.H
                            });

                        byte[] barcodeBytes = Convert.FromBase64String(barcodeImage.ImageData);

                        var ticketLevel = ticketLevels.First(tl => tl.TicketLevelId == level);

                        ticketDetails.Add(new TicketDetail
                        {
                            EventTicketLevelId = level,
                            TicketLevel = ticketLevel.Level,
                            Barcode = barcodeBytes,
                            TicketPrice = ticketLevel.Price,
                            BarcodeContent = barcodeImage.BarcodeContent
                        });
                    }

                    string eventTicketReference = 10.RandomString(true);

                    Guid ticketId = await _TicketRequestsDataLayer.PurhcaseEventTickets(request, request.TicketLevels.Select(tl => new TicketLevelDto
                    {
                        EventTicketLevelId = tl,
                        EventTicketCode = 6.RandomString(false)
                    }), eventTicketReference);

                    string fileName = ticketId.ToString().Replace('-', '_');
                    TicketFileHelper.GenerateTickets(eventTicketReference, fileName, GetGubingTickets(), ticketDetails, eventDetail, patron);

                    PurchaseTicketsResponse response = new PurchaseTicketsResponse
                    {
                        TicketReference = eventTicketReference,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        NumberOfTickets = request.NumberOfTickets,
                        TicketsFileName = $"{fileName}.pdf",
                        EventId = eventDetail.EventId,
                        EventName = eventDetail.EventName
                    };

                    return response.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<PurchaseTicketsResponse>($"Unexpected error.");
                }
            });
        }
    
        public static byte[] GetGubingTickets()
        {
            byte[] bytes = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}\\GB.png");

            return bytes;
        }

       
    }
}
