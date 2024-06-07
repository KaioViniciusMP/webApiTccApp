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
using webApiTcc.Application.DTO.Response;
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
                        s.formaPagamento == request.formaPagamento &&
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
                        formaPagamento = request.formaPagamento,
                        usuarioCodigo = request.usuarioCodigo,
                        cvvCartao = request.cvvCartao,
                        isDeposito = false
                    };

                    _context.tabHistoricoTransacao.Add(historicoTransacao);

                    // Salva todas as mudanças
                    _context.SaveChanges();

                    response.status = true;
                    response.message = "Compra realizada com sucesso.";
                    response.objInfo = historicoTransacao;
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
        public StatusResponse DepositoExtra(EntradaFinanceiraExtraRequest request)
        {
            StatusResponse response = new StatusResponse();

            // Na tabela de historico de transação tem que colocar mais uma coluna definindo se esta acontecendo um deposito ou uma transferencia de dinheiro
            // Ou seja, uma entrada ou uma saida

            try
            {
                var usuario = SuporteDal.PesquisarFirstOrDefault<TabUsuario>(u => u.codigo == request.usuarioCodigo, _context);
                if (usuario == null)
                {
                    response.status = false;
                    response.message = "Usuário não encontrado.";
                    return response;
                }

                // Verifique se a conta corrente existe e pertence ao usuário especificado
                var contaCorrente = SuporteDal.PesquisarFirstOrDefault<TabContaCorrente>(
                    c => c.codigo == request.contaCorrenteCodigo && c.usuarioCodigo == request.usuarioCodigo, _context);

                if (contaCorrente == null)
                {
                    response.status = false;
                    response.message = "Conta corrente não encontrada ou não pertence ao usuário especificado.";
                    return response;
                }

                // Atualizar o saldo da conta corrente existente
                contaCorrente.saldo += request.valorEntrada;

                _context.tabContaCorrente.Update(contaCorrente);

                TabHistoricoTransacao tabHistoricoTransacao = new TabHistoricoTransacao();
                tabHistoricoTransacao.valorTransacao = request.valorEntrada;
                tabHistoricoTransacao.cvvCartao = request.cvvCartao;
                tabHistoricoTransacao.dataTransacao = DateTime.Now;
                tabHistoricoTransacao.descricao = request.descricao;
                tabHistoricoTransacao.formaPagamento = request.forma_Pagamento;
                tabHistoricoTransacao.contaCorrenteCodigo = contaCorrente.codigo;
                tabHistoricoTransacao.usuarioCodigo = request.usuarioCodigo;
                tabHistoricoTransacao.modalidadeCodigo = request.modalidadeCodigo;
                tabHistoricoTransacao.titulo = request.titulo;
                tabHistoricoTransacao.isDeposito = true;


                _context.tabHistoricoTransacao.Add(tabHistoricoTransacao);
                _context.SaveChanges();

                response.status = true;
                response.message = $"Deposito realizado com sucesso!";
                response.objInfo = tabHistoricoTransacao;
                return response;

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Não foi possivel transferir uma entrada financeira. Error: {ex.Message}";
                return response;
            }
        }
        public TransacaoFeitaResponse DetalhesTransacaoFeita(DetalhesTransacaoFeitaRequest request)
        {
            try
            {
                var item = (from tht in _context.tabHistoricoTransacao
                            join tm in _context.tabModalidade on tht.modalidadeCodigo equals tm.codigo
                            join tc in _context.tabCartao on tht.cvvCartao equals tc.CVV
                            join tcc in _context.tabContaCorrente on tht.contaCorrenteCodigo equals tcc.codigo
                            join tu in _context.tabUsuario on tht.usuarioCodigo equals tu.codigo
                            where tht.codigo == request.codigoTransacaoFeita & tht.usuarioCodigo == request.codigoUsuario
                            select new
                            {
                                codigoTransacao = tht.codigo,//codigoTransacao
                                tituloTransacao = tht.titulo,//tituloTransacao
                                descricaoTransacao = tht.descricao,
                                agenciaUsada = tcc.agencia,
                                formaPagamentoUsada = tht.formaPagamento,
                                valorTransacaoFeita = tht.valorTransacao,
                                modalidade = tm.nomeModalidade,
                                dataTransacaoFeita = tht.dataTransacao,
                                cvv = tc.CVV,
                                bandeiraCartaoUtilizado = tc.bandeiraCartao,
                                codigoUsuarioUsado = tht.usuarioCodigo
                            }).FirstOrDefault();

                if (item != null)
                    return ToResponseDetalhesTransacaoFeita(item);

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private TransacaoFeitaResponse ToResponseDetalhesTransacaoFeita(dynamic obj)
        {
            return new TransacaoFeitaResponse
            {
                codigoTransacao = obj.codigoTransacao,
                agencia = obj.agenciaUsada,
                bandeiraCartao = obj.bandeiraCartaoUtilizado,
                codigoUsuario = obj.codigoUsuarioUsado,
                cvv = obj.cvv,
                dataTransacao = obj.dataTransacaoFeita,
                descricao = obj.descricaoTransacao,
                nomeModalidade = obj.modalidade,
                titulo = obj.tituloTransacao,
                valorTransacao = obj.valorTransacaoFeita,
                formaPagamento = obj.formaPagamentoUsada,
            };
        }
    }
}
