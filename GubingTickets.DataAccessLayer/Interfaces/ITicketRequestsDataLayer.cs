using GubingTickets.Models.ApiModels;
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
    }
}
