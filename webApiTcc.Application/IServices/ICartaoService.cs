﻿using ApiTccManagementPersonal.Application.DTO.Request;
using CriandoApi8ParaTestar.Application.DTO.Base;
using CriandoApi8ParaTestar.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.IServices
{
    public interface ICartaoService
    {
        StatusResponse InserirCartao(CartaoRequest request);
        List<TabCartao> BuscarCartoesCadastrados();
    }
}
