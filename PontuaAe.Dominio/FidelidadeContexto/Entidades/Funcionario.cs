
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Domain.FidelidadeContexto.Entidades
{
    public class Funcionario 
    {
        public Funcionario()
        {

        }

        public Funcionario(int idUsuario, int idEmpresa, string nomeCompleto, string contato)
        {
            NomeCompleto = nomeCompleto;
            IdEmpresa = idEmpresa;
            IdUsuario = idUsuario;
            Contato = contato;
        }
       
        public int ID { get; private set; }
        public int IdUsuario { get; private set; }
        public int IdEmpresa { get; private set; }
        public string NomeCompleto { get; private set; }   
        public string Contato { get; private set; }   
        
    }
}
