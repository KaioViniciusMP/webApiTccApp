using ApiTccManagementPersonal.Application.DTO.Request;
using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.DAL;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;

namespace webApiTcc.Application.Services
{
    public class TransacoesService : ITransacoesService
    {
        private readonly webApiTccContext _context;
        public TransacoesService(webApiTccContext context)
        {
            _context = context;
        }

        public StatusResponse RealizarTransacaoBancaria(TransicaoBancariaRequest request)
        {
            var response = new StatusResponse();

            try
            {
                // Verifica se o cartão existe
                if (SuporteDal.ValidarPorQualquer<TabCartao>(_context, s => s.CVV == request.cvvCartao)/*_context.tabCartao.Any(s => s.CVV == request.cvvCartao)*/)
                {
                    // Obtém o cartão específico
                    var cartao = _context.tabCartao.FirstOrDefault(
                        s => s.CVV == request.cvvCartao &&
                        s.forma_pagamento == request.forma_Pagamento &&
                        s.contaCorrenteCodigo == request.contaCorrenteCodigo);

                    if (cartao == null)
                    {
                        response.status = false;
                        response.message = "Cartão inválido.";
                        return response;
                    }

                    // Obtém a conta corrente associada ao cartão
                    var contaCorrente = _context.tabContaCorrente
                        .FirstOrDefault(x => x.codigo == cartao.contaCorrenteCodigo);

                    if (contaCorrente == null)
                    {
                        response.status = false;
                        response.message = "Conta corrente não encontrada.";
                        return response;
                    }

                    // Verifica se há saldo suficiente
                    if (contaCorrente.saldo < request.valorTransacao)
                    {
                        response.status = false;
                        response.message = "Saldo insuficiente.";
                        return response;
                    }

                    // Atualiza o saldo da conta corrente
                    contaCorrente.saldo -= request.valorTransacao;
                    _context.tabContaCorrente.Update(contaCorrente);
                    _context.SaveChanges();

                    // Cria o histórico de transação
                    var historicoTransacao = new TabHistoricoTransacao
                    {
                        contaCorrenteCodigo = cartao.contaCorrenteCodigo,
                        valorTransacao = request.valorTransacao,
                        dataTransacao = DateTime.Now,
                        modalidadeCodigo = request.modalidadeCodigo,
                        descricao = request.descricao,
                        titulo = request.titulo,
                        forma_Pagamento = request.forma_Pagamento,
                        usuarioCodigo = request.usuarioCodigo,
                        cvvCartao = request.cvvCartao
                    };

                    _context.tabHistoricoTransacao.Add(historicoTransacao);

                    // Salva todas as mudanças
                    _context.SaveChanges();

                    response.status = true;
                    response.message = "Compra realizada com sucesso.";
                    return response;
                }

                response.status = false;
                response.message = "Cartão inválido.";
                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = "Ocorreu um erro ao processar a compra: " + ex.Message;
                return response;
            }
        }

        public List<TabHistoricoTransacao> BuscarHistoricoTransacoes(int codigoContaCorrente)
        {
            try
            {
                return SuporteDal.ListarPorCodigo<TabHistoricoTransacao>(x => x.contaCorrenteCodigo == codigoContaCorrente, _context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
