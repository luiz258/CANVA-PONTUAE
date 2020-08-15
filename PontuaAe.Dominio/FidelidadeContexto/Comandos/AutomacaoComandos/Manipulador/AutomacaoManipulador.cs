using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.AutomacaoComandos.Manipulador
{
    public class AutomacaoManipulador : Notifiable,
        IComandoManipulador<AddAutomacaoComando>,
        IComandoManipulador<EditarAutomacaoComando>,
        IComandoManipulador<RemoverAutomacaoComando>,
        IComandoManipulador<DesativarAutomacao>
    {
        private readonly IAutomacaoMSGRepositorio _automacaoRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        string[] numero;
        //string[] ContatosAniversarianteMesmoDia;  remover
        //string[] ContatosAniversariantesDiasDesiguais;  remover
        string[] arrayContatos;

        public AutomacaoManipulador(
             IAutomacaoMSGRepositorio automacaoRepositorio,
             IEmpresaRepositorio empresaRepositorio,
             IClienteRepositorio clienteRepositorio,
             ISituacaoRepositorio situacaoRepositorio,
             IEnviarSMS enviarSMS


            )
        {
            _automacaoRepositorio = automacaoRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _situacaoRepositorio = situacaoRepositorio;
            _enviarSMS = enviarSMS;
        }

        public async Task<IComandoResultado> ManipularAsync(EditarAutomacaoComando comando)
        {

            var automacao = new Mensagem(
                comando.ID,
                comando.IdEmpresa,
                comando.TipoAutomacao,
                comando.TempoPorDiaDaSemana,
                comando.DiasAntesAniversario,
                comando.TempoPorDiaDoMes,
                comando.Conteudo,
                comando.Segmentacao,
                comando.SegCustomizado,
                comando.TempoPorDia);

            await _automacaoRepositorio.Editar(automacao);

            return new ComandoResultado(true, "OK", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(AddAutomacaoComando comando)
        {

            var automacao = new Mensagem(
                comando.IdEmpresa,
                comando.TipoAutomacao,
                comando.TempoPorDiaDaSemana,
                comando.DiasAntesAniversario,
                comando.TempoPorDiaDoMes,
                comando.Conteudo,
                comando.Segmentacao,
                comando.SegCustomizado,
                comando.TempoPorDia);

            await _automacaoRepositorio.Salvar(automacao);
            return new ComandoResultado(true, "Salvo com sucesso", Notifications);
        }

        public async Task<IComandoResultado> ManipularAsync(RemoverAutomacaoComando comando)
        {
           await _automacaoRepositorio.Deletar(comando.ID, comando.IdEmpresa);
            return new ComandoResultado(true, "Colaborador Removido ", Notifications);
        }



        public async Task<IComandoResultado> ManipularAsync(DesativarAutomacao comando)
        {
            await _automacaoRepositorio.Desativar(comando.IdEmpresa, comando.Id);
            return new ComandoResultado(true, "Desativada", Notifications);
        }


        //----------------------------------------------------------------------------------------------------------------
        // Metodos que sera executados por na Pasta Servico API/GereciamentoJobsTask
        //----------------------------------------------------------------------------------------------------------------

        //-----1  ANIVERSÁRIO TipoAutomacaoAniversario
        public async Task AutomacaoTipoAniversarianteAsync()
        {
            List<string> ContatosAniversariantesDiasDesiguais = new List<string>();
            List<string> ContatosAniversarianteMesmoDia = new List<string>();

            List<ObterAutomacaoTipoAniversario> listaContatos = new List<ObterAutomacaoTipoAniversario>();
            IEnumerable<Mensagem> Lista = _automacaoRepositorio.ListaTipoAutomacao();

            foreach (var linha in Lista)
            {

                if (linha.TipoAutomacao == "Aniversariante" && linha.Estado == true)
                {

                    IEnumerable<ObterAutomacaoTipoAniversario> listDadosAutomacaoAniversario;
                    listDadosAutomacaoAniversario = _automacaoRepositorio.ObterDadosAutomacaoAniversario("Aniversariante", linha.Segmentacao, linha.SegCustomizado, linha.IdEmpresa);

                    foreach (var item in listDadosAutomacaoAniversario)


                    {
                        listaContatos.Add(new ObterAutomacaoTipoAniversario { DataNascimeto = item.DataNascimeto, Contato = item.Contato, Conteudo = item.Conteudo });
                    }

                    foreach (var l in listDadosAutomacaoAniversario)
                    {
                        foreach (var i in listaContatos)

                        {
                            //ESTA LOGICA ESTÁ ERRADA,    
                            ///CRIAR UM ALGORITMO PARA PEGA A DATANASCIMENTO E VERIFICA  NO CALENDARIO DO MES ATUAL  AS DATAS QUE BATE INGUAL  CRIAR O ARRAY DE CONTATO
                            ///CRIAR UM ARRAY PARA CADA GRUPO DE CONTATOS QUE FAZ ANIVERSARIO NO DIA/MES ESPECIFICO,
                            /// SE DIA 10/06 = 10/06 :  ADD contato  
                            /// não vai precisa mais desse array  listaContatos
                     //Lembra de averiguar      //var ddd = i.DataNascimeto == l.DataNascimeto ? ContatosAniversarianteMesmoDia.Add( l.Contato ) : ContatosAniversariantesDiasDesiguais.Add(l.Contato);

                        }

                        string horaEnvio = "";

                        string dataEnvio = "";

                        if (l.DiasAntesAniversario != 0)
                        {

                            //int diasAntes = l.DiasAntesAniversario;
                            var data1 = l.DataNascimeto.Remove(0, 2);
                            var caracter = l.DiasAntesAniversario.ToString();
                            dataEnvio = data1.Insert(0, $"{caracter}");
                            // String data = l.DataNascimeto.AddDays(-diasAntes);
                            //string dataEnvio = data.ToString("dd-MM-yyyy");
                            horaEnvio = "11:30";
                        }

                        if (l.DiasAntesAniversario == 0)
                        {
                            dataEnvio = l.DataNascimeto;
                            horaEnvio = "11:00";
                        }


                        if (ContatosAniversariantesDiasDesiguais != null)
                        {
                            //nesta linha vai vim um array
                            var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(ContatosAniversariantesDiasDesiguais, l.Conteudo, dataEnvio, horaEnvio);
                            Agenda agendar = new Agenda(dataEnvio, horaEnvio);
                            //aqui criar um foreach para pecorrer o array de codigo cada codigo vai ser 
                            Mensagem campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agendar);

                            campanhaSMS.CalcularQtdEnviado(ContatosAniversariantesDiasDesiguais.Count);
                            await _automacaoRepositorio.Editar(campanhaSMS);

                            foreach (var c in arrayDeCodigoDasMensagens)
                            {
                                var array = c.Split(',');
                                var contato = array[0];
                                var idSMS = Convert.ToInt32(array[1]);
                                var _dataEnvio = DateTime.Now;
                                var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, l.IdEmpresa, l.ID, idSMS);
                                await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                            }
                        }

                        if (ContatosAniversarianteMesmoDia != null)
                        {
                            //Contatos Agrupados
                            var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(ContatosAniversarianteMesmoDia, l.Conteudo, dataEnvio, horaEnvio);
                            Agenda agendar = new Agenda(dataEnvio, horaEnvio);
                            //aqui criar um foreache para pecorrer o array de codigo cada codigo vai ser 
                            Mensagem campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agendar);

                            campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
                            await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                            foreach (var c in arrayDeCodigoDasMensagens)
                            {
                                var array = c.Split(',');
                                var contato = array[0];
                                var idSMS = Convert.ToInt32(array[1]);
                                var _dataEnvio = DateTime.Now;
                                var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, linha.IdEmpresa, l.ID, idSMS);
                                await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                            }
                        }

                    }

                }
            }
        }

        //-----2  EM UM DIA DA SEMANA  EXECUTAR O SHEDULE AS 04:00HS; TODO DIA VERIFICA  E AGENDA O ENVIO PARA AS 08:00HS
        public async Task AutomacaoTipoDiaDaSemana()
        {
            List<string> numero = new List<string>();
            IEnumerable<Mensagem> Lista = _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in Lista)
            {
                if (l.TipoAutomacao == "Dia da Semana" && l.Estado == true)
                {

                    //CRIAR UM IF PRA VERIFICA   SO PASSA SE O Tipo de automação for ingual a dia da semana

                    IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao;
                    listaDadosAutomacao = _automacaoRepositorio.ObterDadosAutomacaoSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                    //string[] Contato =  _automacaoRepositorio.ListaTelefones(l.IdEmpresa, l.SegCustomizado, l.Segmentacao);
                    //int QtdContatos = Contato.Length;

                    foreach (var cc in listaDadosAutomacao)
                    {

                        //string[] Contatoo = new string[] { }; 
                        numero.Add(cc.Contato);

                    }

                    foreach (var linha in listaDadosAutomacao)
                    {

                        var aa = DateTime.Now.DayOfWeek.ToString();
                        if (linha.TempoPorDiaDaSemana == aa) //OBS SE NÃO FUNCIONAR, ENTÃO IMPLEMENTA DateTime.Now.DayOfWeek PARA OBTER O DIA DA SEMANA NA DATA ATUAL NA VARIAVEL Dia
                        {
                            string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                            //var data = dataEnvio.ToShortDateString();
                            string hora = "11:47";

                            var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(numero, linha.Conteudo, dataEnvio, hora);
                            //resolver esta parte abaixo
                            Agenda agenda = new Agenda(dataEnvio, hora);
                            var campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agenda);
                            campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
                            await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                            foreach (var c in arrayDeCodigoDasMensagens)
                            {
                                var array = c.Split(',');
                                var contato = array[0];
                                var idSMS = Convert.ToInt32(array[1]);
                                var _dataEnvio = DateTime.Now;
                                var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, l.IdEmpresa, l.ID, idSMS);
                                await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                            }

                        }
                    }
                }
            }
        }


        ////-----3 APOS  TRINTA DIAS SEM RETORNO, EXECUTAR O SHEDULE AS 03:30HS; TODO DIA VERIFICA  E AGENDA O ENVIO PARA AS 10:15HS
        public async Task AutomacaoTipoTrintaDias()
        {
            List<string> arrayContatos = new List<string>();
            IEnumerable<Mensagem> ListaMensagensAutomaticasDeTodasAsEmpresas = _automacaoRepositorio.ListaTipoAutomacao();

            foreach (var l in ListaMensagensAutomaticasDeTodasAsEmpresas)
            {

                if (l.TipoAutomacao == "Trinta dias" && l.Estado == true)
                {
                    IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao;
                    listDadosAutomacao = await _automacaoRepositorio.ObterContatosQueVisitaramAposTrintaDias("Trinta dias", l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                    foreach (var item in listDadosAutomacao)
                    {

                        TimeSpan data = DateTime.Now.Subtract(item.DataVisita);
                        int qtdDiasAposUltimaVisita = data.Days;  // pega o dia

                        //int dia = Convert.ToInt32(i.DataVisita.Date - DateTime.Now.Date);   //subtrai a data com a data atual  e converte em inteiro a subtração

                        if (qtdDiasAposUltimaVisita >= 1)
                        {

                            arrayContatos.Add(item.Contato );

                        }

                        string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                        string horaEnvio = "10:15";

                        var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(arrayContatos, item.Conteudo, dataEnvio, horaEnvio);
                        Agenda agenda = new Agenda(dataEnvio, horaEnvio);
                        var campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agenda);
                        int qtdEnviada = arrayContatos.Count;
                        campanhaSMS.CalcularQtdEnviado(qtdEnviada);
                        await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                        foreach (var c in arrayDeCodigoDasMensagens)
                        {
                            var array = c.Split(',');
                            var contato = array[0];
                            var idSMS = Convert.ToInt32(array[1]);
                            var _dataEnvio = DateTime.Now;
                            var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, l.IdEmpresa, l.ID, idSMS);
                            await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                        }
                    }
                }
            }
        }
        //-----4 APOS QUINZE DIAS SEM RETORNO, EXECUTAR O SHEDULE AS 03:30HS; TODO DIA VERIFICA E AGENDA O ENVIO PARA AS 10:12HS
        public async Task AutomacaoClientesInativoQuinzeDias()
        {
            List<string> arrayContatos = new List<string>();

            IEnumerable<Mensagem> ListaMensagensAutomaticasDeTodasAsEmpresas = _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in ListaMensagensAutomaticasDeTodasAsEmpresas)
            {

                if (l.TipoAutomacao == "Trinta dias" && l.Estado == true)
                {
                    IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao;
                    listDadosAutomacao = await _automacaoRepositorio.ObterContatosQueVisitaramAposQuinzeDias("Quinze dias", l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                    foreach (var item in listDadosAutomacao)
                    {

                        TimeSpan data = DateTime.Now.Subtract(item.DataVisita);
                        int qtdDiasAposUltimaVisita = data.Days;

                        //int dia = Convert.ToInt32(i.DataVisita.Date - DateTime.Now.Date);  

                        if (qtdDiasAposUltimaVisita >= 1)
                        {
                            arrayContatos.Add(item.Contato);
                        }


                        string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                        string horaEnvio = "10:12";

                        var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(arrayContatos, item.Conteudo, dataEnvio, horaEnvio);
                        Agenda agenda = new Agenda(dataEnvio, horaEnvio);
                        var campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agenda);

                        campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
                        await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                        foreach (var c in arrayDeCodigoDasMensagens)
                        {

                            var array = c.Split(',');
                            // [0] = contato,  [1] = Id
                            //aqui vai cria a situação da campanha
                            var contato = array[0];
                            var idSMS = Convert.ToInt32(array[1]);
                            var _dataEnvio = DateTime.Now;
                            var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, l.IdEmpresa, l.ID, idSMS);
                            await _situacaoRepositorio.SalvarSituacao(mensagemSMS);

                        }
                    }
                }
            }
        }

        //// //-----5 Apos ultima fidelização, EXECUTAR O SHEDULE AS 03:30HS; TODO DIA VERIFICA  E AGENDA O ENVIO PARA AS 10:15HS
        public async Task AutomacaoAposUltimaFidelizacao()
        {
            List<string> arrayContatos = new List<string>();

            IEnumerable<Mensagem> ListaMensagensAutomaticasDeTodasAsEmpresas = _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in ListaMensagensAutomaticasDeTodasAsEmpresas)
            {

                if (l.TipoAutomacao == "Ultima fidelizacao" && l.Estado == true)
                {

                    IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao;
                    listDadosAutomacao = await _automacaoRepositorio.ObterContatosQueVisitaramAposUltimaFidelizacao("Ultima fidelizacao", l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                    //string[] arrayContatos = new string[] { };

                    foreach (var item in listDadosAutomacao)
                    {

                        TimeSpan data = DateTime.Now.Subtract(item.DataVisita);
                        int dia = data.Days;

                        //int dia = Convert.ToInt32(i.DataVisita.Date - DateTime.Now.Date);  

                        if (dia == item.TempoPorDia)
                        {
                            arrayContatos.Add( item.Contato );
                        }

                        string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                        string horaEnvio = "11:40";

                        var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(arrayContatos, item.Conteudo, dataEnvio, horaEnvio);
                        Agenda agenda = new Agenda(dataEnvio, horaEnvio);
                        var campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agenda);
                        campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
                        await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                        foreach (var c in arrayDeCodigoDasMensagens)
                        {

                            var array = c.Split(',');
                            var contato = array[0];
                            var idSMS = Convert.ToInt32(array[1]);
                            var _dataEnvio = DateTime.Now;
                            var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, l.IdEmpresa, l.ID, idSMS);
                            await _situacaoRepositorio.SalvarSituacao(mensagemSMS);

                        }

                    }
                }
            }
        }
    }
}
