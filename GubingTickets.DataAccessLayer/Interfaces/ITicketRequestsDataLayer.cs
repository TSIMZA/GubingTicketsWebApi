using GubingTickets.Models.ApiModels;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.ApiModels.Responses;
using GubingTickets.Models.Event;
using GubingTickets.Models.Gifts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GubingTickets.DataAccessLayer.Interfaces
{
    public interface ITicketRequestsDataLayer
    {
        Task<IEnumerable<GiftItem>> GetReservedGiftItems();
        Task<IEnumerable<GiftItem>> ReserveGiftItem(GiftRegItemReserve giftRegItemReserve);
        Task<IEnumerable<TicketSalesUser>> GetTicketSalesUsers(int eventId);

        Task<EventDetail> GetEventDetails(int eventId);
        Task<TicketSalesUser> GetTicketSalesUserById(Guid ticketSalesUserId);

        Task<RemaingTicketsResponse> GetRemainingTickets(int eventDetailId);

        Task<ValidCodeResponse> ValidateTicket(int eventDetailId, string ticketCode, Guid userId);

        Task OverrideTicketSales(int eventDetailId, int tables, int tickets, bool delete);

        Task<IEnumerable<EventTicketLevel>> GetEventTicketLevels(int eventDetailId);

        Task<Guid> PurhcaseEventTickets(PurchaseTicketsRequest request, IEnumerable<TicketLevelDto> ticketLevels, string eventTicketReference);
    }
}
