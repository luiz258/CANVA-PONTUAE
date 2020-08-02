using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class SituacaoSMS
    {
        public SituacaoSMS()
        {
                
        }

        //dados para atualiza o resultado do sms para o lead
        public SituacaoSMS(decimal valorRecebido, DateTime? dataVenda,  string contato, int idEmpresa, int idMensagem, int idSMS)
        {
            ValorRecebido = valorRecebido;
            DataCompra = dataVenda;
            //DataRecebida = dataEnvio;
            Contatos = contato;
            IdMensagem = idMensagem;
            IdEmpresa = idEmpresa;
            IdSMS = idSMS;
        }
        public SituacaoSMS(DateTime? dataEnvio, string contato, int idEmpresa, int idMensagem, int idSMS)
        {
            DataRecebida = dataEnvio ;
            Contatos = contato;
            IdEmpresa = idEmpresa;
            IdMensagem = idMensagem;
            ValorRecebido = 0;
            IdSMS = idSMS;
        }

        public int ID { get; private set; }
        public int IdEmpresa { get; private set; }
        public int IdMensagem { get; private set; }
        public int IdSMS { get; private set; }
        public string Contatos { get; private set; }
        public DateTime? DataRecebida { get; private set; }
        public DateTime? DataCompra { get; private set; }
        public string Estado { get; private set; }
        public string Verificado { get; private set; }
        public decimal ValorRecebido { get; private set; }

        ///public void CalcularConversao(int qtdRetorno, decimal valorGasto) => ValorRecebido = valorGasto * qtdRetorno;  REMOVER

    }
}
