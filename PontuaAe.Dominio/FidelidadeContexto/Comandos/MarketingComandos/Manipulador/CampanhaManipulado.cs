using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Resultado;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.MarketingComandos.Manipulador
{
    public class CampanhaManipulado : Notifiable,
        IComandoManipulador<AddCampanhaComando>,
        //IComandoManipulador<EditarCampanhaComando>,
        IComandoManipulador<RemoverCampanhaComando>
    {
        private readonly ICampanhaMSGRepositorio _campanhaRepositorio;
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly ISituacaoRepositorio _situacaoRepositorio;
        private readonly IEnviarSMS _enviarSMS;
        private readonly IEnviarSMS _agendarSMS;
       

        public CampanhaManipulado(
            ICampanhaMSGRepositorio campanhaRepositorio,
            IEmpresaRepositorio empresaRepositorio,
            IClienteRepositorio clienteRepositorio,
            ISituacaoRepositorio situacaoRepositorio,
            IEnviarSMS enviarSMS,
            IEnviarSMS agendarSMS

           )

        {
            _campanhaRepositorio = campanhaRepositorio;
            _empresaRepositorio = empresaRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _situacaoRepositorio = situacaoRepositorio;
            _enviarSMS = enviarSMS;
            _agendarSMS = agendarSMS;

        }

        public async Task<IComandoResultado> ManipularAsync(AddCampanhaComando comando)
        {
            

            var nomeEmpresa = await _empresaRepositorio.ObterNome(comando.IdEmpresa);

            var conteudo = $"{nomeEmpresa}:{comando.Conteudo}";

           
            IEnumerable <ListaContatosPorSegCustomizado> ListContatos = await _campanhaRepositorio.BuscaContatosPorSegCustomizado(comando.IdEmpresa, comando.SegCustomizado);
           
            List<string> tt = new List<string>();
            foreach (var item in ListContatos)
            {
                tt.Add(item.Contato);
            }

            var data_ = comando.DataEnvio.ToString("dd/MM/yyyy");
            //var hora_ = comando.HoraEnvio.ToString("HH:mm:ss");
            var arrayDeCodigoDasMensagens = await  _enviarSMS.EnviarSMSPorSMSDEVAsync(tt, conteudo, data_, comando.HoraEnvio);


            var juntaDataHora = $"{data_}" + $"{comando.HoraEnvio}";

            var agenda = new Agenda(juntaDataHora);  
            var _campanhaSMS = new Mensagem(comando.IdEmpresa, comando.Nome, comando.Segmentacao, comando.SegCustomizado, comando.QtdSelecionado, comando.Conteudo, agenda);
            _campanhaSMS.CalcularQtdEnviado(arrayDeCodigoDasMensagens.Count);
            await _campanhaRepositorio.Salvar(_campanhaSMS);

            //Obter ID da Campanha  e  criar  tabela Situacao com codigo, idMensagem e IdEmpresa Relacionados 
            var IdCampanha = await _campanhaRepositorio.ObterID(comando.IdEmpresa);

            foreach (var c in arrayDeCodigoDasMensagens)
            {
                var array = c.Split(',');
                // [0] = contato,  [1] = Id
                //aqui vai cria a situação da campanha
                var contato = array[0];
                var idSMS =  Convert.ToInt32(array[1]);
                var dataEnvio =  DateTime.Now;
                var mensagemSMS = new SituacaoSMS(dataEnvio, contato, comando.IdEmpresa, IdCampanha, idSMS);
                await _situacaoRepositorio.SalvarSituacao(mensagemSMS);

            }

            return new ComandoResultado(true, "OK", Notifications);

        }


        public async Task<IComandoResultado> ManipularAsync(RemoverCampanhaComando comando)
        {
            await _campanhaRepositorio.Deletar(comando.ID, comando.IdEmpresa);
            return new ComandoResultado(true, "OK ", Notifications);
        }


    }

}

