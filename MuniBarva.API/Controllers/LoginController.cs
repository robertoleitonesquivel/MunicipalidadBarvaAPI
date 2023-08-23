using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS;
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
        public async Task<ActionResult<ApiResponse<EmployeesDTO>>> SignIn(string email, string password)
        {
            try
            {
                var response = await this._loginService.SignIn(email, password);

                if (response is null)
                {
                    return NotFound("Credenciales incorrectos.");
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Send(RecoverPasswordDTO recoverPassword)
        {
            try
            {
                var response = await this._loginService.Send(recoverPassword);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
