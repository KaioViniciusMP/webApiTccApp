using ApiTccManagementPersonal.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;
using webApiTcc.Application.Services;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartaoController : ControllerBase
    {
        private readonly ICartaoService _cartaoservice;
        private readonly IAutenticacoesServices _autenticacoesServices;
        public CartaoController(ICartaoService service, IAutenticacoesServices serviceAutenticacao)
        {
            _cartaoservice = service;
            _autenticacoesServices = serviceAutenticacao;
        }

        [HttpPost]
        public IActionResult InserirCartao(CartaoRequest request)
        {
            try
            {
                var result = _cartaoservice.InserirCartao(request);
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
                    referencia = $"Rota: Cartao - Metodo: InserirCartao - ContacorrenteCodigo: {request.contaCorrenteCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }

        }

        [HttpPost("BuscarCartoesCadastrados")]
        public IActionResult BuscarCartoesCadastrados(BuscarCartoesCadastradosRequest request)
        {
            try
            {
                var result = _cartaoservice.BuscarCartoesCadastrados(request);
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
                    referencia = $"Rota: Cartao - Metodo: BuscarCartoesCadastrados - usuarioCodigo: {request.usuarioCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }
    }
}
