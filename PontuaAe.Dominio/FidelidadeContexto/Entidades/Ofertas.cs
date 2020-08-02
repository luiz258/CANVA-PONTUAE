using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Ofertas
    {
        public Ofertas(Empresa empresa, string descricao, string imagem, DateTime validade)
        {
          
            Descricao = descricao;
            Imagem = imagem;
            Validade = validade;
        }

        public int IdEmpresa { get; private set; }
        public string Descricao { get; private set; }
        public string Imagem { get; private set; }
        public DateTime Validade { get; private set; }
    }
}
