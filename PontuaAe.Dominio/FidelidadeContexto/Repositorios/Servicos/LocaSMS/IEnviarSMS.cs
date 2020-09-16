using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS
{
    public interface IEnviarSMS
    {
        Task<int> EnviarPorLocaSMSAsync(string Contato, string Conteudo);
        Task Enviar_Um_SMSPor_API_SMSDEVAsync(string Contatos, string Conteudo);
        Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ListaContatosPorSegCustomizado> ListContatos, string Conteudo, string DataEnvio, string HoraEnvio);
        Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao, string Conteudo, string DataEnvio, string HoraEnvio);
        Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao, string DataEnvio, string HoraEnvio);
        Task<string> EnviarSMSPorSMSDEVAsync(string Contato, string NomeCompleto, string Conteudo, string DataEnvio, string HoraEnvio);
        //Task<string> AgendarEnvioPorLocaSMSAsync(string Contatos, string Conteudo, string DataEnvio, string HoraEnvio);





    }
}
