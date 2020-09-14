using Dapper;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DbConfig _db;

        public UsuarioRepositorio(DbConfig db)
        {
            _db = db;
        }

        
        public async Task AlteraSenha(int ID, string Senha)  //VERIFICA SE ESTÁ SENDO UTILIZADO
        {
            await _db.Connection.ExecuteAsync("UPDATE USUARIO SET  Senha=@Senha  WHERE  ID=@ID", new { @Senha = Senha, @ID = ID });
        }

        public async Task Deletar(int ID)
        {
            await _db.Connection.ExecuteScalarAsync("DELETE FROM USUARIO WHERE ID=@ID", new { @ID = ID });
        }

        public async Task Desativar(int ID)
        {
            await _db.Connection.ExecuteAsync("UPDATE USUARIO SET Estado = 0 WHERE ID=@ID", new { @ID = ID });
        }

        public async Task Editar(Usuario usuario)
        {
            await _db.Connection
                  .ExecuteAsync("UPDATE USUARIO SET  Email = @Email, Senha = @Senha WHERE ID=@ID", new
                  {

                      @Email = usuario.Email,
                      @Senha = usuario.Senha,
                      @RoleId = usuario.RoleId,
                      @ID = usuario.ID
                  });
        }

        public async Task<Usuario> ObterUsuario(string Email)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<Usuario>("SELECT Email, ID, RoleId, Senha FROM USUARIO WHERE Email = @Email", new { @Email = Email });

        }

        public async Task<Usuario> ObterUsuarioCliente(string Email)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<Usuario>("SELECT c.Contato ,u.Email, u.ID, u.ClaimType, u.ClaimValue FROM USUARIO u INNER JOIN CLIENTE c ON u.ID = c.IdUsuario WHERE u.Email = @Email", new { @Email = Email });

        }


        public async Task Salvar(Usuario usuario)
        {
           await _db.Connection
               .ExecuteAsync("INSERT INTO USUARIO (Email, Senha, RoleId, Estado) VALUES ( @Email, @Senha, @RoleId, @Estado)", new
               {

                   @Email = usuario.Email,
                   @Senha = usuario.Senha,
                   @RoleId = usuario.RoleId,
                   @Estado = usuario.Estado,

               });

        }

        public async Task<bool> ValidaCPF(string NumeroCpf)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM USUARIO WHERE CPF = @NumeroCpf) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @NumeroCpf = NumeroCpf });

        }

        public async Task<bool> ValidaEmail(string Email)
        {
            return await _db.Connection
                 .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM USUARIO WHERE Email = @Email) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Email = Email });

        }

        public async Task ResetaSenha(string Senha, int ID)
        {
            await _db.Connection.ExecuteAsync("UPDATE USUARIO SET Senha=@Senha WHERE ID=@ID", new { @Senha = Senha, @ID = ID });
        }

        public async Task AlteraConta(int ID, string Email, string Senha)
        {
            await _db.Connection.ExecuteAsync("UPDATE USUARIO SET  Senha=@Senha WHERE ID=@ID", new { @ID = ID, @Senha = Senha});
        }
    }
}
