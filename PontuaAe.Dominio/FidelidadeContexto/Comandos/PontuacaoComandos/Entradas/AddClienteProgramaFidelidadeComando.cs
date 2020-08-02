using System;
using FluentValidator;
using FluentValidator.Validation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PontuaAe.Compartilhado.Comandos;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas
{
    public class AddClienteProgramaFidelidadeComando: Notifiable, IComando

    {
        public int IDCliente { get; set; }
        public int IDEmpresa { get; set; }
        public double Saldo { get;  set; }

        public bool Valida()
        {
            return IsValid;
        }
    }
}
