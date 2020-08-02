using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas
{
    public class EditarRegraPontuacaoComando : IComando
    {
        public string Nome { get; set; }
        public int IdEmpresa{ get; set; }
        public decimal Reais { get; set; }
        public decimal PontosFidelidade { get; set; }
        public int ValidadePontos { get; set; }
        public int TipoProgramaFidelidade { get; set; }



        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
