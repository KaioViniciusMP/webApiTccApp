using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Application.DTO.Request;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;

namespace webApiTcc.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly webApiTccContext _context;
        public UsuarioService(webApiTccContext context)
        {
            _context = context;
        }

        public List<TabUsuario> BuscarUsuariosCadastrados()
        {
            var usuarios = _context.tabUsuario.ToList();
            return usuarios;
        }

        public StatusResponse CadastrarUsuario(UsuarioRequest request)
        {
            var response = new StatusResponse();
            try
            {
                var usuario = new TabUsuario()
                {
                    senha = request.senha,
                    usuario = request.usuario
                };

                _context.tabUsuario.Add(usuario);
                _context.SaveChanges();

                response.status = true;
                response.message = "Cadastro de usuario feito com sucesso";

                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Não foi possivel cadastrar um usuario Error: {ex.Message}";

                return response;
            }
        }
    }
}
