using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        //METODO  API  SMS DEV:  ENVIA MENSAGEM
        public async Task<string[]> EnviarSMSPorSMSDEVAsync(string[] Contatos, string Conteudo)
        {

            arrayId = new string[Contatos.Length];

            for (var i = 0; i < Contatos.Length; i++)
            {

                var numero = Contatos[i];
                StringContent queryString = new StringContent("");
                HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudo}", queryString);
                response.EnsureSuccessStatusCode();
                string resultado = response.Content.ReadAsStringAsync().Result;
                String _resultado = resultado;
                string[] dados = _resultado.Split(',');
                var _dado = dados[2];
                string[] d = _dado.Split(':');
                var dado = JsonConvert.DeserializeObject(d[1]);
                int id = Convert.ToInt32(dado);
                string posicao = $"{numero} , {id}";
                this.arrayId[i] = posicao;
            }

            return arrayId;

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
        public async Task<List<string>> EnviarSMSPorSMSDEVAsync(List<string> Contatos, string Conteudo, string DataEnvio, string HoraEnvio)
        {
           var  qtdContato = new string[Contatos.Count];

            for (var i = 0; i < qtdContato.Length; i++)
            {

                var numero = Contatos[i];
                StringContent queryString = new StringContent("");
                HttpResponseMessage response = await client.PostAsync($"https://api.smsdev.com.br/send?key={Chave_SMSDEV}&type=9&number={numero}&msg={Conteudo}&jobdate={DataEnvio}&jobtime={HoraEnvio}", queryString);
                response.EnsureSuccessStatusCode();
                string resultado = response.Content.ReadAsStringAsync().Result;
                String _resultado = resultado;
                string[] dados = _resultado.Split(',');
                var _dado = dados[2];
                string[] d = _dado.Split(':');
                var dado = JsonConvert.DeserializeObject(d[1]);
                int id = Convert.ToInt32(dado);
                string posicao = $"{numero} , {id}";
                Contatos[i] = posicao;
            }

            return Contatos;
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
