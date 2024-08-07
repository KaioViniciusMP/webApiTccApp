﻿using ApiTccManagementPersonal.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartaoController : ControllerBase
    {
        private readonly ICartaoService _cartaoservice;
        public CartaoController(ICartaoService service)
        {
            _cartaoservice = service;
        }

        [HttpPost]
        public IActionResult InserirCartao(CartaoRequest request)
        {
            var result = _cartaoservice.InserirCartao(request);
            if (result.status)
                return Ok(result);
            else
                return BadRequest(result.message);
        }

        [HttpPost("BuscarCartoesCadastrados")]
        public IActionResult BuscarCartoesCadastrados(BuscarCartoesCadastradosRequest request)
        {
            var result = _cartaoservice.BuscarCartoesCadastrados(request);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

    }
}
