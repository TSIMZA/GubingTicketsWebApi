using System.Runtime.Serialization;

namespace GubingTickets.Models.Event
{
    [DataContract]
    public class TicketDetail
    {
        [DataMember]
        public byte[] Barcode { get; set; }

        [DataMember]
        public int EventTicketLevelId { get; set; }

        [DataMember]
        public string TicketLevel { get; set; }

        [DataMember]
        public decimal TicketPrice { get; set; }

        [DataMember]
        public string BarcodeContent { get; set; }
    }
}
