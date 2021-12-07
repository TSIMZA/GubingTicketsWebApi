using GubingTickets.DataAccessLayer.Implementations;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Models.Gifts;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Implementations;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using GubingTickets.WebApi.NetFramework.Controllers.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GubingTickets.WebApi.NetFramework.Controllers
{

    public class WeddingGiftController : BaseController
    {
        private readonly ITicketRequestsLayer _TicketRequestsLayer;

        public WeddingGiftController() : base()
        {
            _TicketRequestsLayer = new TicketRequestsLayer(new TicketRequestsDataLayer(new FrameworkDbConnectionFactory(null)));
        }

        [HttpPost]
        [Route("ReserveGiftItem")]
        public async Task<HttpResponseMessage> ReserveGiftItem([FromBody][Required(ErrorMessage = "Invalid Request")]   GiftRegItemReserve request)
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.ReserveGiftItem(request), Request);
        }


        [HttpGet]
        [Route("GetAllGiftItems")]
        public async Task<HttpResponseMessage> GetAllGiftItems()
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.GetReservedGiftItems(), Request);
        }
    }
}