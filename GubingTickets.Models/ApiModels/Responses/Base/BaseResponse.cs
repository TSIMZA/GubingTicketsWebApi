using GubingTickets.Models.ApiModels.Responses.Enums;
using System.Runtime.Serialization;

namespace GubingTickets.Models.ApiModels.Responses.Base
{
    [DataContract]
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = false;

            ResponseDetail = new ResponseDetail
            {
                ResponseCode = ResponseCode.UnknownError,
                ResponseMessage = string.Empty
            };
        }

        [DataMember]
        public ResponseDetail ResponseDetail { get; set; }

        [DataMember]
        public bool Success { get; set; }
    }

    [DataContract]
    public class BaseResponse<T>:BaseResponse
    {
        public BaseResponse()
        {
            Data = default;
        }

        [DataMember]
        public T Data { get; set; }
    }
}
