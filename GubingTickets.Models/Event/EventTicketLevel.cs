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

        public string Level { get; set; }

        public decimal Price { get; set; }

        public int TotalTickets { get; set; }

        public int SoldTickets { get; set; }

        public DateTime? SalesActiveFromDateTime { get; set; }

        public DateTime? SalesActiveToDateTime { get; set; }
    }
}
