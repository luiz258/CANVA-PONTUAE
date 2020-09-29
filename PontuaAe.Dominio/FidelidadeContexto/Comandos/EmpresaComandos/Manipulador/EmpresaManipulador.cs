using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Manipulador
{
    public class EmpresaManipulador : Notifiable,
        IComandoManipulador<_AddDadosEmpresaComando>,
        IComandoManipulador<EditarPerfilEmpresaComando>,
        IComandoManipulador<AddRegraProgramaFidelidadeComando>,
        IComandoManipulador<EditarRegraPontuacaoComando>

    {
        private readonly IEmpresaRepositorio _empresaRep;
        private readonly IConfigPontosRepositorio _configPontoRep;
        private readonly IConfiguracaoCashBackRepositorio _configCashBack;
        private readonly IUsuarioRepositorio _repUsuario;
        private readonly IContaSMSRepositorio _contaSMS;
        private readonly IConfigClassificacaoClienteRepositorio _repConfigClassificacaoCliente;



        public EmpresaManipulador(IEmpresaRepositorio empresaRep, IConfigPontosRepositorio cofigPontuacaoRep, IConfiguracaoCashBackRepositorio configCashBack, IUsuarioRepositorio repUsuario, IContaSMSRepositorio contaSMS, IConfigClassificacaoClienteRepositorio repConfigClassificacaoCliente)
        {
            _empresaRep = empresaRep;
            _configPontoRep = cofigPontuacaoRep;
            _configCashBack = configCashBack;
            _repUsuario = repUsuario;
            _contaSMS = contaSMS;
            _repConfigClassificacaoCliente = repConfigClassificacaoCliente;
        }

        public async Task<IComandoResultado> ManipularAsync(_AddDadosEmpresaComando comando)
        {

            if (await _empresaRep.ChecarDocumento(comando.Documento))
                AddNotification("CNPJ", "Este CNPJ está em uso");

            var documento = new Documento(comando.Documento);

            //Criando Conta Admin
            var usuarioAdmin = new Usuario(comando.Email, comando.Senha, comando.RoleId);
            await _repUsuario.Salvar(usuarioAdmin);
            Usuario usuarioAdmim = await _repUsuario.ObterUsuario(comando.Email);

            var empresa = new Empresa(usuarioAdmim.ID, comando.NomeFantasia, comando.Descricao, comando.NomeResponsavel, comando.Telefone, comando.Email, documento, comando.Seguimento, comando.Horario, comando.Facebook, comando.Website, comando.Instagram, comando.Delivery, comando.Bairro, comando.Rua, comando.Numero, comando.Cep, comando.Estado, comando.Complemento, comando.Logo, comando.Cidade);
            AddNotifications(documento.Notifications);

            if (Invalid)
                return new ComandoEmpresaResultado(false, "Por favor, corrija os campos abaixo", Notifications);

            await _empresaRep.Salvar(empresa);
            var _idEmpresa = await _empresaRep.ObterIdEmpresa(usuarioAdmim.ID);
     
            //Adicionar  creditos SMS na Empresa
            ContaSMS creditoSMS = new ContaSMS(_idEmpresa, 200 );
            await _contaSMS.Salvar(creditoSMS);

            //Configuração da Prgrama Fidelidade Basico
            var config = new ConfiguracaoPontos(_idEmpresa);
         

            // criar configuração para Computar o comportamento do tempo de visita e frequencia do cliente
            TemplateClassificacaoCliente criarConfiguracaoPadrao = new TemplateClassificacaoCliente(_idEmpresa);
             await _repConfigClassificacaoCliente.Salvar(criarConfiguracaoPadrao);

            // Salvar Configuração da Prgrama Fidelidade Basico
            await _configPontoRep.SalvaConfiguracaoPontuacao(config);
            return new ComandoEmpresaResultado(true, "Dados Salvos", Notifications);
            
        }

        public async Task<IComandoResultado> ManipularAsync(EditarPerfilEmpresaComando comando)
        {

          

            //Usuario usuarioAdmim = _repUsuario.OterUsuario(comando.Email);
            var empresa = new Empresa(comando.Id, comando.NomeFantasia, comando.Descricao, comando.NomeResponsavel, comando.Telefone, comando.Email, comando.Seguimento, comando.Horario, comando.Facebook, comando.Website, comando.Instagram, comando.Delivery, comando.Bairro, comando.Rua, comando.Numero, comando.Cep, comando.Estado, comando.Complemento, comando.Logo, comando.Cidade);


            if (Invalid)
                return new ComandoEmpresaResultado(false, "Por favor, corrija os campos incorretos", Notifications);

            await _empresaRep.Editar(empresa);

            return new ComandoEmpresaResultado(true, "Dados Atualizado com sucesso ", Notifications);

        }



        public async Task<IComandoResultado> ManipularAsync(AddRegraProgramaFidelidadeComando comando)
        {
            ////1 : CashBack
            //if (comando.TipoProgramaFidelidade == 1)
            //{
            //    var configCasBack = new ConfiguracaoCashBack();
            //    await _configCashBack.Salvar(configCasBack);
            //    return new ComandoEmpresaResultado(true, "Salvo", Notifications);

            //}
            ////2 : Pontos
            ///
            var verificarConfig = await _configPontoRep.ObterdadosConfiguracao(comando.IdEmpresa);

            if(verificarConfig.Nome != null)
            {
                return new ComandoEmpresaResultado(true, "Já existe uma configuração", Notifications);
            }
          


            //criação da regra da pontuação
            var configuracaoPontos = new ConfiguracaoPontos(comando.Nome, comando.IdEmpresa, comando.PontosFidelidade, comando.Reais, comando.ValidadePontos, 0, comando.TipoDeProgramaFidelidade);
                await _configPontoRep.SalvaConfiguracaoPontuacao(configuracaoPontos);
            
            return new ComandoEmpresaResultado(true, "Dados Salvos", Notifications);

        }

        public async Task<IComandoResultado> ManipularAsync(EditarRegraPontuacaoComando comando)
        {
           


            //1 : CashBack
            if (comando.TipoProgramaFidelidade == 1)
            {
                var configCasBack = new ConfiguracaoPontos(comando.Nome, comando.IdEmpresa, comando.Reais, comando.TipoProgramaFidelidade);
                await _configPontoRep.EditarConfiguracaoPontuacao(configCasBack);  
                return new ComandoEmpresaResultado(true, "OK", Notifications);

            }

            //2 : Pontos
         
                //criação da regra da pontuação
                var configuracaoPontos = new ConfiguracaoPontos(comando.Nome, comando.IdEmpresa, comando.PontosFidelidade, comando.Reais, comando.ValidadePontos, 0, comando.TipoProgramaFidelidade);
                await _configPontoRep.EditarConfiguracaoPontuacao(configuracaoPontos);
            
            return new ComandoEmpresaResultado(true, "cartão fidelidade criado com sucesso", Notifications);


        }

        
    }
}