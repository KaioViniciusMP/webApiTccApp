using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.DTO.Request
{
    public class HistoricoTransacoesPorModalidadeRequest
    {
        public int usuarioCodigo { get; set; }
        public int modalidadeCodigo { get; set; }
    }
}
