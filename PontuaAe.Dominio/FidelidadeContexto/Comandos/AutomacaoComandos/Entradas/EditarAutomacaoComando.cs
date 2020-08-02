using PontuaAe.Compartilhado.Comandos;
using System;


namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas
{
    public class EditarAutomacaoComando : IComando
    {
        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public string TipoAutomacao { get; set; } //nome da automacao
        public string Segmentacao { get; set; }
        public string SegCustomizado { get; set; }
        public string TempoPorDiaDaSemana { get; set; } // definir que todo dia de uma semana vai envia a oferta  ex: toda terça feira dispara o sms com a mensagem
        public int DiasAntesAniversario { get; set; } // definir o tempo antes do aniversario ex  2 dias antes sera enviado o sms da promoção  
        public int DiaMes { get; set; } //  dia(1 a 31) de todo mes vai envia uma mensagem definida 
        public string TempoPorDiaMes { get; set; }
        public int TempoPorDia { get; set; }
        public string Conteudo { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
