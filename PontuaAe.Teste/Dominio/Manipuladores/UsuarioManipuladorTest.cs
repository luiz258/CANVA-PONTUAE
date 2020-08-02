using Microsoft.VisualStudio.TestTools.UnitTesting;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Entradas;
using System;
using PontuaAe.Dominio.FidelidadeContexto.Comandos.AutenticaComandos.Manipulador;
using PontuaAe.Dominio.FidelidadeContexto.Repositorios;


namespace PontuaAe.Teste.Dominio.Manipuladores
{
    [TestClass]
    public class UsuarioManipuladorTest
    {
        private readonly IUsuarioRepositorio _usuRep;
        private readonly IEmpresaRepositorio _empRep;
        public UsuarioManipuladorTest()
        {
           // _usuRep = new UsuarioRepositorio();
           // _empRep = new EmpresaRepositorio();
        }
        [TestMethod]
        [TestCategory("Usuarios")]
        public void Criar_Usuario_Empresa()
        {

            var comando = new CadastroEmpresaComando();
            comando.Bairro = "rua boa esperaça " + DateTime.Now.ToString();
            comando.Cep = "77828322";
            comando.Cidade = "Araguaina";
            //comando.ClaimType = "PontuaAe";
            //comando.ClaimValue = "Admin";
            comando.Complemento = "dolado de casa";
            comando.Delivery = "Link.com/br";
            comando.Descricao = "Empresa de teste da pontuaae";
            comando.Documento = "74168148000176"; // Precisar alterar o cnpj antes de execultar
            comando.Email = "testAdmin@gmail.com";
            comando.Estado = "Tocantins";
            comando.Facebook = "dsadsadsa/facebook";
            comando.Horario = "segunda a sexta das 8h até 00h";
            comando.Instagram = "instagram";
            comando.Logo = "1";
            comando.NomeFantasia = "Teste Testes";
            comando.NomeResponsavel = "Luiz REsponsavel teste";
            comando.Numero = "123455432";
            comando.Rua = "morada do sol";
            comando.Seguimento = "alimentação";
            comando.Senha = "34123231";
            comando.Telefone = "9998989898";
            comando.Website = "dsadsad/site";

            //var manipulador = 
            //    new UsuarioManipulador(
            //    _usuRep,
             //   _empRep
            //    );

            //manipulador.Manipular(comando);




            Assert.IsTrue(true);
        }

    }
}
