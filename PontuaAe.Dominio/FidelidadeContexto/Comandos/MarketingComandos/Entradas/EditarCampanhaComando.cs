﻿using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas
{
    public class EditarCampanhaComando : IComando
    {

        public int ID { get; private set; }
        public string Nome { get; set; }
        public int IdEmpresa { get; set; }
        public string[,] Contatos { get; set; }        
        public string Conteudo { get; set; }        
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public int QtdSelecionado { get; set; }
        public bool Estado { get; set; }
        public DateTime DataEnvio { get; set; }
        public TimeSpan HoraEnvio { get; set; }
        public int AgendamentoAtivo { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
