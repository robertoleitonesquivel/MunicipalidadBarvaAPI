using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.SERVICES
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDAO _loginDAO;

        public LoginService(ILoginDAO loginDAO)
        {
            _loginDAO = loginDAO;
        }

        public async Task<EmployeesDTO> SignIn(string _email, string _password)
        {
            var oEmployee = await this._loginDAO.SignIn(_email, _password);

            if (oEmployee is null)
            {
                return null;
            }
            else
            {
                return new EmployeesDTO
                {
                    Id = oEmployee.Id,
                    Name = oEmployee.Name,
                    Email = oEmployee.Email,
                    Jwt = string.Empty
                };
            }
        }
    }
}