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
using System.Linq;

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
     
        //string[] ContatosAniversarianteMesmoDia;  remover
        //string[] ContatosAniversariantesDiasDesiguais;  remover
      

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
            //List<string> ContatosAniversariantesDiasDesiguais = new List<string>();
            //List<string> ContatosAniversarianteMesmoDia = new List<string>();

            List<ObterAutomacaoTipoAniversario> listaContatosAniversarianteMesmoDia = new List<ObterAutomacaoTipoAniversario>();
           
            IEnumerable<Mensagem> Lista = await _automacaoRepositorio.ListaTipoAutomacao();    

            foreach (var linha in Lista)
            {

                if (linha.Estado == 1)
                {
                    IEnumerable<ObterAutomacaoTipoAniversario> listDadosAutomacaoAniversario;
                    listDadosAutomacaoAniversario = await _automacaoRepositorio.ObterDadosAutomacaoAniversario(linha.Segmentacao, linha.SegCustomizado, linha.IdEmpresa);

                    //foreach (var item in listDadosAutomacaoAniversario)
                    //{
                    //    listaContatos.Add(new ObterAutomacaoTipoAniversario { DataNascimento = item.DataNascimento, Contato = item.Contato, NomeCompleto = item.NomeCompleto, Conteudo = item.Conteudo });
                    //}

                    foreach (var l in listDadosAutomacaoAniversario)
                    {

                      
                        var data_ = DateTime.Now.Date;
                        var ano = data_.Year.ToString();

                                string _horaEnvio = "10:30";
                                
                                string _dataEnvio = "";

                                if (l.DiasAntesAniversario != 0)
                                {                                    
                                    //int diasAntes = l.DiasAntesAniversario;
                                    var data1 = l.DataNascimento.Remove(0, 2);
                                    var caracter = l.DiasAntesAniversario.ToString();
                                    _dataEnvio = data1.Insert(0, $"{caracter}");
                                    // String data = l.DataNascimento.AddDays(-diasAntes);
                                    //string dataEnvio = data.ToString("dd-MM-yyyy");
                                    _horaEnvio = "11:30";
                                                                                           
                                }

                                if (l.DiasAntesAniversario == 0)
                                {
                                    _dataEnvio = l.DataNascimento;
                                    _horaEnvio = "11:00";
                                }


                                //nesta linha vai vim um array
                                string apenasUmaMensagem = await _enviarSMS.EnviarSMSPorSMSDEVAsync(l.Contato, l.NomeCompleto, l.Conteudo, _dataEnvio, _horaEnvio);
                                Agenda agendar = new Agenda(_dataEnvio, _horaEnvio);
                                //aqui criar um foreach para pecorrer o array de codigo cada codigo vai ser 
                                Mensagem campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agendar);

                                campanhaSMS.CalcularQtdEnviado(1);
                                await _automacaoRepositorio.Editar(campanhaSMS);

                               
                                    var array = apenasUmaMensagem.Split(',');
                                    var contato = array[0];
                                    var idSMS = Convert.ToInt32(array[1]);
                                    var dataEnvio_ = DateTime.Now;
                                    var mensagemSMS = new SituacaoSMS(dataEnvio_, contato, l.IdEmpresa, l.ID, idSMS);
                                    await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                                                                   
                    }

                }
            }
        }

        //-----2  EM UM DIA DA SEMANA  EXECUTAR O SHEDULE AS 04:00HS; TODO DIA VERIFICA  E AGENDA O ENVIO PARA AS 08:00HS
        public async Task AutomacaoTipoDiaDaSemana()
        {

            IEnumerable<Mensagem> Lista = await _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in Lista)
            {
                if (l.Estado == 1)
                {
                    if (l.TempoPorDiaDaSemana == "Segunda-feira")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Terça-feira")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Quarta-feira")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Quinta-feira")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Sexta-feira")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Sábado")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }
                    if (l.TempoPorDiaDaSemana == "Domingo")
                    {
                        DiaDaSemana("Dia da Semana", l.Segmentacao, l.SegCustomizado, l.IdEmpresa, l.ID);
                    }


                }
            }
        }

        public async Task DiaDaSemana(string diaDaSemana, string segmentacao, string segCustomizado, int idEmpresa, int ID)
        {


                    //CRIAR UM IF PRA VERIFICA   SO PASSA SE O Tipo de automação for ingual a dia da semana

                    IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao;
                    listaDadosAutomacao = await _automacaoRepositorio.ObterDadosAutomacaoSemana(diaDaSemana, segmentacao, segCustomizado, idEmpresa);
                    
                    //string[] Contato = _automacaoRepositorio.ListaTelefones(l.IdEmpresa, l.SegCustomizado, l.Segmentacao);
                    //int QtdContatos = Contato.Length;

                    foreach (var linha in listaDadosAutomacao)
                    {

                        var aa = DateTime.Now.DayOfWeek.ToString();
                        if (linha.TempoPorDiaDaSemana == aa) //OBS SE NÃO FUNCIONAR, ENTÃO IMPLEMENTA DateTime.Now.DayOfWeek PARA OBTER O DIA DA SEMANA NA DATA ATUAL NA VARIAVEL Dia
                        {
                            string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                            //var data = dataEnvio.ToShortDateString();
                            string hora = "11:47";

                            var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(listaDadosAutomacao, linha.Conteudo, dataEnvio, hora);
                            //resolver esta parte abaixo
                            Agenda agenda = new Agenda(dataEnvio, hora);
                            var campanhaSMS = new Mensagem(ID, idEmpresa, agenda);
                            campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
                            await _automacaoRepositorio.atualizarDadosMensagem(campanhaSMS);

                            foreach (var c in arrayDeCodigoDasMensagens)
                            {
                                var array = c.Split(',');
                                var contato = array[0];
                                var idSMS = Convert.ToInt32(array[1]);
                                var _dataEnvio = DateTime.Now;
                                var mensagemSMS = new SituacaoSMS(_dataEnvio, contato, idEmpresa, ID, idSMS);
                                await _situacaoRepositorio.SalvarSituacao(mensagemSMS);
                            }

                        }
                    }
              
        }


        ////-----3 APOS  TRINTA DIAS SEM RETORNO, EXECUTAR O SHEDULE AS 03:30HS; TODO DIA VERIFICA  E AGENDA O ENVIO PARA AS 10:15HS
        public async Task AutomacaoTipoTrintaDias()
        {
       
            IEnumerable<Mensagem> ListaMensagensAutomaticasDeTodasAsEmpresas = await _automacaoRepositorio.ListaTipoAutomacao();

            foreach (var l in ListaMensagensAutomaticasDeTodasAsEmpresas)
            {

                if (l.Estado == 1)
                {
                    IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao;
                    listDadosAutomacao = await _automacaoRepositorio.ObterContatosQueVisitaramAposTrintaDias( l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                  
                        string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                        string horaEnvio = "10:15";

                        var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(listDadosAutomacao, dataEnvio, horaEnvio);
                        Agenda agenda = new Agenda(dataEnvio, horaEnvio);
                        var campanhaSMS = new Mensagem(l.ID, l.IdEmpresa, agenda);
                        int qtdEnviada = arrayDeCodigoDasMensagens.Count;
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
















        
       
        
        public async Task AutomacaoAposUltimaFidelizacao()
        {
            //BUSCAR LISTA DE DATAS DE VISITAS COM ID EMPRESAS  EM CADA 
            //quando eu criar uma configuração de uma campanha automatica para envia sms para base segmentada na regra de apos ultima fidelização
            List<dynamic> arrayContatos = new List<dynamic>();
           

            IEnumerable<Mensagem> ListaMensagensAutomaticasDeTodasAsEmpresas = await _automacaoRepositorio.ListaTipoAutomacao();
            foreach (var l in ListaMensagensAutomaticasDeTodasAsEmpresas)
            {
                int numeroNegativo = -System.Math.Abs(l.TempoPorDia);
                IEnumerable<Mensagem> ListaDatasUlimasVisitas = await _automacaoRepositorio.ListaDatasUlimasVisitas(l.IdEmpresa, numeroNegativo, l.SegCustomizado);
                foreach (var item in ListaDatasUlimasVisitas)
                {

                
                
                if (l.Estado == 1)
                {

                    IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao;

                    listDadosAutomacao = await _automacaoRepositorio.ObterContatosQueVisitaramAposUltimaFidelizacao( item.DataUlimaVisita, l.Segmentacao, l.SegCustomizado, l.IdEmpresa);

                    //string[] arrayContatos = new string[] { };



                    string dataEnvio = DateTime.Now.ToString("dd/MM/yyyy");
                    string horaEnvio = "14:30";

                    var arrayDeCodigoDasMensagens = await _enviarSMS.EnviarSMSPorSMSDEVAsync(listDadosAutomacao, dataEnvio, horaEnvio);
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
