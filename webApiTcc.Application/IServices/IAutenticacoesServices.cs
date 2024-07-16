using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.DTO.Response;

namespace webApiTcc.Application.IServices
{
    public interface IAutenticacoesServices
    {
        AutenticacaoResponse Autenticar(AutenticacaoRequest request);
        string GerarTokenJwt(TabUsuario usuario);
    }
}
