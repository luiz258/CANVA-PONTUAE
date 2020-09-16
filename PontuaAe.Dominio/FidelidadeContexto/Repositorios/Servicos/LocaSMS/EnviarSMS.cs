using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.MarketingConsulta;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Repositorios.Servicos.LocaSMS
{
    public class EnviarSMS : IEnviarSMS
    {

        private static HttpClient client = new HttpClient();
        private readonly string login = "6392892283";
        //private readonly string login = "63992892283";
        private string senha_LocaSMS = "599451";
        //private string senha = "949567";
        //API:  SMS DEV; conta: wandesoncti@gmail.com senha: 15662425  contato: 992892283
        private string Chave_SMSDEV = "0ZUR71CLFVZ47EPBQOQGSNZ1";
        String[] arrayId;
       


        // METODO  API  LOCASMS:  ENVIAR MENSAGENS
        public async Task<int> EnviarPorLocaSMSAsync(string Contato, string Conteudo)
        {
            StringContent queryString = new StringContent("");
            HttpResponseMessage response = await client.PostAsync($"http://209.133.205.2/painel/api.ashx?action=sendsms&lgn={login}&pwd={senha_LocaSMS}&msg={Conteudo}&numbers={Contato}", queryString);
            response.EnsureSuccessStatusCode();
            string resultado = response.Content.ReadAsStringAsync().Result;
            String _resultado = resultado;
            string[] dados = _resultado.Split(',');
            var _dado = dados[1];
            string[] d = _dado.Split(':');
            var dado = JsonConvert.DeserializeObject(d[1]);
            int _identificacao = Convert.ToInt32(dado);
            return _identificacao;
        }


        //METODO  API  SMS DEV:  ENVIA MENSAGEM, usado na pontuação
        public async Task Enviar_Um_SMSPor_API_SMSDEVAsync(string Contatos, string Conteudo)
        {



            var numero = Contatos;
            StringContent queryString = new StringContent("");
            HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudo}", queryString);
            response.EnsureSuccessStatusCode();
            string resultado = response.Content.ReadAsStringAsync().Result;
            String _resultado = resultado;
            //string[] dados = _resultado.Split(',');
            //var _dado = dados[2];
            //string[] d = _dado.Split(':');
            //var dado = JsonConvert.DeserializeObject(d[1]);
            //int id = Convert.ToInt32(dado);
            //string posicao = $"{numero} , {id}"; //
        }

         

        //METODO  API LOCASMS:  AGENDA DE ENVIO DE SMS
        //public async Task<string> AgendarEnvioPorLocaSMSAsync(string Contatos, string Conteudo, string DataEnvio, string HoraEnvio)
        //{
        //    StringContent queryString = new StringContent("");
        //    HttpResponseMessage response = await client.PostAsync($"http://209.133.205.2/painel/api.ashx?action=sendsms&lgn={login}&pwd={senha_LocaSMS}&msg={Conteudo}&numbers={Contatos}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
        //    response.EnsureSuccessStatusCode();
        //    string resultado = response.Content.ReadAsStringAsync().Result;
        //    dynamic _resultado = JsonConvert.DeserializeObject(resultado);
        //    var idCampanha = _resultado.data.results[0];
        //    return idCampanha;
        //}

        //METODO  API SMS DEV: AGENDA DE ENVIO DE SMS     
        public async Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ListaContatosPorSegCustomizado> ListContatos, string Conteudo, string DataEnvio, string HoraEnvio)
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
                    StringContent queryString = new StringContent("");
                    HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudoeditado}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
                    response.EnsureSuccessStatusCode();
                    string resultado = response.Content.ReadAsStringAsync().Result;
                    String _resultado = resultado;
                    string[] dados = _resultado.Split(',');
                    var _dado = dados[2];
                    string[] d = _dado.Split(':');
                    var dado = JsonConvert.DeserializeObject(d[1]);
                    int id = Convert.ToInt32(dado);
                    string posicao = $"{numero} , {id}";

                     _Contatos.Add(posicao);
                     _Contatos.Remove(item.Contato);
                     

               
            }

            return _Contatos;
        }

        public async Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ObterAutomacaoTipoDiaSemana> listaDadosAutomacao, string Conteudo, string DataEnvio, string HoraEnvio)
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
                StringContent queryString = new StringContent("");
                HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudoeditado}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
                response.EnsureSuccessStatusCode();
                string resultado = response.Content.ReadAsStringAsync().Result;
                String _resultado = resultado;
                string[] dados = _resultado.Split(',');
                var _dado = dados[2];
                string[] d = _dado.Split(':');
                var dado = JsonConvert.DeserializeObject(d[1]);
                int id = Convert.ToInt32(dado);
                string posicao = $"{numero} , {id}";

                _Contatos.Add(posicao);
                _Contatos.Remove(item.Contato);

            }

            return _Contatos;
        }

        public async Task<List<string>> EnviarSMSPorSMSDEVAsync(IEnumerable<ObterAutomacaoTipoUltimaFide> listDadosAutomacao, string DataEnvio, string HoraEnvio)
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
                StringContent queryString = new StringContent("");
                HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudoeditado}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
                response.EnsureSuccessStatusCode();
                string resultado = response.Content.ReadAsStringAsync().Result;
                String _resultado = resultado;
                string[] dados = _resultado.Split(',');
                var _dado = dados[2];
                string[] d = _dado.Split(':');
                var dado = JsonConvert.DeserializeObject(d[1]);
                int id = Convert.ToInt32(dado);
                string posicao = $"{numero} , {id}";

                _Contatos.Add(posicao);
                _Contatos.Remove(item.Contato);

            }

            return _Contatos;
        }

        public async Task<string> EnviarSMSPorSMSDEVAsync(string Contato, string NomeCompleto, string Conteudo, string DataEnvio, string HoraEnvio)
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
            StringContent queryString = new StringContent("");
            HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudoeditado}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
            response.EnsureSuccessStatusCode();
            string resultado = response.Content.ReadAsStringAsync().Result;
            String _resultado = resultado;
            string[] dados = _resultado.Split(',');
            var _dado = dados[2];
            string[] d = _dado.Split(':');
            var dado = JsonConvert.DeserializeObject(d[1]);
            int id = Convert.ToInt32(dado);
            string conatoComIdentificacao= $"{numero} , {id}";
       
            return conatoComIdentificacao;
        }









        //Manter desativado por não se viavel e correr risco de travar a aplicação           //Metodo obter resultado do envio  da API LOCASMS
        //public async Task<dynamic> ObterSituacaoSMSAsync(int IdCampanhar)
        //{
        //    HttpResponseMessage response = await client.GetAsync($"http://209.133.205.2/painel/api.ashx?action=getstatus&lgn={login}&pwd={senha_LocaSMS}&id={IdCampanhar}");
        //    response.EnsureSuccessStatusCode();
        //    var resultado = response.Content.ReadAsStringAsync().Result;
        //    return resultado;
        //}






    }
}
