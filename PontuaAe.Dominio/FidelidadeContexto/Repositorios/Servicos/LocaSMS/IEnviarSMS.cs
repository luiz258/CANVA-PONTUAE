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
        Task<string[]> EnviarSMSPorSMSDEVAsync(string[] Contatos, string Conteudo);
        Task<List<string>> EnviarSMSPorSMSDEVAsync(List<string> Contatos, string Conteudo, string DataEnvio, string HoraEnvio);
        //Task<string> AgendarEnvioPorLocaSMSAsync(string Contatos, string Conteudo, string DataEnvio, string HoraEnvio);





    }
}
