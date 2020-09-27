using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PremioComandos.Manipulador
{
    public class PremioManipulador : Notifiable,
        IComandoManipulador<CadastraPremioComando>,
        IComandoManipulador<DeletarPremioComando>,
        IComandoManipulador<EditarPremioComando>
    {
        private readonly IPremioRepositorio _premioRepositorio;
        private readonly IEmpresaRepositorio _Empresarepositorio;

        public PremioManipulador(IPremioRepositorio premioRepositorio, IEmpresaRepositorio empresarepositorio)
        {
            _premioRepositorio = premioRepositorio;
            _Empresarepositorio = empresarepositorio;
        }

        public async Task<IComandoResultado> ManipularAsync(CadastraPremioComando comando)
        {

            Descricao descricao = new Descricao(comando.Descricao);
            Premios premio = new Premios( comando.IdEmpresa, comando.Nome, descricao, comando.QtdPontos);

            AddNotifications(descricao.Notifications);
            if (Invalid)
                return new ComandoResultado(false, "Descrição deve conter, 10 a 250 caracteres ", Notifications);

           await  _premioRepositorio.Salvar(premio);

            return new ComandoResultado(true, "Premio Cadastrado com sucesso", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(DeletarPremioComando comando)
        {
            await _premioRepositorio.Deletar(comando.Id, comando.IdEmpresa);
            return new ComandoResultado(true, "Prêmio Deletado!", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(EditarPremioComando comando)
        {
            Descricao descricao = new Descricao(comando.Texto);
            Premios premio = new Premios(comando.ID  , comando.IdEmpresa, comando.Title, descricao, comando.QtdPontos);


            await _premioRepositorio.Editar(premio);
            return new ComandoResultado(true, "Salvo com sucesso!", Notifications);

        }

    }
}
