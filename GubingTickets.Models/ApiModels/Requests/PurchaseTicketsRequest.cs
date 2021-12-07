using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Requests
{
    [DataContract]
    public class PurchaseTicketsRequest: BaseUserRequest
    {
        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public int NumberOfTickets { get; set; }

        [DataMember]
        public int[] TicketLevels { get; set; }

    }
}
