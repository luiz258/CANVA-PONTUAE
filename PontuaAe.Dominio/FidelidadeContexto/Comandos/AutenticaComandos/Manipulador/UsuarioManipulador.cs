using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador
{
    public class UsuarioManipulador : Notifiable,
        //IComandoManipulador<AutenticarUsuarioComando>,  verifica se vai usar este manipulador se ocorrer tudo bem então remover ele
        IComandoManipulador<RemoveUsuarioComando>,
        IComandoManipulador<AddContaFuncionarioComando>,
        //IComandoManipulador<EditarContaFuncionarioComando>, VERIFICA SE PODE DELETA
        IComandoManipulador<UsuarioComando>

    //IComandoManipulador<CadastroClienteComando>,
    //IComandoManipulador<CadastroEmpresaComando>,

    {
        private readonly IEmpresaRepositorio _empresaRep;
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioManipulador(IUsuarioRepositorio repositorio, IEmpresaRepositorio empresaRep)
        {

            _repositorio = repositorio;
            _empresaRep = empresaRep;
        }

        //public IComandoResultado Manipular(AutenticarUsuarioComando comando)
        //{
        //    Email email = new Email(comando.Email);
        //    Usuario login = new Usuario(email.Endereco, comando.Senha);
        //    //AddNotifications(email);
        //    AddNotifications(login);
        //    if (Invalid)
        //        return new ComandoUsuarioResultado(false, "Por favor, corrija os campos", Notifications);
        //    if (_repositorio.Autenticar(login.Email, login.Senha))
        //        AddNotification("Error", "Usuário não encontrado!");

        //    AddNotifications(login);

        //    return new ComandoUsuarioResultado(true);
        //}
        public async Task<IComandoResultado> ManipularAsync(AddContaFuncionarioComando comando)
        {
            if (await _repositorio.ValidaEmail(comando.Email))
                AddNotification("Email", "Este Email já está em uso");
            var email = new Email(comando.Email);
            var usuario = new Usuario( email.Endereco, comando.Senha, comando.RoleId);
            if (Invalid)
                return new ComandoUsuarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

           await _repositorio.Salvar(usuario);
            return new ComandoUsuarioResultado(true, "", Notifications);
        }

        
        //public IComandoResultado Manipular(EditarContaFuncionarioComando comando)
        //{
        //    if (_repositorio.ValidaEmail(comando.Email))
        //        AddNotification("Email", "Este Email já está em uso");
        //    var email = new Email(comando.Email);
        //     var usuario = new Usuario(comando.NomeCompleto,  email.Endereco, comando.Senha, comando.ClaimType, comando.ClaimValue);
        //    if (Invalid)
        //        return new ComandoUsuarioResultado(
        //            false,
        //            "Por favor, corrija os campos abaixo",
        //            Notifications);

        //    _repositorio.Editar(usuario);
        //    return new ComandoUsuarioResultado(true, "", Notifications);
        //}


        public async Task<IComandoResultado> ManipularAsync(RemoveUsuarioComando comando)
        {
          await  _repositorio.Deletar(comando.IdUsuario);
            return new ComandoUsuarioResultado(true, "Removido", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(UsuarioComando comando)
        {
            var usuario = new Usuario(comando.Email, comando.Senha);
            await  _repositorio.ResetaSenha(comando.Senha, comando.ID);

            return new ComandoUsuarioResultado(true, "Senha Alterada", Notifications);
        }

   
    }
}
