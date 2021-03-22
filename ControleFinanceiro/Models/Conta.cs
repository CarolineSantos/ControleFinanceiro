using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class Conta
    {
        public int IdUsuario { get; set; }
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
    }
}
