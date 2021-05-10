using GubingTickets.Models.Event;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.ApiModels.Responses
{
    [DataContract]
    public class LoadRequestTicketsModel
    {
        [DataMember]
        public IEnumerable<TicketSalesUser> Users { get; set; }

        [DataMember]
        public EventDetail Event { get; set; }
    }
}
