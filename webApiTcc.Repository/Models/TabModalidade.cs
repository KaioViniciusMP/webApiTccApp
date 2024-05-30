using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Repository.Models
{
    public class TabModalidade
    {
        [Key]
        public int codigo { get; set; }
        public string nomeModalidade { get; set; }
    }
}
