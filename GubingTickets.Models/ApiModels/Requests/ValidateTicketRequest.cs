
using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Requests
{
    [DataContract]
    public class ValidateTicketRequest: BaseUserRequest
    {
        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string TicketCode { get; set; }
    }
}
