using CriandoApi8ParaTestar.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.DTO.Response;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacoesController : ControllerBase
    {
        private readonly IAutenticacoesServices _autenticacoesServices;
        public AutenticacoesController(IAutenticacoesServices service)
        {
            _autenticacoesServices = service;
        }
        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            AutenticacaoResponse resposta = _autenticacoesServices.Autenticar(request);
            if (resposta == null)
            {
                return Unauthorized();
            }
            return Ok(resposta);
        }
    }
}



