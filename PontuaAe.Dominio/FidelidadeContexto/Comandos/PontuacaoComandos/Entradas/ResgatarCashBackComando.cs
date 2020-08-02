using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class ResgatarCashBackComando : Notifiable, IComando
    {

        public int IdUsuario { get; set; }
        public int IdPreCadastro { get; set; } //IdPreCadastro do contato
        public int IdEmpresa { get; set; }
        public decimal Valor { get; set; }
        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
