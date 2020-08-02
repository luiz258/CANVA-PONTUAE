using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class PreCadastroRepositorio : IPreCadastroRepositorio

    {

        private readonly DbConfig _db;

        public PreCadastroRepositorio(DbConfig db)
        {
            _db = db;
        }


        public async Task<bool> ChecarContato(string Contato)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS(  Select ID from PRE_CADASTRO WHERE Contato = @Contato)THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Contato = Contato });
            
        }


        public async Task<int> ObterIdPreCadastro(string Contato)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<int>("Select ID from PRE_CADASTRO WHERE Contato = @Contato", new { @Contato = Contato });

        }

        public async Task Salvar(string Contato)
        {
           await _db.Connection
                .ExecuteAsync("INSERT INTO PRE_CADASTRO (Contato) values (@Contato)", new { @Contato = Contato });
        }
    }
}
