using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS.DTO;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeesDTO>> SignIn(string email, string password)
        {
            try
            {
                var oEmployee = await this._loginService.SignIn(email, password);

                if (oEmployee is null)
                {
                    return NotFound("Credenciales incorrectos.");
                }
                else
                {
                    return Ok(oEmployee);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
