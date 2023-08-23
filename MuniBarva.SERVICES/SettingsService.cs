using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.Models;
using MuniBarva.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsDao _settingsDao;
        public SettingsService(ISettingsDao settingsDao)
        {
            this._settingsDao = settingsDao;    
        }

        public async Task<Settings> Get(string _codigo)
        {
            return await _settingsDao.Get(_codigo);
        }

        public async Task<List<Settings>> GetAll()
        {
            return await _settingsDao.GetAll();
        }
    }
}
