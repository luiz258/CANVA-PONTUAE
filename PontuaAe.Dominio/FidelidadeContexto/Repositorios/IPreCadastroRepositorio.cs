using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios
{
    public interface IPreCadastroRepositorio
    {
        Task<bool> ChecarContato(string Contato);
        Task<int> ObterIdPreCadastro(string Contato);
        Task Salvar( string model);
    }
}
