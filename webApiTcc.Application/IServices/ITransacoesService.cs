using ApiTccManagementPersonal.Application.DTO.Request;
using CriandoApi8ParaTestar.Application.DTO.Base;
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
    public interface ITransacoesService
    {
        StatusResponse RealizarTransacaoBancaria(TransicaoBancariaRequest request);
        List<TabHistoricoTransacao> BuscarHistoricoTransacoes(int codigoContaCorrente);
        StatusResponse DepositoExtra(EntradaFinanceiraExtraRequest request);
        TransacaoFeitaResponse DetalhesTransacaoFeita(DetalhesTransacaoFeitaRequest request);
    }
}
