using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Mensagem
    {
        public Mensagem()
        {

        }
       
        //Alteração da campanha automatica   
        public Mensagem(
            int id, 
            int idEmpresa,
            string tipoAutomacao,
            string diaSemana,
            int diasAntesAniversario,
            int diaMes,
            string conteudo,
            string segmentacao,
            string segCustomizado,
            int tempoPorDia)
        {
            ID = id;
            IdEmpresa = idEmpresa;
            TipoAutomacao = tipoAutomacao;
            TempoPorDiaDaSemana = diaSemana;
            DiasAntesAniversario = diasAntesAniversario;
            TempoPorDiaDoMes = diaMes;
            Conteudo = conteudo;
            SegCustomizado = segCustomizado;
            Segmentacao = segmentacao;
            TempoPorDia = tempoPorDia;
            Estado = true;
            EstadoEnvio = "Automatico";
        }
       
        //Criação da campanha automatica 
        public Mensagem(
            int idEmpresa, 
            string tipoAutomacao,
            string diaSemana, 
            int diasAntesAniversario, 
            int diaMes,
            string conteudo,
            string segmentacao,
            string segCustomizado,
            int tempoPorDia)
        {
            IdEmpresa = idEmpresa;
            TipoAutomacao = tipoAutomacao;
            TempoPorDiaDaSemana = diaSemana;
            DiasAntesAniversario = diasAntesAniversario;
            TempoPorDiaDoMes = diaMes;
            Conteudo = conteudo;
            Segmentacao = segmentacao;
            SegCustomizado = segCustomizado;
            TempoPorDia = tempoPorDia;
            Estado = true;
            ValorInvestido = 0;
            QtdEnviada = 0;
            EstadoEnvio = "Automatico";
        }


        //Campanha Agendada, usado para criar uma campanha agendada
        public Mensagem( int idEmpresa, string nome, string segmentacao, string segCustomizado, int qtdSelecionado, Agenda agendar, string conteudo)
        {
           
            IdEmpresa = idEmpresa;
            Nome = nome;
            Segmentacao = segmentacao;
            SegCustomizado = segCustomizado;
            QtdSelecionado = qtdSelecionado;
            Agendar = agendar;
            EstadoEnvio = "Ok";
            Conteudo = conteudo;

        }

      
        //Campanha envio normal
        public Mensagem(int idEmpresa, string nome, string segmentacao, string segCustomizado, int qtdSelecionado, string conteudo, Agenda agendar )
        {
          
            IdEmpresa = idEmpresa;
            Nome = nome;
            Segmentacao = segmentacao;
            SegCustomizado = segCustomizado;
            QtdSelecionado = qtdSelecionado;
            EstadoEnvio = "OK";
            Conteudo = conteudo;
            Agendar = agendar;

        }        

        //apos recebe o status da campanha da API SMS altera o status da campanhar no banco Local
        public Mensagem(int  id, int idEmpresa, Agenda agendar)
        {
          
            ID = id;
            IdEmpresa = idEmpresa;
            Agendar = agendar;

        }

        public Mensagem(int idEmpresa, int id, string estadoEnvio)
        {
            EstadoEnvio = estadoEnvio;
            IdEmpresa = idEmpresa;
            ID = id;
        }

        public int ID { get; private set; }
        public int IdEmpresa { get; private set; }
        public string TipoAutomacao { get; private set; } //Esse atributo representa a descricao Tipo em campanha automatizada,
        public string Segmentacao { get; private set; }
        public string SegCustomizado { get; private set; }
        public string TempoPorDiaDaSemana { get; private set; } // definir que todo dia de uma semana vai envia a oferta  ex: toda terça feira dispara o sms com a mensagem
        public int DiasAntesAniversario { get; private set; } // definir o tempo antes do aniversario ex  2 dias antes sera enviado o sms da promoção  
        public int TempoPorDiaDoMes { get; private set; } //  dia(1 a 31) de todo mes vai envia uma mensagem definida 
        public string Conteudo { get; private set; }
        public string Nome { get; private set; }
        public int QtdSelecionado { get; private set; }
        public Agenda Agendar { get; set; }
        public int QtdEnviada { get; private set; }
        public double ValorInvestido { get; private set; }
        public bool Estado { get; private set; }
        public string EstadoEnvio { get; private set; }
        public string TipoBusca { get; private set; }
        public int TempoPorDia { get; private set; }
   
        public void CalcularQtdEnviado(int qtdEnviada) => ValorInvestido = 0.12 * (QtdEnviada += qtdEnviada); 



    }
}
