using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.Gifts;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces
{
    public interface ITicketRequestsLayer
    {
        Task<BaseResponse<IEnumerable<GiftItem>>> ReserveGiftItem(GiftRegItemReserve giftRegItemReserve);
        Task<BaseResponse<IEnumerable<GiftItem>>> GetReservedGiftItems();
        Task<BaseResponse<LoadRequestTicketsModel>> LoadEventTicketsData(int eventId);
        Task<BaseResponse<RemaingTicketsResponse>> GetRemainingEventTickets(int eventId);
        Task<BaseResponse<ValidCodeResponse>> ValidateTicket(ValidateTicketRequest request);
        Task<BaseResponse<string>> OverrideTicketSales(int eventId, int tables, int tickets, bool delete);
        Task<BaseResponse<PurchaseTicketsResponse>> PurchaseEventTickets(PurchaseTicketsRequest request);
    }
}
