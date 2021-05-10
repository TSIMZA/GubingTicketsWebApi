using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace GubingTickets.DataAccessLayer.Utils.ConnectionFactory
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _Default = "Default";

        private readonly IConfiguration _Configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IDbConnection GetDbConnection()
        {
            return GetDbConnection(_Default);
        }

        public IDbConnection GetDbConnection(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
                throw new ArgumentNullException(nameof(connectionName));

            return new SqlConnection(_Configuration[$"ConnectionStrings:{connectionName}"]);
        }
    }
}
