using System.Collections.Generic;
using System.Threading.Tasks;
using GubingTickets.DataAccessLayer.Interfaces;
using GubingTickets.Models.ApiModels;
using GubingTickets.Models.Event;
using Dapper;
using GubingTickets.DataAccessLayer.Utils.Cache;
using System.Data;
using GubingTickets.DataAccessLayer.Utils.ConnectionFactory;

namespace GubingTickets.DataAccessLayer.Implementations
{
    public class TicketRequestsDataLayer : ITicketRequestsDataLayer
    {
        private readonly IDbConnectionFactory _DbConnectionFactory;
        private readonly ICachingLayer _CachingLayer;
        public TicketRequestsDataLayer(ICachingLayer cachingLayer, IDbConnectionFactory connectionFactory)
        {
            _DbConnectionFactory = connectionFactory;
            _CachingLayer = cachingLayer;
        }

        public async Task<EventDetail> GetEventDetails(int gubingEventId)
        {
            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EventDetail>("dbo.up_GetEventDetails", new { gubingEventId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TicketSalesUser>> GetTicketSalesUsers(int gubingEventId)
        {
            using (IDbConnection connection = _DbConnectionFactory.GetDbConnection())
            {
                return await connection.QueryAsync<TicketSalesUser>("dbo.up_GetTicketSalesUsers", new { gubingEventId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
