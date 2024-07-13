using ApiTccManagementPersonal.Application.DTO.Request;
using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.DAL;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;

namespace webApiTcc.Application.Services
{
    public class CartaoService : ICartaoService
    {
        private readonly webApiTccContext _context;

        private readonly SuporteDal _suporteDal;
        public CartaoService(webApiTccContext context)
        {
            _context = context;
            _suporteDal = new SuporteDal(_context);
        }

        public StatusResponse InserirCartao(CartaoRequest request)
        {
            StatusResponse response = new StatusResponse();

            try
            {
                var contaCorrenteCodigo = SuporteDal.PesquisarFirstOrDefault<TabContaCorrente>(c => c.codigo == request.contaCorrenteCodigo, _context);
                if (contaCorrenteCodigo == null)
                {
                    response.status = false;
                    response.message = $"Não foi possivel criar um cartão pois não existe conta corrente existente para ser vinculada.";
                    return response;
                }

                var objCartao = new TabCartao()
                {
                    formaPagamento = request.forma_pagamento,
                    dataValidade = request.dataValidade,
                    bandeiraCartao = request.bandeiraCartao,
                    CVV = request.CVV,
                    limite = request.limite,
                    contaCorrenteCodigo = contaCorrenteCodigo.codigo
                };

                _context.tabCartao.Add(objCartao);
                _context.SaveChanges();

                response.status = true;
                response.message = $"Cartão criado e vinculado a uma conta corrente com sucesso.";
                response.objInfo = objCartao;

                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Não foi possivel inserir o cartão. Error: {ex.Message}";
                return response;
            }
        }

        public List<BuscarCartoesCadastradosResponse> BuscarCartoesCadastrados(BuscarCartoesCadastradosRequest request)
        {
            //return SuporteDal.Listar<TabCartao>(_context);

            var query = (from tc in _context.tabCartao
                         join tcc in _context.tabContaCorrente on tc.contaCorrenteCodigo equals tcc.codigo
                         join tu in _context.tabUsuario on tcc.usuarioCodigo equals tu.codigo
                         where tu.codigo == request.usuarioCodigo
                         select new BuscarCartoesCadastradosResponse
                         {
                             BandeiraCartao = tc.bandeiraCartao,
                             CodigoCartao = tc.codigo,
                             ContaCorrenteCodigo = tc.contaCorrenteCodigo,
                             Cvv = tc.CVV,
                             DataValidade = tc.dataValidade,
                             FormaPagamento = tc.formaPagamento,
                             Limite = tc.limite,
                             UsuarioCodigo = tu.codigo
                         }).ToList();

            return query;
        }
    }
}

public class BuscarCartoesCadastradosResponse
{
    public int CodigoCartao { get; set; }
    public string BandeiraCartao { get; set; }
    public int UsuarioCodigo { get; set; }
    public int ContaCorrenteCodigo { get; set; }
    public string Cvv { get; set; }
    public DateTime DataValidade { get; set; }
    public string FormaPagamento { get; set; }
    public decimal? Limite { get; set; }
}
