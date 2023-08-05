using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace MuniBarva.DAO
{
    public class MasterDao
    {
        private readonly string _connectionString;

        public MasterDao(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MuniBarvaDB");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}