using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Api.Controllers.Services.jwt;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Account
{

   
    [Route("v1/loginCliente")]
    [AllowAnonymous]
    public class LoginClienteController : Controller
    {
        private readonly IUsuarioRepositorio _repUsuario;
        private readonly IEmpresaRepositorio _repEmpresa;
        private readonly IClienteRepositorio _repCliente;
        private readonly IFuncionarioRepositorio _repFuncionario;
        
        public LoginClienteController(IUsuarioRepositorio repository, IEmpresaRepositorio empresaRepository, IClienteRepositorio clienteRepository, IFuncionarioRepositorio repFuncionario)
        {
            _repUsuario = repository;
            _repCliente = clienteRepository;
            _repEmpresa = empresaRepository;
            _repFuncionario = repFuncionario;
    }

        [HttpPost]
        [Route("v1/Autenticacao")]
        public async Task<ActionResult<dynamic>> Autenticacao([FromBody] UsuarioComando model)
        {

            var usuario = await _repUsuario.ObterUsuario(model.Email);

            // se a linha acima não funcionar testa este bloco
            // var dd = new Usuario(usuario.Email, usuario.Senha);
            //var r = dd.Autenticar(usuario.Email, usuario.Senha);


            var _resultado =  usuario.Autenticar(model.Email, model.Senha);
         
            dynamic role =  usuario.RoleId;

            if (_resultado == true)
            {
                if (role == "Cliente")
                {

                    ObterUsuarioCliente _UsuarioCliente = await _repCliente.ObterDadosDoUsuarioCliente(usuario.ID);
                    //string _contatoFuncionario = await _repFuncionario.ObterContatoFuncionario(usuario.ID);
                    var token = TokenService.GenerateToken(usuario);
                    return new
                    {
                        token = token,
                        User = new
                        {
                            claimValue = usuario.RoleId,
                            id = usuario.ID,
                            contato = usuario.Contato,
                            email = usuario.Email,
                        },
                        Menssage = "Login efetuado com sucesso !",


                    };

                }

            }






            return NotFound(new { message = "Usuário ou senha inválidos" });

        }

        protected string EncriptaSenha(string pass)
        {
            if (string.IsNullOrEmpty(pass)) return "";
            ///var _senha = (pass += "|2d331cca-f6c0-40c0-bbp3-6e32909c2560");
            var md5 = System.Security.Cryptography.MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(pass));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }

    }
}