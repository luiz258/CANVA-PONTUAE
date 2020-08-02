using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class ContaSMSRepositorio : IContaSMSRepositorio
    {
        private readonly DbConfig _db;

        public ContaSMSRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task Editar(ContaSMS model)
        {

           await _db.Connection.ExecuteAsync("UPDATE CONTA_SMS SET Saldo=@Saldo WHERE IdEmpresa=@IdEmpresa ", new { @Saldo = model.Saldo }) ;
        }

        public async Task Salvar(ContaSMS model)
        {
            await _db.Connection.ExecuteAsync("INSERT INTO CONTA_SMS (Saldo, IdEmpresa) VALUES(@Saldo, @IdEmpresa)", new {@Saldo = model.Saldo, @IdEmpresa = model.IdEmpresa });
        }
    }
}
