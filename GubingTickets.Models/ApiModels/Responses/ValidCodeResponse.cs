using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Responses
{
    [DataContract]
    public class ValidCodeResponse
    {
        [DataMember]
        public bool IsAlreadyScanned { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string TicketLevel { get; set; }
    }
}
