using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GubingTickets.Web.Api.Controllers.Base
{
    public abstract class BaseController: ControllerBase
    {
        public BaseController()
        {

        }

        protected async Task<IActionResult> RequestHandler<T>(Func<Task<BaseResponse<T>>> func, string modelStateErrorMessage = "Invalid request.")
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(new ResponseDetail
                    {
                        ResponseCode = ResponseCode.InvalidModel,
                        ResponseMessage = modelStateErrorMessage
                    });
                }

                BaseResponse<T> response = await func();

                if(!response.Success)
                {
                    return BadRequest(response.ResponseDetail);
                }

                return Ok(response);
            }
            catch(Exception ex)
            {
                return BadRequest(new ResponseDetail
                {
                    ResponseCode = ResponseCode.InvalidModel,
                    ResponseMessage = $"Unexpected error {ex.GetType().Name}"
                });
            }
        }
    }
}
