using GubingTickets.DataAccessLayer.Interfaces;
using GubingTickets.Models.ApiModels;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;
using GubingTickets.Models.Event;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations.Base;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using GubingTickets.Web.ApplicationLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations
{
    public class TicketRequestsLayer : BaseApplicationLayer<TicketRequestsLayer>, ITicketRequestsLayer
    {
        private readonly ITicketRequestsDataLayer _TicketRequestsDataLayer;
        public TicketRequestsLayer(ITicketRequestsDataLayer ticketRequestsDataLayer) :base()
        {
            _TicketRequestsDataLayer = ticketRequestsDataLayer;
        }

        public Task<BaseResponse<LoadRequestTicketsModel>> LoadEventTicketsData(int eventId)
        {
            return RequestHandler(async () =>
            {
                try
                {
                    EventDetail eventDetails = await _TicketRequestsDataLayer.GetEventDetails(eventId);

                    if(eventDetails == null)
                        return ResponseCode.DataNotFound.GetFailureResponse<LoadRequestTicketsModel>($"Failed to load event data.");

                    IEnumerable<TicketSalesUser> ticketSalesUsers = await _TicketRequestsDataLayer.GetTicketSalesUsers(eventId);

                    if (ticketSalesUsers == null || !ticketSalesUsers.Any())
                        return ResponseCode.DataNotFound.GetFailureResponse<LoadRequestTicketsModel>($"{(ticketSalesUsers == null? "Failed to load ticket sales users.":"No available ticket sales users.")}");

                    LoadRequestTicketsModel response = new LoadRequestTicketsModel
                    {
                        Event = eventDetails,
                        Users = ticketSalesUsers
                    };

                    return response.GetSuccessResponse();
                }
                catch (Exception ex)
                {
                    return ResponseCode.UnknownError.GetFailureResponse<LoadRequestTicketsModel>($"Unexpected error on {nameof(LoadEventTicketsData)} - {ex.GetType().Name}");
                }
            });
        }

        public Task<BaseResponse<string>> RequestEventTickets(TicketsRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
