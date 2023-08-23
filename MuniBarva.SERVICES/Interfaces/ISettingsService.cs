using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface ISettingsService
    {
        Task<Settings> Get(string _codigo);
        Task<List<Settings>> GetAll();
    }
}
