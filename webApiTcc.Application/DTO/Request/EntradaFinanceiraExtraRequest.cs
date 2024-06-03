using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.DTO.Request
{
    public class EntradaFinanceiraExtraRequest
    {
        public string remetente { get; set; }
        public decimal valorEntrada { get; set; }
        public string descricao { get; set; }
        public int contaCorrenteCodigo { get; set; }
        public string forma_Pagamento { get; set; }
        public int usuarioCodigo { get; set; }
        public int modalidadeCodigo { get; set; }
        public string cvvCartao { get; set; }
        public string titulo { get; set; }
    }
}
