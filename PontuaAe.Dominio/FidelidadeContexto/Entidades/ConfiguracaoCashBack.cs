using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConfiguracaoCashBack
    {
        public int Percentual { get; set; }
        public decimal ValorMinimo { get; set; }
        public bool Estado => true;


    }
}
