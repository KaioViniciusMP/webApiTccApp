﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CriandoApi8ParaTestar.Repository.Models
{
    public class TabUsuario
    {
        [Key]
        public int codigo { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }

    }
}