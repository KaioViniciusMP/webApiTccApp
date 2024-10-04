using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.DTO.Request
{
    public class GravaLogExceptionRequest
    {
        public DateTime dataHora { get; set; }
        public string referencia { get; set; }
        public string excecao { get; set; }
    }
}
