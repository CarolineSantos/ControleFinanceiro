using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class ContaItem
    {
        public int IdContaItem { get; set; }
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
