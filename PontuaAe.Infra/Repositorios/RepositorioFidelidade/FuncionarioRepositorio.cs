using Dapper;
using PontuaAe.Domain.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.FuncionarioConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class FuncionarioRepositorio : IFuncionarioRepositorio
    {
        private readonly DbConfig _db;

        public FuncionarioRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task Salvar(Funcionario funcionario)
        {
           await  _db.Connection
               .ExecuteAsync("INSERT INTO FUNCIONARIO (NomeCompleto, Contato, IdEmpresa, IdUsuario) values (@NomeCompleto, @Contato, @IdEmpresa, @IdUsuario )", new
               {
                   @NomeCompleto = funcionario.NomeCompleto,
                   @Contato = funcionario.Contato,
                   @IdEmpresa = funcionario.IdEmpresa,
                   @IdUsuario = funcionario.IdUsuario

                 
               }); 
        }

        public async Task Deletar(int IdUsuario, int IdEmpresa)
        {
           await _db.Connection.ExecuteAsync("DELETE FROM FUNCIONARIO WHERE IdUsuario=@IdUsuario AND IdEmpresa = @IdEmpresa", new { @IdUsuario = IdUsuario, @IdEmpresa = IdEmpresa });
        }

        public async Task Editar(Funcionario funcionario)
        {
          await _db.Connection
               .ExecuteAsync("UPDATE FUNCIONARIO SET NomeCompleto=@NomeCompleto, Contato=@Contato WHERE  IdEmpresa=@IdEmpresa", new
               {
                   @NomeCompleto = funcionario.NomeCompleto,
                   @Contato = funcionario.Contato,
                   @IdEmpresa = funcionario.IdEmpresa,
               });
        }

        public async Task<int> ObterIdEmpresa(int IdUsuario)
        {
            return await _db.Connection
                 .ExecuteScalarAsync<int>("SELECT IdEmpresa FROM FUNCIONARIO WHERE IdUsuario=@IdUsuario", new
                 { @IdUsuario = IdUsuario });


        }

        public async Task<IEnumerable<ListaFuncionarioConsulta>> ListaFuncionario(int IdEmpresa)
        {
            return await _db.Connection
                .QueryAsync<ListaFuncionarioConsulta>("SELECT f.NomeCompleto, u.ID, u.Email  FROM USUARIO u INNER JOIN FUNCIONARIO f ON u.ID = f.IdUsuario WHERE f.IdEmpresa = @IdEmpresa", new
                {
                    @IdEmpresa = IdEmpresa
                });
        }


        public async Task<ObterDetalheFuncionarioConsulta> ObterDetalheFuncionario(int ID, int IdEmpresa)
        {
            return await _db.Connection.QueryFirstOrDefaultAsync<ObterDetalheFuncionarioConsulta>("select u.ID, f.NomeCompleto, u.Email, u.Senha," +
      " u.RoleId from FUNCIONARIO f  INNER JOIN USUARIO  u ON f.IdUsuario = u.ID WHERE u.ID=@ID AND f.IdEmpresa=@IdEmpresa", new { @ID = ID, @IdEmpresa = IdEmpresa });
        }



        public async Task<string> ObterContatoFuncionario(int IdUsuario)
        {
            return await _db.Connection
              .QueryFirstOrDefaultAsync<string>("SELECT f.Contato FROM USUARIO u INNER JOIN FUNCIONARIO f ON u.ID = f.IdUsuario WHERE f.IdUsuario = @IdUsuario", new { @IdUsuario = IdUsuario });

        }

        public async Task<int> ObterId(int ID)
        {
            return await _db.Connection.ExecuteScalarAsync<int>("SELECT ID FROM FUNCIONARIO WHERE ID=@ID", new {@ID = ID });
        }
    }
}
