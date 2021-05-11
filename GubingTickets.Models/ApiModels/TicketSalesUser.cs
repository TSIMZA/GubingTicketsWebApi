using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.ApiModels
{
    [DataContract]
    public class TicketSalesUser
    {
        [DataMember]
        public int SalesUserId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string UserName => $"{FirstName} {LastName}";
    }
}
