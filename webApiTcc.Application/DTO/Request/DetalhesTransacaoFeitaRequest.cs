using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.DTO.Request
{
    public class DetalhesTransacaoFeitaRequest
    {
        public int codigoTransacaoFeita { get; set; }
        public int codigoUsuario { get; set; }
    }
}
