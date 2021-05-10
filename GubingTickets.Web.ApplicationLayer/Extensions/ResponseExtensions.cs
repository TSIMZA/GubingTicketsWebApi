using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;

namespace GubingTickets.Web.ApplicationLayer.Extensions
{
    public static class ResponseExtensions
    {
        public static BaseResponse<T> GetSuccessResponse<T>(this T obj, string responseMessage = null)
        {
            return new BaseResponse<T>
            {
                Data = obj,
                Success = true,
                ResponseDetail = new ResponseDetail
                {
                    ResponseCode = ResponseCode.Success,
                    ResonseMessage = string.IsNullOrWhiteSpace(responseMessage) ? "Success" : responseMessage
                }
            };
        }


        public static BaseResponse<T> GetFailureResponse<T>(this ResponseCode responseCode, string responseMessage = null)
        {
            return new BaseResponse<T>
            {
                Data = default,
                Success = false,
                ResponseDetail = new ResponseDetail
                {
                    ResponseCode = responseCode,
                    ResonseMessage = string.IsNullOrWhiteSpace(responseMessage) ? "Unknown error." : responseMessage
                }
            };
        }
    }
}
