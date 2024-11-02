using ApiTccManagementPersonal.Application.DTO.Request;
using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Application.DTO.Request;

namespace webApiTcc.Application.IServices
{
    public interface IContaCorrenteService
    {
        StatusResponse CriarContaCorrente(ContaCorrenteRequest request);
        List<TabContaCorrente> BuscarContasCorrentesExistentes();
        List<TabContaCorrente> BuscarContasCorrentesExistentesPorUsuario(int codigoUsuario);
    }
}
