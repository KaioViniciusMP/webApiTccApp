using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Application.DTO.Base
{
    public class StatusResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object? objInfo { get; set; }
    }
}
