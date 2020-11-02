using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.ApiChatproWhatsapp
{
    public interface IChatproWhatsApp
    { 
        Task Enviar_mensagemDaPontuacao(string Contato, string Conteudo);
        Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ListaContatosPorSegCustomizado> ListContatos, string Conteudo);
        Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao, string Conteudo, string DataEnvio, string HoraEnvio);
        Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao, string DataEnvio, string HoraEnvio);
        Task<string> EnviarMensagemEmMassa(string Contato, string NomeCompleto, string Conteudo, string DataEnvio, string HoraEnvio);
    } 
}
