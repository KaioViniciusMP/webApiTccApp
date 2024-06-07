using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Repository.Models
{
    public class TabHistoricoTransacao
    {
        [Key]
        public int codigo { get; set; }
        public DateTime dataTransacao { get; set; }
        public decimal valorTransacao { get; set; }
        public string formaPagamento { get; set; }
        public int contaCorrenteCodigo { get; set; }
        public int modalidadeCodigo { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public int usuarioCodigo { get; set; }
        public string cvvCartao { get; set; }
        public bool isDeposito { get; set; }
    }
}
