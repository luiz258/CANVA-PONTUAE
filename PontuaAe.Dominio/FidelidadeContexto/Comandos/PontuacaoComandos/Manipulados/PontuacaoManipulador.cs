using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.PontuacaoComandos.Manipulador
{
    public class PontuacaoManipulador : Notifiable,
        IComandoManipulador<PontuarClienteComando>,
        IComandoManipulador<ResgatarPontosComando>,
        IComandoManipulador<ResgatarCashBackComando>
    {
        private readonly IConfigPontosRepositorio _configPontosRepositorio;
        private readonly IPontuacaoRepositorio _pontuacaoRepositorio;
        private readonly IReceitaRepositorio _receitaRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        private readonly IPreCadastroRepositorio _preCadastroRepositorio;
        private readonly IConfiguracaoCashBackRepositorio _configCashBackRepositorio;

        public PontuacaoManipulador(
            IClienteRepositorio clienteRepositorio,
            IPontuacaoRepositorio pontuacaoRepositorio,
            IReceitaRepositorio receitaRepositorio,
            IPremioRepositorio premiosRepositorio,
            IConfigPontosRepositorio configPontosRepositorio,
            IAutomacaoMSGRepositorio automacaoRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            ISituacaoRepositorio situacaoRepositorio,
            IEnviarSMS enviarSMS,
            IPreCadastroRepositorio preCadastroRepositorio,
            IConfiguracaoCashBackRepositorio configCashBackRepositorio
)
        {

            _pontuacaoRepositorio = pontuacaoRepositorio;
            _receitaRepositorio = receitaRepositorio;
            _configPontosRepositorio = configPontosRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _situacaoRepositorio = situacaoRepositorio;
            _enviarSMS = enviarSMS;
            _preCadastroRepositorio = preCadastroRepositorio;
            _configCashBackRepositorio = configCashBackRepositorio;
        }

        public async Task<IComandoResultado> ManipularAsync(PontuarClienteComando comando)
        {
            try
            {
                var nomeEmpresa = await _empresaRepositorio.ObterNome(comando.IdEmpresa);

                //os bloco de if  são para  atualiza o saldo de ponto da PONTUAÇAO ou para cria uma nova pontuação  para o contato
                var VerificaIdPrecadastro = await _pontuacaoRepositorio.ChecarClienteNaBasePontuacao(comando.IdPreCadastro, comando.IdEmpresa);

                //se a conta cashback estive Ativada
                var tipoConfig = await _configCashBackRepositorio.ChecarConfigCashBack();
                //----------VERIFICAÇÃO POR CASHBACK----------------

                if (tipoConfig == 1)
                {

                    // Busca configuraçãoCasback
                    var config = await _configPontosRepositorio.ObterdadosConfiguracao(comando.IdEmpresa);

                    if (VerificaIdPrecadastro == true)
                    {
                        Pontuacao validador = new Pontuacao(comando.IdPreCadastro, comando.IdEmpresa);

                        var pontuacaoAnterior = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);

                        validador.PontuarPorCashBack(config.Percentual, comando.ValorInfor, pontuacaoAnterior);

                        await _pontuacaoRepositorio.AtualizarSaldo(validador);

                    }
                    else if (tipoConfig == 2)
                    {
                        Pontuacao geraPontuacaoSaldoZero = new Pontuacao(0, comando.IdEmpresa, comando.IdPreCadastro);
                        await _pontuacaoRepositorio.CriarPontuacao(geraPontuacaoSaldoZero);

                        //envia sms boas vindas ao programa de fidelidade

                        var n = comando.Contato;
                        string _numero = n;
                        await _enviarSMS.EnviarPorLocaSMSAsync(_numero, $"{nomeEmpresa} : Parabens! voce Faz parte do nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja seus pontos");

                        Pontuacao validador = new Pontuacao(comando.IdPreCadastro, comando.IdEmpresa);

                        var SaldoAnterior = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);

                        validador.PontuarPorCashBack(config.Percentual, comando.ValorInfor, SaldoAnterior);

                        await _pontuacaoRepositorio.AtualizarSaldo(validador);

                    }

                }

                else
                {

                    //----------VERIFICAÇÃO POR PONTUACAO----------------

                    var configPontuacao = await _configPontosRepositorio.ObterdadosConfiguracao(comando.IdEmpresa);

                    if (comando.ValorInfor < configPontuacao.Reais)

                        return new ComandoResultado(false, $" O valor minimo para pontua é  " + $"{ configPontuacao.Reais }", null);


                    if (VerificaIdPrecadastro == true)
                    {
                        Pontuacao validador = new Pontuacao(comando.IdPreCadastro, comando.IdEmpresa);

                        var pontuacaoAnterior = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);

                        validador.Pontuar(comando.ValorInfor, configPontuacao.PontosFidelidade, configPontuacao.Reais, pontuacaoAnterior);

                        await _pontuacaoRepositorio.AtualizarSaldo(validador);

                    }

                    else if (VerificaIdPrecadastro == false)
                    {
                        Pontuacao geraPontuacaoSaldoZero = new Pontuacao(0, comando.IdEmpresa, comando.IdPreCadastro);
                        await _pontuacaoRepositorio.CriarPontuacao(geraPontuacaoSaldoZero);

                        //envia sms boas vindas ao programa de fidelidade

                        var n = comando.Contato;
                        string _numero = n;
                        await _enviarSMS.EnviarPorLocaSMSAsync(_numero, $"{nomeEmpresa} : Parabens! voce Faz parte do nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja seus pontos");

                        Pontuacao validador = new Pontuacao(comando.IdPreCadastro, comando.IdEmpresa);

                        var SaldoAnterior = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);

                        validador.Pontuar(comando.ValorInfor, configPontuacao.PontosFidelidade, configPontuacao.Reais, SaldoAnterior);

                        await _pontuacaoRepositorio.AtualizarSaldo(validador);

                    }
                }


                //obter ID da Pontuação por parametro IdPreCadastro E IdEmpresa no momento da operação
                var Id = await _pontuacaoRepositorio.ObterIdPontuacao(comando.IdEmpresa, comando.IdPreCadastro);

                //RECEITA 
                Receita DadosReceita = new Receita(comando.ValorInfor, comando.IdEmpresa, Id, comando.Id, "pontuacao");
                await _receitaRepositorio.Salvar(DadosReceita);
                //Notificação Saldo de Pontos

                var numero = comando.Contato;
                var idPreCadastro = await _preCadastroRepositorio.ObterIdPreCadastro(comando.Contato);
                decimal saldoCliente = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, idPreCadastro);
                int saldo = Convert.ToInt32(saldoCliente);

                await _enviarSMS.EnviarPorLocaSMSAsync(numero, $"{nomeEmpresa}: Voce Acumulou {saldo} pontos em nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja como funciona e os premios que pode resgatar");

                //Este bloco abaixo, averigua se sera necessário
                //var _saldoCliente =  _clienteRepositorio.ObterSaldo(IdCliente, comando.IdEmpresa);        
                //IList<Premios> ListPremiosDisponiveis = _premiosRepositorio.PremiosDisponiveis(_saldoCliente.Saldo);

                //Notificação Prêmios disponiveis
                // if(ListPremiosDisponiveis != null)
                // {
                //    _enviarSMS.EnviarAsync(contato, $"{nomeEmpresa}:voce ja pode resgatar: premios em nosso programa de fidelidade. Acesse https://pontuaae.com.br e veja o que pode resgatar");
                //}


                IEnumerable<SituacaoSMS> ListaSituacao;
                ListaSituacao = await _situacaoRepositorio.ListaSituacaoSMS(comando.IdEmpresa);
                foreach (var i in ListaSituacao)
                {
                    if (i.Contatos == comando.Contato)
                    {
                        var dataRetorno = DateTime.Now;
                        var dadosSituacao = new SituacaoSMS(comando.ValorInfor, dataRetorno, i.Contatos, comando.IdEmpresa, i.IdMensagem, i.IdSMS);
                        await _situacaoRepositorio.SalvarSituacao(dadosSituacao);

                    }
                }

                return new ComandoResultado(true, "A pontuação foi registrada com sucesso ", Notifications);
            }
            catch (Exception e)
            {

                throw;
            }

         

        }


        public async Task<IComandoResultado> ManipularAsync(ResgatarPontosComando comando)
        {

            var configPontuacao = await _configPontosRepositorio.ObterdadosConfiguracao(comando.IdEmpresa);
            decimal obterSaldo = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);

            if (obterSaldo < comando.ValorDoPremio )
                return new ComandoResultado(false, "Saldo de pontos menor do que os pontos necessário ", Notifications);

            if (obterSaldo <= 0)
                return new ComandoResultado(false, "Erro ao efetuar o resgate de prêmio", Notifications);

            var resgatar = new Pontuacao(obterSaldo, comando.IdEmpresa, comando.IdPreCadastro);
            resgatar.Resgatar(comando.ValorDoPremio);
            await _pontuacaoRepositorio.AtualizarSaldo(resgatar);

            RegistroResgate DadosDoResgate = new RegistroResgate(comando.IdPreCadastro, comando.IdEmpresa, comando.IdUsuario, comando.ValorDoPremio);
            await _pontuacaoRepositorio.RegistraResgate(DadosDoResgate);

            return new ComandoResultado(true, "Resgate efetuado com sucesso. Pode entregar o prêmio ao cliente", Notifications);

        }

        public async Task<IComandoResultado> ManipularAsync(ResgatarCashBackComando comando)
        {
            decimal obterSaldo = await _pontuacaoRepositorio.obterSaldo(comando.IdEmpresa, comando.IdPreCadastro);
          
            var resgatar = new Pontuacao(obterSaldo, comando.IdEmpresa, comando.IdPreCadastro);
            resgatar.DebitarCashBack(comando.Valor);
            await _pontuacaoRepositorio.AtualizarSaldo(resgatar);
            return new ComandoResultado(true, "Transação fetuada com sucesso.", Notifications);
        }
    }
}
