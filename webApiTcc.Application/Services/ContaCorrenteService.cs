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
    }
}
