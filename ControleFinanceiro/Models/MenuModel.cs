using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class MenuModel
    {
        public string Titulo { get; set; }
        public string Icone { get; set; }
        public Type Pagina { get; set; }

        public MenuModel() { }
    }
}
