using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.Pesquisa.Comandos.Entradas
{
    public class SalvaAvaliacaoNps : IComando
    {
        public string telefone { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCliente { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
