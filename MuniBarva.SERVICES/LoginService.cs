using Mapster;
using MuniBarva.DAO.Interfaces;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using MuniBarva.COMMON.Interfaces;

namespace MuniBarva.SERVICES
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDao _loginDAO;
        private readonly IConfiguration _config;
        private readonly IEncrypt _encrypt;

        public LoginService(ILoginDao loginDAO, IConfiguration config, IEncrypt encrypt)
        {
            _loginDAO = loginDAO;
            _config = config;
            _encrypt = encrypt;
        }

        public async Task<EmployeesDTO> SignIn(string _email, string _password)
        {
            _password = await _encrypt.Sha256(_password);  

            var oEmployee = await this._loginDAO.SignIn(_email, _password);

            if (oEmployee is null)
            {
                return null;
            }
            else
            {
                var oEmployeesDTO = oEmployee.Adapt<EmployeesDTO>();

                oEmployeesDTO.Jwt = await GetJwT(oEmployeesDTO);

                return oEmployeesDTO;
            }
        }

        private async Task<string> GetJwT(EmployeesDTO _employeesDTO)
        {
            return await Task.Run(() =>
            {
                List<Claim> claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, _employeesDTO.Email));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:KEY").Value.ToString()));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return token;
            });
        }
    }
}