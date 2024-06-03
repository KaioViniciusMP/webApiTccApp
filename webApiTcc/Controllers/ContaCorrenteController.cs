using ApiTccManagementPersonal.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IContaCorrenteService _contacorrenteservice;
        public ContaCorrenteController(IContaCorrenteService service)
        {
            _contacorrenteservice = service;
        }

        [HttpPost]
        public IActionResult CriarContaCorrente([FromBody] ContaCorrenteRequest request)
        {
            var result = _contacorrenteservice.CriarContaCorrente(request);
            if (result.status)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }

        [HttpGet]
        public IActionResult BuscarContasCorrentesExistentes()
        {
            var result = _contacorrenteservice.BuscarContasCorrentesExistentes();
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
