using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Repository.Models
{
    public class tabLogExcecao
    {
        [Key]
        public long codigo { get; set; }
        public DateTime dataHora { get; set; }
        public string referencia { get; set; }
        public string excecao { get; set; }
    }
}
