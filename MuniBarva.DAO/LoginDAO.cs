using Dapper;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO
{
    public class LoginDAO : ILoginDAO
    {

        private readonly MasterDAO _masterDAO;

        public LoginDAO(MasterDAO masterDAO)
        {
            _masterDAO = masterDAO;
        }

        public async Task<Employees> SignIn(string _email, string _password)
        {
            using (var connection = this._masterDAO.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Employees>("dbo.MUNI_PA_SIGNIN", new
                {
                    Email = _email,
                    Password = _password
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
