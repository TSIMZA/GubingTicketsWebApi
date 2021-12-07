using System;
using System.Runtime.Serialization;

namespace GubingTickets.Models.Gifts
{
    [DataContract]
    public class GiftRegItemReserve
    {
        [DataMember]
        public Guid itemId { get; set; }

        [DataMember]
        public string reservedById { get; set; }


    }
}
