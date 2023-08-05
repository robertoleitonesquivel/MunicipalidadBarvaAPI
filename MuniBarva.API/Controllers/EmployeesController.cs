using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuniBarva.MODELS.DTO;
using MuniBarva.MODELS.Models;
using MuniBarva.SERVICES.Interfaces;

namespace MuniBarva.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<List<EmployeesDTO>>> GetAll(int skip, int take)
        {
            try
            {
                var employees = await _employeesService.GetAll(skip, take);

                if (employees is not null)
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No se encontraron resultados.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<EmployeesDTO>> GetById(int id)
        {
            try
            {
                var employees = await _employeesService.GetById(id);

                if (employees is not null)
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No se encontraron resultados.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> Add(PostEmployeesDTO employees)
        {
            try
            {
                await _employeesService.Add(employees);

                return Ok("Guardado con éxito.");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("Update")]
        public async Task<ActionResult<string>> Update(PatchEmployeesDTO employees)
        {
            try
            {
                await _employeesService.Update(employees);

                return Ok("Actualizado con éxito.");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                await _employeesService.Delete(id);


                return Ok("Eliminado con éxito.");

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
