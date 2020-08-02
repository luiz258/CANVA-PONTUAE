
//using FluentValidator;
//using FluentValidator.Validation;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using PontuaAe.Domain.FidelidadeContexto.Entidades;
//using PontuaAe.Domain.FidelidadeContexto.ValorObjeto;
//using System;

//namespace PontuaAe.Teste
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        private Cliente _cli;
//        private Empresa _emp;
//        private Descricao _desc;
//        private Pontuacao _p;
//        private Ponto _ponto;
//        [TestMethod]
//        public void TestMethod1()
//        {
            
//            var nome = new Nome("Luiz", "Marcelo");
//            var cpf = new CPF("05642712179");
//            var user = new Usuario(cpf,"32432532532523", "32432532532523");
//            var doc = new Documento("67.130.044/0001-07");
//            var email = new Email("luizmarcelo@gmail.com");
//            var end = new Endereco("sol","boa", "1234","778283","Araguina", "to","ddd");
//            var adm = new Administrador(email, "32432532532523", "32432532532523");
//            _desc = new Descricao("afsafsafsafsafsafswewqewsdsadsadsafsaggjjkuysafsafsafsafsafsafsafsafsaf");

            
//            _cli = new Cliente(nome, cpf, "masculino", email,  "9984613793", DateTime.Now.AddDays(-444), user);
//            _emp = new Empresa("teste", "PontuaAe",end , _desc, "luiz", email, "dsadsaffsa", doc, "teste", "segunda a sexta", "www", "www", "sim", "dsaf.png", "logo.png",adm);
//            _p = new Pontuacao(_emp, _cli, _ponto, DateTime.Now, 90);
            
//            var premio = new Premios(_emp,"açai",14,_desc,100,"sdadsad.png",DateTime.Now.AddDays(_p.Validade).ToString() );

//            _ponto = new Ponto(premio,0, 0.70,1,20);
            

//            _ponto.Pontuar(_ponto.SaldoPontos, _ponto.SaldoReais, _ponto.SaldoTransacao);
//            Assert.AreEqual(_ponto.IsValid
//                , true );
            

//        }
//    }
//}
