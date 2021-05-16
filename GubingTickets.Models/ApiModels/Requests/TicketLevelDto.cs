using GubingTickets.Models.Attributes;

namespace GubingTickets.Models.ApiModels.Requests
{
    public class TicketLevelDto
    {
        [TVP(1)]
        public int EventTicketLevelId { get; set; }

        [TVP(2)]
        public string EventTicketCode { get; set; }
    }
}
