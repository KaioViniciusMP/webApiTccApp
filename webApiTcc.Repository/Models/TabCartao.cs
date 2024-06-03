using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Repository.Models
{
    public class TabCartao
    {
        [Key]
        public int codigo { get; set; }
        public int contaCorrenteCodigo { get; set; }
        public string formaPagamento { get; set; }
        public DateTime dataValidade { get; set; }
        public string bandeiraCartao { get; set; }
        public string CVV { get; set; }
        public decimal? limite { get; set; }
    }
}
