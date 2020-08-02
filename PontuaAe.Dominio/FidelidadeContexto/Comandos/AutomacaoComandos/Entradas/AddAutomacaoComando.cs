using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas
{
    public class AddAutomacaoComando : IComando
    {
        public int IdEmpresa { get; set; }
        public string TipoAutomacao { get; set; } // descricao do tipo automacao
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public int TempoPorDiaDoMes { get; set; } // dia(1 a 31) de todo mes vai envia uma mensagem definida
        public int TempoPorDia { get; set; }
        public string TempoPorDiaDaSemana { get; set; } // definir que todo dia de uma semana vai envia a oferta  ex: toda terça feira dispara o sms com a mensagem
        public int DiasAntesAniversario { get; set; } // definir o tempo antes do aniversario ex  2 dias antes sera enviado o sms da promoção  
        public string Conteudo { get; set; }


        bool IComando.Valida()
        {
            throw new NotImplementedException();
        }
    }
}
