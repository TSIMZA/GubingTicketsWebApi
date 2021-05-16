using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Responses
{
    [DataContract]
    public class PurchaseTicketsResponse
    {
        [DataMember,JsonProperty("ticketReference")]
        public string TicketReference { get; set; }

        [DataMember, JsonProperty("ticketsFileName")]
        public string TicketsFileName { get; set; }

        [DataMember, JsonProperty("firstName")]
        public string FirstName { get; set; }

        [DataMember, JsonProperty("lastName")]
        public string LastName { get; set; }

        [DataMember, JsonProperty("numberOfTickets")]
        public int NumberOfTickets { get; set; }

        [DataMember, JsonProperty("eventName")]
        public string EventName { get; set; }

        [DataMember, JsonProperty("eventId")]
        public int EventId { get; set; }
    }
}
