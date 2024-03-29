﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.Web.Api.Controllers.Base;
using GubingTickets.Web.ApplicationLayer.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GubingTickets.Web.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TicketManagementController : BaseController
    {

        private readonly ITicketRequestsLayer _TicketRequestsLayer;

        public TicketManagementController(ITicketRequestsLayer ticketRequestsLayer):base()
        {
            _TicketRequestsLayer = ticketRequestsLayer;
        }

        [HttpGet]
        [Route("LoadEventTicketsData")]
        public async Task<IActionResult> LoadEventTicketsData(int eventId)
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.LoadEventTicketsData(eventId));
        }

        [HttpPost]
        [Route("PurchaseEventTickets")]
        public async Task<IActionResult> PurchaseEventTickets([FromBody][Required(ErrorMessage ="Invalid Request")]PurchaseTicketsRequest request)
        {
            return await RequestHandler(async () => await _TicketRequestsLayer.PurchaseEventTickets(request));
        }
    }
}
