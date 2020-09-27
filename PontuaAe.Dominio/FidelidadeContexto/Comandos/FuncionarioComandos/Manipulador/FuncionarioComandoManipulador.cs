using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Domain.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.FuncionarioComandos.Manipulador
{
    public class FuncionarioComandoManipulador : Notifiable,
        IComandoManipulador<CadastrarFuncionarioComando>,
        IComandoManipulador<EditarFuncionarioComando>,
        IComandoManipulador<RemoverColaboradorComando>
    {
        private readonly IFuncionarioRepositorio _repFuncionario;        
        private readonly IUsuarioRepositorio _repUsuario;
  

        public FuncionarioComandoManipulador(IFuncionarioRepositorio repFuncionario, IUsuarioRepositorio repUsuario )
        {
            _repFuncionario = repFuncionario;
            _repUsuario = repUsuario;

        }

        public async Task<IComandoResultado> ManipularAsync(CadastrarFuncionarioComando comando)
        {
            // Verificar se o E-mail e valido
            var EmailValido = new Email(comando.Email);
            AddNotifications(EmailValido.Notifications);

            //// Verificar se o E-mail já existe na base
            if (await _repUsuario.ValidaEmail(comando.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            var RoleId = comando.ControleUsuario == 2 ? "Funcionario" : "Administrador";

            var usuario = new Usuario(comando.Email, comando.Senha, RoleId);
            if (Invalid)
                return new ComandoFuncionarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            await _repUsuario.Salvar(usuario);

            var _usuario = await _repUsuario.ObterUsuario(comando.Email);
            
            var fucionario = new Funcionario(_usuario.ID, comando.IdEmpresa, comando.NomeCompleto, comando.Contato);

            if (Invalid)
                return new ComandoFuncionarioResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            await _repFuncionario.Salvar(fucionario);

            //Retorna true
            return new ComandoFuncionarioResultado(true, "Cadastrado com sucesso", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(EditarFuncionarioComando comando)
        {
            // Verificar se o E-mail e valido
            var EmailValido = new Email(comando.Email);
            AddNotifications(EmailValido.Notifications);

            var RoleId = comando.ControleUsuario == 2 ? "Funcionario" : "Administrador";

            var usuario = new Usuario(comando.Email, RoleId);
            if (Invalid)
                return new ComandoFuncionarioResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            await _repUsuario.Editar(usuario);
            

            var fucionario = new Funcionario(comando.Id, comando.IdEmpresa, comando.NomeCompleto, comando.Contato);

            if (Invalid)
                return new ComandoFuncionarioResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            await _repFuncionario.Editar(fucionario);

            //Retorna true
            return new ComandoFuncionarioResultado(true, "Alteração feita com sucesso", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(RemoverColaboradorComando comando)
        {
            var ID = comando.ID;
            var IdEmpresa_ = comando.IdEmpresa;
            //int _ID = await _repFuncionario.ObterId(ID); 
            await _repFuncionario.Deletar(ID, IdEmpresa_);
            await _repUsuario.Deletar(ID);

            return new ComandoFuncionarioResultado(true, "Removido com sucesso", Notifications);
        }
    }
}