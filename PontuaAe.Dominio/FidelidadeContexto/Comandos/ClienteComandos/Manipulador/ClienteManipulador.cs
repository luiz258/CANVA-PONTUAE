using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.ClienteComandos.Manipulador
{
    public class ClienteManipulador : Notifiable,
        IComandoManipulador<PreCadastroClienteComando>,
        IComandoManipulador<EditarClienteComando>,
        IComandoManipulador<RemoverClienteComando>
      
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IConfigPontosRepositorio _repConfigPtns;
        private readonly IUsuarioRepositorio _usuarioRepsitorio;
        private readonly IPontuacaoRepositorio _pontuacaoRepositorio;
        private readonly IReceitaRepositorio _receitaRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IConfigClassificacaoClienteRepositorio _repConfigClassificacaoCliente;

        public ClienteManipulador(
            IClienteRepositorio clienteRepositorio,
            IConfigPontosRepositorio repConfigPtns,
            IPontuacaoRepositorio pontuacaoRepositorio,
            IReceitaRepositorio receitaRepositorio,
            IEnviarSMS enviarSMS,
            IEmpresaRepositorio empresaRepositorio,
            IUsuarioRepositorio usuarioRepsitorio,
            IConfigClassificacaoClienteRepositorio repConfigClassificacaoCliente
            )
        {
            _repConfigPtns = repConfigPtns;
            _clienteRepositorio = clienteRepositorio;
            _pontuacaoRepositorio = pontuacaoRepositorio;
            _receitaRepositorio = receitaRepositorio;
            _enviarSMS = enviarSMS;
            _empresaRepositorio = empresaRepositorio;
            _usuarioRepsitorio = usuarioRepsitorio;
            _repConfigClassificacaoCliente = repConfigClassificacaoCliente;
    }

        public async Task<IComandoResultado> ManipularAsync(PreCadastroClienteComando comando)
        {
                     //excluir esse bloco comentato
            //if (_clienteRepositorio.ChecarCliente(comando.Telefone))
            //     return new ComandoClienteResultado(false, "Telefone já cadastrado", Notifications);

            //var PreCadastrado = new Cliente(comando.Telefone);
            //AddNotifications(PreCadastrado.Notifications);

            //if (Invalid)
            //    return new ComandoClienteResultado(false, "Por favor, corrija ", Notifications);

            //_clienteRepositorio.SalvarPreCadastro(PreCadastrado);
            
            //int _ObterIdCliente = _clienteRepositorio.ObterIDCliente(comando.Telefone);

            var contaPontuacao = await _repConfigPtns.ObterdadosConfiguracao(comando.IdEmpresa);

            //Pontuacao geraPontuacaoSaldoZero = new Pontuacao(0, comando.IdEmpresa, _ObterIdCliente, contaPontuacao.ValidadePontos );
            //_pontuacaoRepositorio.CriarPontuacao(geraPontuacaoSaldoZero);
        
            ////envia sms boas vindas ao programa de fidelidade
            //var nomeEmpresa = _empresaRepositorio.ObterNome(comando.IdEmpresa).ToString();
            //var numero = comando.Telefone;
            //string _telefone = numero;
            //_enviarSMS.EnviarAsync(_telefone, $"{nomeEmpresa}: Parabens! voce Faz parte do nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja seus pontos");

            return new ComandoClienteResultado(true, "Seja bem vindo", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(CadastroClienteComando comando)
        {

            if ( await _clienteRepositorio.ChecarTelefone(comando.Contato))
                AddNotification("Telefone", "Este Telefone já está em uso");

            // Verificar se o E-mail e valido
            var EmailValido = new Email(comando.Email);
            AddNotifications(EmailValido.Notifications);

            //Verificar se o E-mail já existe na base
            if (await _usuarioRepsitorio.ValidaEmail(comando.Email))
                AddNotification("Email", "Este E-mail já está em uso");
            if (Invalid)
                return new ComandoClienteResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            var usuario = new Usuario(comando.Email, comando.Senha, comando.RoleId);
            if (Invalid)
                return new ComandoClienteResultado(
                    false,
                    "Por favor, corrija os campos abaixo",
                    Notifications);

            await _usuarioRepsitorio.Salvar(usuario);


            var _usuario = await _usuarioRepsitorio.ObterUsuario(comando.Email);
            var PerfilUsuario = new Cliente(_usuario.ID, comando.Nome, comando.Contato, EmailValido,  comando.DataNascimento, comando.Cidade, comando.Sexo);

            if (Invalid)
                return new ComandoClienteResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            await _clienteRepositorio.Salvar(PerfilUsuario);
            return new ComandoClienteResultado(true, "Salvo com sucesso", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(EditarClienteComando comando)
        {
            //var dataNascimento = DateTime.Parse(comando.DataNascimento);

            var PerfilUsuario = new Cliente(comando.IdUsuario, comando.NomeCompleto, comando.DataNascimento, comando.Cidade, comando.Contato, comando.Sexo);

            if (Invalid)
                return new ComandoClienteResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            await _clienteRepositorio.Editar(PerfilUsuario);
            return new ComandoClienteResultado(true, "Salvo com sucesso", Notifications);
        }

        
        public async Task ClassificaRecorrencia()
        {
            IEnumerable<Pontuacao> _ListClassificao;
            _ListClassificao = await _pontuacaoRepositorio.ObterClassificacaoCliente();

            foreach (var item in _ListClassificao)     
            {

                //criar  um metodo para busca a  variavel de tempo de visita (1= 30 dias, 2 = 60 dias, 3= 90 ...) e passa como parametro nos metodos QtdVisitasClassificacaoOuro ....
                
                Pontuacao ClassificaTipoCliente = new Pontuacao();
                ConsultaTemplateClassificacaoCliente dado = await _repConfigClassificacaoCliente.ObterConfig(item.IdEmpresa);
                var agrupamentoEmOuro = await _receitaRepositorio.ObterQtdDiasAusenteClassificacaoOuro( item.IdEmpresa, item.ID, dado.TempoEmDiasClienteOuro);  
                var agrupamentoEmPrata = await _receitaRepositorio.ObterQtdDiasAusenteClassificacaoPrata( item.IdEmpresa, item.ID, dado.TempoEmDiasClientePrata);
                var agrupamentoEmBronze = await _receitaRepositorio.ObterQtdDiasAusenteClassificacaoBronze( item.IdEmpresa, item.ID, dado.TempoEmDiasClienteBronze);

                ClassificaTipoCliente.SeguimentarCliente(item.DataVisita, dado.TempoEmDiasClienteOuro, dado.TempoEmDiasClientePrata, dado.TempoEmDiasClienteBronze,
                dado.QtdVisitasClassificacaoOuro, dado.QtdVisitasClassificacaoPrata, dado.QtdVisitasClassificacaoBronze, agrupamentoEmOuro, agrupamentoEmPrata, agrupamentoEmBronze, item.ID, item.IdEmpresa);  
                await _clienteRepositorio.EditarClassificacaoCliente(ClassificaTipoCliente);   
            }
        }

        public void ClassificaClientesNaoFrequentes()
        {
            //FALTA IMPLEMENTA A CLASSIFICAÇÃO DE TIPO DE CLIENTES PERDIDOS, INATIVOS
        }

        public async Task<IComandoResultado> ManipularAsync(RemoverClienteComando comando)
        {
           // _clienteRepositorio.Deletar(comando.ID);
            return new ComandoClienteResultado(true, "Removido", Notifications);
        }

       
    }
}

