using GubingTickets.Models.ApiModels;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.Event;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GubingTickets.DataAccessLayer.Interfaces
{
    public interface ITicketRequestsDataLayer
    {
        Task<IEnumerable<TicketSalesUser>> GetTicketSalesUsers(int eventId);

        Task<EventDetail> GetEventDetails(int eventId);
        Task<TicketSalesUser> GetTicketSalesUserById(Guid ticketSalesUserId);

        Task<RemaingTicketsResponse> GetRemainingTickets(int eventDetailId);

        Task OverrideTicketSales(int eventDetailId, int tables, int tickets, bool delete);

        Task<IEnumerable<EventTicketLevel>> GetEventTicketLevels(int eventDetailId);

        Task<Guid> PurhcaseEventTickets(PurchaseTicketsRequest request, IEnumerable<TicketLevelDto> ticketLevels, string eventTicketReference);
    }
}
