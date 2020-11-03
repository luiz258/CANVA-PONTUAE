using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.ApiChatproWhatsapp
{
    public class ChatproWhatsApp : IChatproWhatsApp
    {

        public ChatproWhatsApp()
        {

        }
        public async Task Enviar_mensagemDaPontuacao(string Contato, string Conteudo)
        {
            var client = new RestClient("https://v4.chatpro.com.br/chatpro-o6dwpl29ja/api/v1/send_message");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "1gyapr4q30bdl1cb2ds4l0f449on4h");
            request.AddParameter("undefined", "{\r\n  \"menssage\":" + $"\"{Conteudo}\",\r\n" + "\"number\": " + $"\"{Contato}\"\r\n" + " }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

        }

        //METODO  API SMS DEV: AGENDA DE ENVIO DE SMS     
        public async Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ListaContatosPorSegCustomizado> ListContatos, string Conteudo)
        {
            List<string> _Contatos = new List<string>();
            foreach (var item in ListContatos)
            {
                _Contatos.Add(item.Contato);

                string Conteudoeditado;
                if (item.NomeCompleto != null)
                {
                    dynamic _nomeCompleto = item.NomeCompleto;
                    dynamic _nome = _nomeCompleto.Split(" ");
                    string n = Convert.ToString(_nome[0]);
                    Conteudoeditado = Conteudo.Replace("%nome%", n);
                }
                else
                {
                    Conteudoeditado = Conteudo;
                    Conteudoeditado = Conteudo.Replace("%nome%", "");
                }
 
                var numero = item.Contato;
                //
                var client = new RestClient("https://v4.chatpro.com.br/chatpro-o6dwpl29ja/api/v1/send_message");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Authorization", "1gyapr4q30bdl1cb2ds4l0f449on4h");
                request.AddParameter("undefined", "{\r\n  \"menssage\":" + $"\"{Conteudoeditado}\",\r\n" + "\"number\": " + $"\"{numero}\"\r\n" + " }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //
                string posicao = $"{numero}";

                _Contatos.Add(posicao);
                _Contatos.Remove(item.Contato);

            }

            return _Contatos;
        }



        public async Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao, string Conteudo, string DataEnvio, string HoraEnvio)
        {
            List<string> _Contatos = new List<string>();
            foreach (var item in listaDadosAutomacao)
            {
                _Contatos.Add(item.Contato);


                string Conteudoeditado;
                if (item.NomeCompleto != null)
                {
                    dynamic _nomeCompleto = item.NomeCompleto;
                    dynamic _nome = _nomeCompleto.Split(" ");
                    string n = Convert.ToString(_nome[0]);
                    Conteudoeditado = Conteudo.Replace("%nome%", n);
                }
                else
                {
                    Conteudoeditado = Conteudo;
                    Conteudoeditado = Conteudo.Replace("%nome%", "");
                }

                var numero = item.Contato;
                //
                var client = new RestClient("https://v4.chatpro.com.br/chatpro-o6dwpl29ja/api/v1/send_message");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Authorization", "1gyapr4q30bdl1cb2ds4l0f449on4h");
                request.AddParameter("undefined", "{\r\n  \"menssage\":" + $"\"{Conteudoeditado}\",\r\n" + "\"number\": " + $"\"{numero}\"\r\n" + " }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //
                string posicao = $"{numero}";

                _Contatos.Add(posicao);
                _Contatos.Remove(item.Contato);

            }

            return _Contatos;
        }



        public async Task<List<string>> EnviarMensagemEmMassa(IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao, string DataEnvio, string HoraEnvio)
        {
            List<string> _Contatos = new List<string>();
            foreach (var item in listDadosAutomacao)
            {
                _Contatos.Add(item.Contato);


                string Conteudoeditado;
                if (item.NomeCompleto != null)
                {
                    dynamic _nomeCompleto = item.NomeCompleto;
                    dynamic _nome = _nomeCompleto.Split(" ");
                    string n = Convert.ToString(_nome[0]);
                    Conteudoeditado = item.Conteudo.Replace("%nome%", n);
                }
                else
                {
                    Conteudoeditado = item.Conteudo;
                    Conteudoeditado = item.Conteudo.Replace("%nome%", "");
                }
                var numero = item.Contato;
                //
                var client = new RestClient("https://v4.chatpro.com.br/chatpro-o6dwpl29ja/api/v1/send_message");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("Authorization", "1gyapr4q30bdl1cb2ds4l0f449on4h");
                request.AddParameter("undefined", "{\r\n  \"menssage\":" + $"\"{Conteudoeditado}\",\r\n" + "\"number\": " + $"\"{numero}\"\r\n" + " }", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //
                string posicao = $"{numero}";

                _Contatos.Add(posicao);
                _Contatos.Remove(item.Contato);

            }

            return _Contatos;
        }


        public async Task<string> EnviarMensagemEmMassa(string Contato, string NomeCompleto, string Conteudo, string DataEnvio, string HoraEnvio)
        {
            string Conteudoeditado;
            if (NomeCompleto != null)
            {
                dynamic _nomeCompleto = NomeCompleto;
                dynamic _nome = _nomeCompleto.Split(" ");
                string n = Convert.ToString(_nome[0]);
                Conteudoeditado = Conteudo.Replace("%nome%", n);
            }
            else
            {
                Conteudoeditado = Conteudo;
                Conteudoeditado = Conteudo.Replace("%nome%", "");
            }
            var numero = Contato;
            //
            var client = new RestClient("https://v4.chatpro.com.br/chatpro-o6dwpl29ja/api/v1/send_message");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "1gyapr4q30bdl1cb2ds4l0f449on4h");
            request.AddParameter("undefined", "{\r\n  \"menssage\":" + $"\"{Conteudoeditado}\",\r\n" + "\"number\": " + $"\"{numero}\"\r\n" + " }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //
            string conatoComIdentificacao = $"{numero}";

            return conatoComIdentificacao;
        }

    }
}
