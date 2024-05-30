using ApiTccManagementPersonal.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.IServices;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransacoesController : Controller
    {
        private readonly ITransacoesService _transacoesservice;
        public TransacoesController(ITransacoesService service)
        {
            _transacoesservice = service;
        }
        [HttpPost]
        public IActionResult RealizarTransacaoBancaria(TransicaoBancariaRequest request)
        {
            var result = _transacoesservice.RealizarTransacaoBancaria(request);
            if(result.status)
                return Ok(result.message);
            else
                return BadRequest(result.message);
        }

        [HttpGet]
        [Route("{codigoContaCorrente}")]
        public IActionResult BuscarHistoricoTransacoes([FromRoute] int codigoContaCorrente)
        {
            var result = _transacoesservice.BuscarHistoricoTransacoes(codigoContaCorrente);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
