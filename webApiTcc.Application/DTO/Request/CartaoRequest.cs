using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTccManagementPersonal.Application.DTO.Request
{
    public class CartaoRequest
    {
        public int contaCorrenteCodigo { get; set; }
        public string forma_pagamento { get; set; }
        public DateTime dataValidade { get; set; }
        public string bandeiraCartao { get; set; }
        public string CVV { get; set; }
        public decimal? limite { get; set; }
    }
}
