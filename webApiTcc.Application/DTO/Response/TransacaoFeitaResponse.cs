using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Application.DTO.Response
{
    public class TransacaoFeitaResponse
    {
        public int codigoTransacao { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string agencia { get; set; }
        public decimal valorTransacao { get; set; }
        public string nomeModalidade { get; set; }
        public DateTime dataTransacao { get; set; }
        public string cvv { get; set; }
        public string bandeiraCartao { get; set; }
        public int codigoUsuario { get; set; }
        public string formaPagamento { get; set; }
    }
}
