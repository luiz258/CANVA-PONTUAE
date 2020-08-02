using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConfigClassificacaoCliente
    {

        public ConfigClassificacaoCliente(int idEmpresa)
        {
            IdEmpresa = idEmpresa;

            QtdVisitasClassificacaoOuro = 10;
            QtdVisitasClassificacaoPrata = 9;
            QtdVisitasClassificacaoBronze = 5;
            QtdVisitaClassificacaoAtivo = 3;
   
    
            TempoEmDiasClienteOuro = 0;//  0: representa o mes atual  30 dias :30
            TempoEmDiasClientePrata = -1 ; //-1: representa o mes antecessor : 60
            TempoEmDiasClienteBronze = -3; //-3: representa 2 meses até o atual :90 
            TempoEmDiasClientePedido =180 ; // falta definir
            TempoEmDiasClienteInativo =100 ; // falta definir

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
