﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.ViewModel
{
    public class JogoViewModel
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Produtora { get; set; }
        
        public double Preco { get; set; }
    }
}
