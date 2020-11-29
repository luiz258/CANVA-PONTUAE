using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ContatosRepositorio : IContatosRepositorio
    {


        private readonly DbConfig _db;
        public ContatosRepositorio(DbConfig db)
        {
            _db = db;
        }
        public async Task Salvar(Contatos numero)
        {
            await _db.Connection
               .ExecuteAsync(" INSERT INTO Contatos (IdEmpresa, Numero) VALUES  (@IdEmpresa, @Numero)",
                   new
                   {
                       @IdEmpresa = numero.IdEmpresa,
                       @Numero = numero.Numero,

                   });
        }
    }
}
