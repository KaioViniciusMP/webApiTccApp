using ApiTccManagementPersonal.Application.DTO.Response;
using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Application.DTO.Request;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.DAL;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;
using webApiTcc.Repository.DTO;

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
                    nome = request.nome,
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

        public StatusResponse EditarUsuario(UsuarioRequest request, int usuarioCodigo)
        {
            StatusResponse response = new StatusResponse();
            try
            {
                TabUsuario objUs = SuporteDal.PesquisarFirstOrDefault<TabUsuario>(c => c.codigo == usuarioCodigo, _context);
                
                if(objUs == null)
                {
                    response.status = false;
                    response.message = $"Não a usuario existente que possa ser editado.";
                    return response;
                }

                //Repository.DTO.UsuarioResponse usuario = objUs.ToResponse();

                objUs.usuario = request.usuario;
                objUs.senha = request.senha;
                objUs.nome = request.nome;

                _context.tabUsuario.Update(objUs /*usuario*/);
                _context.SaveChanges();

                response.status = true;
                response.message = $"Usuario Atualizado com sucesso.";
                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Ocorreu algum erro ao Editar o usuario. Error: {ex.Message}";
                return response;
            }
        }
    }
}
