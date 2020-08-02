using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConversaoSMS
    {
        public ConversaoSMS()
        {

        }

        public ConversaoSMS(int idEmpresa, int idSituacaoSMS, int qtdRetorno, decimal totalVendas)
        {
            IdEmpresa = idEmpresa;
            IdSituacaoSMS = idSituacaoSMS;
            QtdRetorno = qtdRetorno;
            TotalVendas = totalVendas;
        }

        public int ID { get; private set; }
        public int IdEmpresa { get; private set; }
        public int IdSituacaoSMS { get; private set; }

        public decimal QtdRetorno { get; private set; }
        public decimal TotalVendas { get; private set; }

       




    }
}
