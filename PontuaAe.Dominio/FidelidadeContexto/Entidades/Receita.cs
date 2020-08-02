
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Receita
    {
        public Receita()
        {

        }

        public Receita( decimal valor, int idEmpresa, int idPontuacao, int idUsuario, string tipoAtividade)
        {

            IdEmpresa = idEmpresa;
            IdPontuacao = idPontuacao;
            IdUsuario = idUsuario;
            Valor = valor;
            TipoAtividade = tipoAtividade;
            DataVenda = DateTime.Now;//.ToString("dd-MM-yyyy);
  
        }

        public int IdUsuario { get; set; }
        public int IdEmpresa { get; private set; }
        public int IdPontuacao { get; private set; }
        public decimal Valor { get; private set; }
        public string TipoAtividade { get; private set; }
        public DateTime DataVenda { get; private set; }

       

    }
}

