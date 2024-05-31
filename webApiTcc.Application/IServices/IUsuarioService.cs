using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Application.DTO.Request;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.IServices
{
    public interface IUsuarioService
    {
        StatusResponse CadastrarUsuario(UsuarioRequest request);
        List<TabUsuario> BuscarUsuariosCadastrados();
        StatusResponse EditarUsuario(UsuarioRequest request, int usuarioCodigo);
    }
}
