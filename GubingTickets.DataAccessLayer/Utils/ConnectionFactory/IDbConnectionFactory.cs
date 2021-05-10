using System.Data;

namespace GubingTickets.DataAccessLayer.Utils.ConnectionFactory
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetDbConnection();

        IDbConnection GetDbConnection(string connectionName);
    }
}
