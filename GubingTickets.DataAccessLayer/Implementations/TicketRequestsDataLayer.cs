using System.Collections.Generic;
using System.Threading.Tasks;
using GubingTickets.DataAccessLayer.Interfaces;
using GubingTickets.Models.ApiModels;
using GubingTickets.Models.Event;
using Dapper;
using GubingTickets.DataAccessLayer.Utils.Cache;
using System.Data;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;
using GubingTickets.Models.ApiModels.Requests;
using GubingTickets.DataAccessLayer.Extensions;
using System;

namespace GubingTickets.DataAccessLayer.Implementations
{
    public class TicketRequestsDataLayer : ITicketRequestsDataLayer
    {
        private readonly IDbConnectionFactory _DbConnectionFactory;
        private readonly ICachingLayer _CachingLayer;
        public TicketRequestsDataLayer(IDbConnectionFactory connectionFactory)
        {
            _DbConnectionFactory = connectionFactory;
        }

        public async Task<EventDetail> GetEventDetails(int eventDetailId)
        {
            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EventDetail>("dbo.up_GetEventDetails", new { eventDetailId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TicketSalesUser>> GetTicketSalesUsers(int eventDetailId)
        {
            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryAsync<TicketSalesUser>("dbo.up_GetTicketSalesUsers", new { eventDetailId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<EventTicketLevel>> GetEventTicketLevels(int eventDetailId)
        {
            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryAsync<EventTicketLevel>("dbo.up_GetEventTicketLevels", new { eventDetailId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Guid> PurhcaseEventTickets(PurchaseTicketsRequest request, IEnumerable<TicketLevelDto> ticketLevels, string eventTicketReference)
        {
            DynamicParameters parameters = ticketLevels.GetDynamicParameters("RequestedTickets", "TVP_RequestTicket");
            parameters.Add("@EventDetailId", request.EventId);
            parameters.Add("@FirstName", request.FirstName);
            parameters.Add("@LastName", request.LastName);
            parameters.Add("@MobileNumber", request.MobileNumber);
            parameters.Add("@EmailAddress", request.EmailAddress);
            parameters.Add("@NoOfTickets", request.NumberOfTickets);
            parameters.Add("@EventTicketReference", eventTicketReference);
            parameters.Add("@SalesUserId", request.UserId);

            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryFirstAsync<Guid>("dbo.up_PurchaseTickets", parameters , commandType: CommandType.StoredProcedure);
            }
        }
    }
}
