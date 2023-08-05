
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO.Interfaces
{
    public interface IEmployeesDao
    {
        Task Add(Employees _employees);
        Task Update(Employees _employees);
        Task Delete(int _id);
        Task<Employees> GetById(int _id);
        Task<List<Employees>> GetAll(int _skip, int _take); 


    }
}
