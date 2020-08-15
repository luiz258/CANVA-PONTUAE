using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConsultaTemplateClassificacaoCliente
    {
        public ConsultaTemplateClassificacaoCliente()
        {

        }

        public int IdEmpresa { get; set; }
        public int QtdVisitasClassificacaoOuro { get; set; }
        public int QtdVisitasClassificacaoPrata { get; set; }
        public int QtdVisitasClassificacaoBronze { get; set; }
        public int QtdVisitaClassificacaoAtivo { get; set; }

        public int TempoEmDiasClienteOuro { get; set; }
        public int TempoEmDiasClientePrata { get; set; }
        public int TempoEmDiasClienteBronze { get; set; }

        //public int TempoEmDiasClienteCasual { get; set; }
        public int TempoEmDiasClientePedido { get; set; }
        public int TempoEmDiasClienteInativo { get; set; }



    }
}
