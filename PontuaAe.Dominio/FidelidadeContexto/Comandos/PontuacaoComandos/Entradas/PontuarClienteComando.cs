using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class PontuarClienteComando: Notifiable, IComando
    {

        public int IdEmpresa { get; set; }
        public int IdPreCadastro { get; set; }   
        public int Id { get; set; }
        public string Contato { get; set; }
        public decimal ValorInfor { get; set; }

        public bool Valida()
        {
            return IsValid;
        }
    }
}
