using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES.Interfaces
{
    public interface IEmployeesService
    {
        Task Add(PostEmployeesDTO _employeesDTO);
        Task Update(PatchEmployeesDTO _employeesDTO);
        Task Delete(int _id);
        Task<GetEmployeesDTO> GetById(int _id);
        Task<List<GetEmployeesDTO>> GetAll(int _skip, int _take);
    }
}
