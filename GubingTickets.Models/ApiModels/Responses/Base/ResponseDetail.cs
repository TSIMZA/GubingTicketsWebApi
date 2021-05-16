using GubingTickets.Models.ApiModels.Responses.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace GubingTickets.Models.ApiModels.Responses.Base
{
    [DataContract]
    public class ResponseDetail
    {
        [DataMember]
        public ResponseCode ResponseCode { get; set; }

        [DataMember]
        public string ResponseMessage { get; set; }
    }
}
