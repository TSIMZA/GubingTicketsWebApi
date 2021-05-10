using GubingTickets.Models.ApiModels.Responses.Base;
using GubingTickets.Models.ApiModels.Responses.Enums;
using GubingTickets.Web.ApplicationLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations.Base
{
    public class BaseApplicationLayer<TType> where TType:class
    {
        public BaseApplicationLayer()
        {

        }

        protected async Task<BaseResponse<T>> RequestHandler<T>(Func<Task<BaseResponse<T>>> func)
        {
            try
            {
                return await Task.Run(func);
            }
            catch (Exception ex)
            {
                return ResponseCode.UnknownError.GetFailureResponse<T>($"Unexpected error on {typeof(TType).Name} - {ex.GetType().Name}");
            }
        }
    }
}
