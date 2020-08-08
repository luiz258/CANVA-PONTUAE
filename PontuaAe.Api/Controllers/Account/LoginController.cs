using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PM.Api.Controllers.Services.jwt;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;

namespace PontuaAe.Api.Controllers.Account
{

    [ApiController]
    [Route("v1/login")]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepositorio _repUsuario;
        private readonly IEmpresaRepositorio _repEmpresa;
        private readonly IClienteRepositorio _repCliente;
        public LoginController(IUsuarioRepositorio repository, IEmpresaRepositorio empresaRepository, IClienteRepositorio clienteRepository)
        {
            _repUsuario = repository;
            _repCliente = clienteRepository;
            _repEmpresa = empresaRepository;
        }

        
        [HttpPost]
        [Route("v1/Autenticacao")]
        public async Task<ActionResult<dynamic>>  Autenticacao([FromBody] UsuarioComando model)
        {

            var usuario = await _repUsuario.ObterUsuario(model.Email);

            // se a linha acima não funcionar testa este bloco
           // var dd = new Usuario(usuario.Email, usuario.Senha);
            //var r = dd.Autenticar(usuario.Email, usuario.Senha);


            var _resultado =  usuario.Autenticar(model.Email, model.Senha); 
            dynamic role = usuario.RoleId;
           
            if (_resultado == true)
            {
                if (role == "Funcionario")
                {
                    var IdEmpresa = await _repEmpresa.ObterIdEmpresa(usuario.ID);
                    var token = TokenService.GenerateToken(usuario);
                    return new
                    {
                        token = token,
                        User = new 
                        {
                            claimValue = usuario.RoleId,
                            id = usuario.ID,
                            idEmpresa = IdEmpresa,
                            contato = usuario.Contato,
                            email = usuario.Email,
                        }

                       
                       
                    };

                }  
                
                
                if (role == "Administrador")
                {
                    var IdEmpresa = await _repEmpresa.ObterIdEmpresa(usuario.ID);
                    var token = TokenService.GenerateToken(usuario);
                    return new
                    {
                        token = token,
                        User = new
                        {
                            claimValue = usuario.RoleId,
                            id = usuario.ID,
                            idEmpresa = IdEmpresa,
                            //contato = "",
                            email = model.Email


                        }

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