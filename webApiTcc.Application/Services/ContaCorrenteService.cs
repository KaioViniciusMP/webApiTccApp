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
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly webApiTccContext _context;
        private readonly SuporteDal _suporteDal;
        public ContaCorrenteService(webApiTccContext context)
        {
            _context = context;
            _suporteDal = new SuporteDal(_context);
        }

        public StatusResponse CriarContaCorrente(ContaCorrenteRequest request)
        {
            StatusResponse response = new StatusResponse();
            try
            {
                var usuarioCodigo = SuporteDal.PesquisarFirstOrDefault<TabUsuario>(u => u.codigo == request.usuarioCodigo, _context);
                if (usuarioCodigo == null)
                {
                    response.status = false;
                    response.message = $"Não foi possivel criar uma conta corrente, pois não existe usuario existente para vincular a esta conta.";
                    return response;
                }

                var objContaCorrente = new TabContaCorrente()
                {
                    agencia = request.agencia,
                    saldo = request.saldo,
                    usuarioCodigo = usuarioCodigo.codigo,
                };

                _context.tabContaCorrente.Add(objContaCorrente);
                _context.SaveChanges();

                response.status = true;
                response.message = $"Conta corrente criada e vinculada ao usuario com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Não foi possivel criar uma conta corrente. Error: {ex.Message}";
                return response;
            }
        }
        public List<TabContaCorrente> BuscarContasCorrentesExistentes()
        {
            return SuporteDal.Listar<TabContaCorrente>(_context);
        }
        public StatusResponse DepositoExtra(EntradaFinanceiraExtraRequest request)
        {
            StatusResponse response = new StatusResponse();

            // Na tabela de historico de transação tempos que colocar mais uma coluna definindo se esta acontecendo um deposito ou uma transferencia de dinheiro
            // Ou seja, uma entrada ou uma saida
            // E esse metodo não deveria estar aqui, deveria estar na rota de transações financeiras 

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
                _context.SaveChanges();

                TabHistoricoTransacao tabHistoricoTransacao = new TabHistoricoTransacao();
                tabHistoricoTransacao.valorTransacao = request.valorEntrada;
                tabHistoricoTransacao.dataTransacao = DateTime.Now;
                tabHistoricoTransacao.descricao = request.descricao;
                tabHistoricoTransacao.forma_Pagamento = request.forma_Pagamento;
                tabHistoricoTransacao.contaCorrenteCodigo = contaCorrente.codigo;
                tabHistoricoTransacao.usuarioCodigo = request.usuarioCodigo;
                tabHistoricoTransacao.modalidadeCodigo = request.modalidadeCodigo;
                

                _context.tabHistoricoTransacao.Add(tabHistoricoTransacao);
                _context.SaveChanges();

                response.status = false;
                response.message = $"Deposito feito na conta corrente {contaCorrente.agencia} realizado com sucesso!";
                return response;

            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Não foi possivel transferir uma entrada financeira. Error: {ex.Message}";
                return response;
            }
        }
    }
}
