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
    public class LoginDao : ILoginDao
    {

        private readonly MasterDao _masterDao;

        public LoginDao(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<Employees> SignIn(string _email, string _password)
        {
            using (var connection = this._masterDao.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Employees>("MUNI_PA_SIGNIN", new
                {
                    Email = _email,
                    Password = _password
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
