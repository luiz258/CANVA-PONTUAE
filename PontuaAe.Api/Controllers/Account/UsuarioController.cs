using Microsoft.AspNetCore.Mvc;
using PontuaAe.Api.Services.Email;
using PontuaAe.Compartilhado.Comandos;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Resultados;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Api.Controllers.Account
{

    [ApiController]
    [Route("v1/usuario")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioManipulador _manipulador;
        private readonly IUsuarioRepositorio _repository;
        private readonly IEmailSender _emailSender;


        public UsuarioController(UsuarioManipulador manipulador, IUsuarioRepositorio repository, IEmailSender emailSender)
        {
            _manipulador = manipulador;
            _repository = repository;
            _emailSender = emailSender;

        }

        [HttpPost]
        [Route("v1/recuperarSenha")] // recuperarConta
        //[Authorize(Policy = "Admin")]
        //[Authorize(Policy = "Funcionario")]
        public async Task<dynamic> SenhaEsquecida([FromBody] UsuarioComando dado)
        {
            var usuario = _repository.ObterUsuario(dado.Email);

            var senhaGerada = geraSenha();

            //email destino, assunto do email, mensagem a enviar
            EnviarEmail(dado.Email, $"", "Olá, 😉 Estamos enviando a sua nova senha " +
                "Você deverá alterar sua senha após o primeiro login.  Sua Senha 🔑: " + senhaGerada);

            var senha = CriptografarSenha(senhaGerada);
            _repository.ResetaSenha(senha, usuario.Id);

            return "OK";
        }

        protected void EnviarEmail(string email, string assunto, string mensagem)
        {
            try
            {
                //email destino, assunto do email, mensagem a enviar
                 _emailSender.SendEmailAsync(email, assunto, mensagem);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected string geraSenha()
        {
            string chars = "1234567890ABCD";
            string pass = "";
            Random random = new Random();
            for (int f = 0; f < 6; f++)
            {
                pass = pass + chars.Substring(random.Next(0, chars.Length - 1), 1);
            }

            return pass;
        }

        protected string CriptografarSenha(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            //var _senha = (pass += "|2d331cca-f6c0-40c0-bbp3-6e32909c2560");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(pass));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }


        [HttpPut]
        [Route("v1/putUsuario")]
        public async Task<IComandoResultado> PutAsync([FromBody]  UsuarioComando comando)
        {
            var usuario = (ComandoUsuarioResultado)await _manipulador.ManipularAsync(comando);
            return usuario;
        }
    }
}

