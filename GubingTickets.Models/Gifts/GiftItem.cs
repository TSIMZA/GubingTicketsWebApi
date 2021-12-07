using System;
using System.Runtime.Serialization;

namespace GubingTickets.Models.Gifts
{
    [DataContract]
    public class GiftItem : GiftRegItemReserve
    {
        [DataMember]
        public string itemImage { get; set; }

        [DataMember]
        public string itemName { get; set; }

        [DataMember]
        public decimal itemCost { get; set; }

        [DataMember]
        public string itemUrl { get; set; }

        [DataMember]
        public string itemStore { get; set; }

        [DataMember]
        public string itemDescription { get; set; }

        [DataMember]
        public bool isReserved { get; set; }

        [DataMember]
        public DateTime? reservedDateTime { get; set; }
    }
}
