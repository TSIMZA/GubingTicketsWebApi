using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.Event
{
    [DataContract]
    public class EventTicketLevel
    {
        [DataMember]
        public int TicketLevelId { get; set; }

        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int TotalTickets { get; set; }

        [DataMember]
        public int SoldTickets { get; set; }

        [DataMember]
        public DateTime? SalesActiveFromDateTime { get; set; }

        public DateTime? SalesActiveToDateTime { get; set; }
    }
}
