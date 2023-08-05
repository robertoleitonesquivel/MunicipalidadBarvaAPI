using Mapster;
using MuniBarva.COMMON.Interfaces;
using MuniBarva.DAO;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using MuniBarva.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.SERVICES
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesDao _employeesDao;
        private readonly IEncrypt _encrypt;

        public EmployeesService(IEmployeesDao employeesDao, IEncrypt encrypt)
        {
            _employeesDao = employeesDao;
            _encrypt = encrypt;
        }

        public async Task Add(PostEmployeesDTO _employeesDTO)
        {
            _employeesDTO.Password = await _encrypt.Sha256(_employeesDTO.Password);

            var oEmployee = _employeesDTO.Adapt<Employees>();

            await _employeesDao.Add(oEmployee);
        }

        public async Task Delete(int _id)
        {
            await _employeesDao.Delete(_id);
        }

        public async Task<List<GetEmployeesDTO>> GetAll(int _skip, int _take)
        {
            var oEmployees = await _employeesDao.GetAll(_skip, _take);

            return oEmployees.Adapt<List<GetEmployeesDTO>>();
        }

        public async Task<GetEmployeesDTO> GetById(int _id)
        {
            var oEmployee = await _employeesDao.GetById(_id);

            return oEmployee.Adapt<GetEmployeesDTO>();
        }

        public async Task Update(PatchEmployeesDTO _employeesDTO)
        {
            if (!string.IsNullOrEmpty(_employeesDTO.Password))
            {
                _employeesDTO.Password = await _encrypt.Sha256(_employeesDTO.Password);
            }

            var oEmployee = _employeesDTO.Adapt<Employees>();

            await _employeesDao.Update(oEmployee);
        }
    }
}
