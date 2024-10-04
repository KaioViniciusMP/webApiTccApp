using ApiTccManagementPersonal.Application.DTO.Request;
using Azure.Core;
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
        private readonly IAutenticacoesServices _autenticacoesServices;
        public ContaCorrenteController(IContaCorrenteService service, IAutenticacoesServices serviceAutenticacao)
        {
            _contacorrenteservice = service;
            _autenticacoesServices = serviceAutenticacao;
        }

        [HttpPost]
        public IActionResult CriarContaCorrente([FromBody] ContaCorrenteRequest request)
        {
            try
            {
                var result = _contacorrenteservice.CriarContaCorrente(request);
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
                    referencia = $"Rota: ContaCorrente - Metodo: InserirCartao - usuarioCodigo: {request.usuarioCodigo}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult BuscarContasCorrentesExistentes()
        {
            try
            {
                var result = _contacorrenteservice.BuscarContasCorrentesExistentes();
                if (result != null)
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
                    referencia = $"Rota: ContaCorrente - Metodo: BuscarContasCorrentesExistentes"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }
    }
}
