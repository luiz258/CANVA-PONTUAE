using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas
{
    public class RemoverColaboradorComando : Notifiable, IComando
    {
  
        public int ID { get; set; }
        public int IdEmpresa { get; set; }


        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
