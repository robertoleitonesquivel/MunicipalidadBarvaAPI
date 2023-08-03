using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace MuniBarva.DAO
{
    public class MasterDAO
    {
        private readonly string _connectionString;

        public MasterDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MuniBarvaDB");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}