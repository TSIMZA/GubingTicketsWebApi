using GubingTickets.Models.Event;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.ApiModels.Requests
{
    [DataContract]
    public class PurchaseTicketsRequest
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
        public Guid UserId { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int[] TicketLevels { get; set; }

    }
}
