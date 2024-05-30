using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTccManagementPersonal.Application.DTO.Request
{
    public class ContaCorrenteRequest
    {
        public string agencia { get; set; }
        public int usuarioCodigo { get; set; }
        public decimal saldo { get; set; }
    }
}
