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
    public class SettingsDao : ISettingsDao
    {
        private readonly MasterDao _masterDao;

        public SettingsDao(MasterDao masterDao)
        {
            _masterDao = masterDao;
        }

        public async Task<Settings> Get(string _codigo)
        {
            using (var connection = this._masterDao.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Settings>("MUNI_PA_SETTINGS", new
                {
                    Codigo = _codigo             
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Settings>> GetAll()
        {
            using (var connection = this._masterDao.GetConnection())
            {
                var result = await connection.QueryAsync<Settings>("MUNI_PA_SETTINGS", new
                {
                    Codigo = string.Empty
                }, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }
    }
}
