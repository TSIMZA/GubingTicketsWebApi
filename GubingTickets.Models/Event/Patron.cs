using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.Event
{
    [DataContract]
    public class Patron
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MobileNumber { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }
    }
}
