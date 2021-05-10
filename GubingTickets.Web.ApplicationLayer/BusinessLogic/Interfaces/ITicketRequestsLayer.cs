using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.ApiModels.Responses.Base;
using System.Threading.Tasks;

namespace GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces
{
    public interface ITicketRequestsLayer
    {
        Task<BaseResponse<LoadRequestTicketsModel>> LoadEventTicketsData(int eventId);
        Task<BaseResponse<string>> RequestEventTickets(TicketsRequest request);
    }
}
