using Dapper;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MuniBarva.DAO
{
    public class EmployeesDao : IEmployeesDao
    {
        private readonly MasterDao _masterDao;

        public EmployeesDao(MasterDao masterDao)
        {
            this._masterDao = masterDao;
        }


        public async Task Add(Employees _employees)
        {
            using (var connection = _masterDao.GetConnection())
            {
                await connection.ExecuteAsync("MUNI_PA_ADD_EMPLOYEE", new
                {
                    Name = _employees.Name,
                    Email = _employees.Email,
                    Password = _employees.Password
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Delete(int _id)
        {
            using (var connection = _masterDao.GetConnection())
            {
                await connection.ExecuteAsync("MUNI_PA_DELETE_EMPLOYEE", new { Id = _id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<Employees>> GetAll(int _skip, int _take)
        {
            using (var connection = _masterDao.GetConnection())
            {
                var employees = await connection.QueryAsync<Employees>("MUNI_PA_GETALL_EMPLOYEES",
                    new { Skip = _skip, Take = _take },
                    commandType: CommandType.StoredProcedure);

                return employees.ToList();
            }
        }

        public async Task<Employees> GetById(int _id)
        {
            using (var connection = _masterDao.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Employees>("MUNI_PA_GETBYID_EMPLOYEE", new { Id = _id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Employees _employees)
        {
            using (var connection = _masterDao.GetConnection())
            {
                await connection.ExecuteAsync("MUNI_PA_UPDATE_EMPLOYEE", new
                {
                    Id = _employees.Id,
                    Name = _employees.Name,
                    Email = _employees.Email,
                    Password = _employees.Password
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
