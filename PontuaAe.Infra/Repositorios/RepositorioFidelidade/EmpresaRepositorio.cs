using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using Dapper;
using System.Linq;
using PontuaAe.Infra.FidelidadeContexto.DataContexto;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.EmpresaConsulta;
using System.Collections.Generic;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ConfigPontuacaoConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.UsuarioConsulta;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios.RepositorioFidelidade
{
    public class EmpresaRepositorio : IEmpresaRepositorio

    {
        private readonly DbConfig _db;
        public EmpresaRepositorio(DbConfig db)
        {
            _db = db;
        }

        public async Task<bool> ChecarDocumento(string Documento)
        {
             return await _db.Connection
                .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT ID FROM EMPRESA WHERE Documento= @Documento) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Documento = Documento });

        }

        public async Task<bool> ChecarEmail(string Email)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS( SELECT EMAIL FROM EMPRESA WHERE Email = @Email ) THEN CAST( 1 AS BIT) ELSE CAST(0 AS BIT)  END", new { @Email = Email });

        }

        public async Task Editar(Empresa empresa)
        {
           await _db.Connection
                .ExecuteAsync("UPDATE EMPRESA SET " +
                    "NomeFantasia=@NomeFantasia, " +
                    "Descricao=@Descricao, " +
                    "NomeResponsavel=@NomeResponsavel, " +
                    "Email=@Email, " +
                    "Telefone=@Telefone, " +
                    "Horario=@Horario, " +
                    "Facebook=@Facebook, " +
                    "Website=@Website, " +
                    "Instagram=@Instagram, " +
                    "Delivery=@Delivery, " +
                    "Bairro=@Bairro, " +
                    "Rua=@Rua, " +
                    "Numero=@Numero, " +
                    "Cep=@Cep, " +
                    "Cidade=@Cidade, " +
                    "Estado=@Estado, " +
                    "Logo=@Logo, " +
                    "Complemento=@Complemento " +
                    "WHERE IdUsuario=@IdUsuario ",
                 new
                 {
                     @NomeFantasia = empresa.NomeFantasia,
                     @Descricao = empresa.Descricao,
                     @NomeResponsavel = empresa.NomeResponsavel,
                     @Email = empresa.Email,
                     @Telefone = empresa.Telefone,
                     @Horario = empresa.Horario,
                     @Facebook = empresa.Facebook,
                     @Website = empresa.Website,
                     @Instagram = empresa.Instagram,
                     @Delivery = empresa.Delivery,
                     @Bairro = empresa.Bairro,
                     @Rua = empresa.Rua,
                     @Numero = empresa.Numero,
                     @Cep = empresa.Cep,
                     @Cidade = empresa.Cidade,
                     @Estado = empresa.Estado,
                     @Logo = empresa.Logo,
                     @Complemento = empresa.Complemento,
                     @IdUsuario = empresa.IdUsuario
                 });
        }

        public async Task<IEnumerable<ListarEmpresasConsulta>> ListaEmpresa()
        {
            return await _db.Connection.
                QueryAsync<ListarEmpresasConsulta>("select ID, NomeFantasia, Logo  From Empresa");
        }

        public async Task<ObterDetalheEmpresa> ObterDetalheEmpresa(int ID)
        {
            return await _db.Connection
                .QueryFirstOrDefaultAsync<ObterDetalheEmpresa>("SELECT * FROM EMPRESA WHERE ID=@ID", new
                { @ID = ID });

           
        }
        // Esse
        public async Task<int> ObterIdEmpresa(int IdUsuario)
        {
            return await _db.Connection
                 .ExecuteScalarAsync<int>("SELECT ID FROM EMPRESA WHERE IdUsuario=@IdUsuario", new
                 { @IdUsuario = IdUsuario });


        }

        public async Task<string> ObterNome(int ID)
        {
            return await _db.Connection
             .QueryFirstOrDefaultAsync<string>("SELECT NomeFantasia FROM EMPRESA WHERE ID=@ID", new
             { @ID = ID });
        }

        public async Task Salvar(Empresa empresa)
        {
           await _db.Connection
                .ExecuteAsync(" INSERT INTO EMPRESA (NomeFantasia, NomeResponsavel, Descricao, Seguimento, Documento, Email, Telefone, Bairro, Rua, Numero, Complemento, Cep, Cidade, Estado, Logo, Instagram, Facebook, Website, Horario, Delivery, IdUsuario) VALUES  (@NomeFantasia, @NomeResponsavel, @Descricao, @Seguimento, @Documento, @Email, @Telefone, @Bairro, @Rua, @Numero, @Complemento, @Cep, @Cidade, @Estado, @Logo, @Instagram, @Facebook, @Website, @Horario, @Delivery, @IdUsuario)",
                    new
                    {
                        @NomeFantasia = empresa.NomeFantasia,
                        @NomeResponsavel = empresa.NomeResponsavel,
                        @Descricao = empresa.Descricao,
                        @Seguimento = empresa.Seguimento,
                        @Documento = empresa.Documento.Cnpj,
                        @Email = empresa.Email,
                        @Telefone = empresa.Telefone,
                        @Bairro = empresa.Bairro,
                        @Rua = empresa.Rua,
                        @Numero = empresa.Numero,
                        @Complemento = empresa.Complemento,
                        @Cep = empresa.Cep,
                        @Cidade = empresa.Cidade,
                        @Estado = empresa.Estado,
                        @Logo = empresa.Logo,
                        @Instagram = empresa.Instagram,
                        @Facebook = empresa.Facebook,
                        @Website = empresa.Website,
                        @Horario = empresa.Horario,
                        @Delivery = empresa.Delivery,
                        @IdUsuario = empresa.IdUsuario
                    });
        }


    }
}
