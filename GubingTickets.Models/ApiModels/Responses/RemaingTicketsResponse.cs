using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Responses
{
    [DataContract]
    public class RemaingTicketsResponse
    {
        [DataMember]
        public int RemainingTickets { get; set; }


        [DataMember]
        public int RemainingTables { get; set; }
    }
}
