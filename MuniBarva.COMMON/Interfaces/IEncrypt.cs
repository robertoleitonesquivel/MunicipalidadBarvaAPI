using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.COMMON.Interfaces
{
    public interface IEncrypt
    {
        Task<string> Sha256(string text);
    }
}
