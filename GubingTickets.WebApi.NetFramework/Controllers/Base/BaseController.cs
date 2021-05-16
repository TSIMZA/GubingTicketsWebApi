using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;
using GubingTickets.WebApi.NetFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GubingTickets.WebApi.NetFramework.Controllers.Base
{
    public abstract class BaseController : ApiController
    {
        public BaseController()
        {

        }

        protected async Task<HttpResponseMessage> RequestHandler<T>(Func<Task<BaseResponse<T>>> func, HttpRequestMessage httpRequestMessage, string modelStateErrorMessage = "Invalid request.")
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        ResponseDetail = new ResponseDetail
                        {
                            ResponseCode = ResponseCode.InvalidModel,
                            ResponseMessage = modelStateErrorMessage
                        }
                    }
                    .GetHttpResponseMessage(httpRequestMessage, HttpStatusCode.BadRequest);
                }

                BaseResponse<T> response = await func();

                if (!response.Success)
                {
                    return response.GetHttpResponseMessage(httpRequestMessage, HttpStatusCode.BadRequest);
                }

                return response.GetHttpResponseMessage(httpRequestMessage, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Success = false,
                    ResponseDetail = new ResponseDetail
                    {
                        ResponseCode = ResponseCode.UnknownError,
                        ResponseMessage = $"Unexpected error {ex.GetType().Name}"
                    }
                }.GetHttpResponseMessage(httpRequestMessage, HttpStatusCode.BadRequest);
            }
        }
    }
}
