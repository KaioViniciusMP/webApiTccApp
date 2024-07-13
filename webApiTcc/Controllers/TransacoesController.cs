using ApiTccManagementPersonal.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;
using webApiTcc.Application.Services;

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
                return Ok(result);
            else
                return BadRequest(result.message);
        }

        [HttpGet]
        [Route("{codigoContaCorrente}")]
        public IActionResult BuscarHistoricoTransacoes([FromRoute] int codigoContaCorrente)
        {
            var result = _transacoesservice.BuscarHistoricoTransacoes(codigoContaCorrente);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPost("DepositoExtra")]
        public IActionResult DepositoExtra([FromBody] EntradaFinanceiraExtraRequest request)
        {
            var result = _transacoesservice.DepositoExtra(request);
            if (result.status)
                return Ok(result);
            else
                return BadRequest(result.status);
        }

        [HttpPost("DetalhesTransacao")]
        public IActionResult DetalhesTransacaoFeita([FromBody] DetalhesTransacaoFeitaRequest request)
        {
            var result = _transacoesservice.DetalhesTransacaoFeita(request);
            if (result == null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("BuscarModalidades")]
        public IActionResult BuscarModalidades()
        {
            var result = _transacoesservice.BuscarModalidades();
            if(result == null)
                return NotFound();
            else 
                return Ok(result);
        }

        [HttpPost("BuscarHistoricoTransacoesPorModalidade")]
        public IActionResult BuscarHistoricoTransacoesPorModalidade([FromBody] HistoricoTransacoesPorModalidadeRequest request)
        {
            var result = _transacoesservice.BuscarHistoricoTransacoesPorModalidade(request);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }
    }
}

