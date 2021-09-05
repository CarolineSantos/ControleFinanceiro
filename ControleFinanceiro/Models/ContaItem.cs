using System;
using System.Collections.Generic;
using System.Text;

namespace ControleFinanceiro.Models
{
    public class ContaItem
    {
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public string Valor { get; set; }
        public DateTime Competencia { get; set; }
        public int Parcelas { get; set; }
        public int IdUsuario { get; set; }
    }
}
