using GubingTickets.Models.Event;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.ApiModels.Requests
{
    [DataContract]
    public class TicketsRequest
    {
        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string lastName { get; set; }

        [DataMember]
        public string mobileNumber { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public int NumberOfTickets { get; set; }

        [DataMember]
        public IEnumerable<TicketDetail> TicketDetails { get; set; }

    }
}
