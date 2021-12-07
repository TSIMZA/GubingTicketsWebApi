using System;
using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Requests
{
    [DataContract]
    public class BaseUserRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
