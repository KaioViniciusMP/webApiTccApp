using ApiTccManagementPersonal.Application.DTO.Request;
using Azure.Core;
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
        private readonly IAutenticacoesServices _autenticacoesServices;

        public TransacoesController(ITransacoesService service, IAutenticacoesServices serviceAutenticacao)
        {
            _transacoesservice = service;
            _autenticacoesServices = serviceAutenticacao;
        }

        [HttpPost]
        public IActionResult RealizarTransacaoBancaria(TransicaoBancariaRequest request)
        {
            try
            {
                var result = _transacoesservice.RealizarTransacaoBancaria(request);
                if (result.status)
                    return Ok(result);
                else
                    return BadRequest(result.message);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: RealizarTransacaoBancaria - ContacorrenteCodigo: {request.contaCorrenteCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{codigoContaCorrente}")]
        public IActionResult BuscarHistoricoTransacoes([FromRoute] int codigoContaCorrente)
        {
            try
            {
                var result = _transacoesservice.BuscarHistoricoTransacoes(codigoContaCorrente);
                if (result != null && result.Count > 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: BuscarHistoricoTransacoes"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpPost("DepositoExtra")]
        public IActionResult DepositoExtra([FromBody] EntradaFinanceiraExtraRequest request)
        {
            try
            {
                var result = _transacoesservice.DepositoExtra(request);
                if (result.status)
                    return Ok(result);
                else
                    return BadRequest(result.status);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: DepositoExtra - ContacorrenteCodigo: {request.contaCorrenteCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpPost("DetalhesTransacao")]
        public IActionResult DetalhesTransacaoFeita([FromBody] DetalhesTransacaoFeitaRequest request)
        {
            try
            {
                var result = _transacoesservice.DetalhesTransacaoFeita(request);
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: DepositoExtra - codigoUsuario: {request.codigoUsuario}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpGet("BuscarModalidades")]
        public IActionResult BuscarModalidades()
        {
            try
            {
                var result = _transacoesservice.BuscarModalidades();
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: BuscarModalidades"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpPost("BuscarHistoricoTransacoesPorModalidade")]
        public IActionResult BuscarHistoricoTransacoesPorModalidade([FromBody] HistoricoTransacoesPorModalidadeRequest request)
        {
            try
            {
                var result = _transacoesservice.BuscarHistoricoTransacoesPorModalidade(request);
                if (result != null && result.Count > 0)
                    return Ok(result);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Transacoes - Metodo: BuscarHistoricoTransacoesPorModalidade - usuarioCodigo: {request.usuarioCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }
    }
}

