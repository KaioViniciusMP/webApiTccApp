using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Repository.Models
{
    public class TabContaCorrente
    {
        [Key]
        public int codigo { get; set; }
        public string agencia { get; set; }
        public int usuarioCodigo { get; set; }
        public decimal saldo { get; set; }
    }
}
