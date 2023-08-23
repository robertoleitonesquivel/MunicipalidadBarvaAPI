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
using MuniBarva.MODELS;
using MuniBarva.COMMON;

namespace MuniBarva.SERVICES
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDao _loginDAO;
        private readonly IConfiguration _config;
        private readonly IEncrypt _encrypt;
        private readonly ISendEmail _sendEmail;
        private readonly ISettingsService _settingsService;

        public LoginService
                (
                    ILoginDao loginDAO,
                    IConfiguration config,
                    IEncrypt encrypt,
                    ISendEmail sendEmail,
                    ISettingsService settingsService
                )
        {
            _loginDAO = loginDAO;
            _config = config;
            _encrypt = encrypt;
            _sendEmail = sendEmail;
            _settingsService = settingsService; 
        }

        public async Task<ApiResponse<string>> Send(RecoverPasswordDTO recoverPassword)
        {
            var token = await _encrypt.Sha256(Guid.NewGuid().ToString());

            await _loginDAO.SaveToken(token, recoverPassword.Email);

            var settings = await _settingsService.Get(Constants.RecoverPassword);

            string message = settings.Description.Replace("@Email", recoverPassword.Email);

            message = message.Replace("@Token", token);

            await _sendEmail.Send(recoverPassword.Email, "Recuperación de contraseña", message);

            return new ApiResponse<string>
            {
                Message = "Ha sido enviado un mensaje a su email con los pasos para restablecer la contaseña, por favor revise su email."
            };

        }

        public async Task<ApiResponse<EmployeesDTO>> SignIn(string _email, string _password)
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

                return new ApiResponse<EmployeesDTO> { Data = oEmployeesDTO };
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