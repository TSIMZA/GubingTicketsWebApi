using GubingTickets.DataAccessLayer.Implementations;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;
using GubingTickets.Models.ApiModels.Requests;
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

    public class TicketManagementController : BaseController
    {
        private readonly ITicketRequestsLayer _TicketRequestsLayer;

        public TicketManagementController() : base()
        {
            _TicketRequestsLayer = new TicketRequestsLayer(new TicketRequestsDataLayer(new FrameworkDbConnectionFactory(null)));
        }

        [HttpGet]
        [Route("LoadEventTicketsData")]
        public async Task<HttpResponseMessage> LoadEventTicketsData(int eventId)
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.LoadEventTicketsData(eventId), Request);
        }

        [HttpPost]
        [Route("PurchaseEventTickets")]
        public async Task<HttpResponseMessage> PurchaseEventTickets([FromBody][Required(ErrorMessage = "Invalid Request")]PurchaseTicketsRequest request)
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.PurchaseEventTickets(request), Request);
        }
    }
}