using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.Event
{
    [DataContract]
    public class EventDetail
    {
        [DataMember]
        public int EventId { get; set; }

        [DataMember]
        public string EventName { get; set; }

        [DataMember]
        public string EventLocation { get; set; }

        [DataMember]
        public DateTime EventStartDateTime { get; set; }

        [DataMember]
        public DateTime? EventEndDateTime { get; set; }

        [DataMember]
        public string EventLogo { get; set; }

        [DataMember]
        public IEnumerable<string> TicketLevels { get; set; }

        [DataMember]
        public byte[] EventLogoBytes { get; set; }

        public string EventIdString => EventId.ToString().PadLeft(6, '0');
    }
}
