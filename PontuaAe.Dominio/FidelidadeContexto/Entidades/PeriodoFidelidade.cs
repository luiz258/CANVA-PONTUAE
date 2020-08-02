using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class PeriodoFidelidade
    {

        //Perfil cliente
    
        //Sem visitas a mais de 300
        private int _TempoMinimoClientePerdido;
        //Sem visitas a mais de 365
        private int _TempoMinimoClienteInativo;

        private int _TempoMaximoClienteNovo; //90 dias
        private int _TempoMaximoClienteVIP; //COLOCAR 30 DIAS      
        private int _TempoMaximoClienteFrequente;
        private int _TempoMaximoClienteEventual;

        // ausente há masis e 300 dias
        //Tempo minimo para considerar cliente perdido 180 dias
        public int TempoMinimoClientePerdido
        {
            get { return _TempoMinimoClientePerdido; }
            set { _TempoMinimoClientePerdido = 300; }
        }
        //Ausente  a mais de 365 dias
        public int TempoMinimoClienteInativo
        {
            get { return _TempoMinimoClienteInativo; }
            private set { _TempoMinimoClienteInativo = 365; }
        }

        //Classificação por Tipo de cliente
        // COLOCA O NUMERO DE VISITAS PARA CLASSIFICA O STATUS DO TIPO DE CLIENTE
        //10 VISITAS OU MAIS DENTRO DE 1 MÊS
        private int _ClassificacaoOuro;
        //6 A 10 VISITAS DENTRO DE 2 MESES
        private int _ClassificacaoPrata;
        //4-10 VISTAS DENTRO DE 3 MESES 
        private int _ClassificacaoBronze;
        // 3 VISITA DENTRO DE 3 MESES
        private int _ClassificacaoEmRisco; // EVENTUAL

        // 1 visita classificado o status como Ativo
        private int _ClassificacaoAtivo;
        private int _ClassificacaoInativo;

        public int ClassificacaoOuro
        {
            get { return _ClassificacaoOuro; }
            set { _ClassificacaoOuro = 10; }
        }

        public int ClassificacaoPrata
        {
            get { return _ClassificacaoPrata; }
            set { _ClassificacaoPrata = 9; }
        }

        public int ClassificacaoBronze
        {
            get { return _ClassificacaoBronze; }
            set { _ClassificacaoBronze = 8; }
        }
        // tem até 3 visitas porém ja faz ao menos 3 meses que não vêm
        public int ClassificacaoEmRisco
        {
            get { return _ClassificacaoEmRisco; }
            set { _ClassificacaoEmRisco = 3; }
        }


        public int ClassificarEmRisco
        {
            //tem  de 4 a 10 visitas, mas não vem há mais de 6 meses
            get { return _ClassificacaoEmRisco; }
            set { _ClassificacaoEmRisco = 4; }
        }

        public int ClassificacaoAtivo
        {
            get { return _ClassificacaoAtivo; }
            set { _ClassificacaoAtivo = 1; }
        }

    }
}

