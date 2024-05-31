using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webApiTcc.Repository.DTO
{
    public class UsuarioResponse
    {
        public int codigo { get; set; }
        public string nome { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
    }
}
