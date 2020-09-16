using System;
using PontuaAe.Compartilhado.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;


namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Cliente : Entidade
    {

        public Cliente()
        {

        }


        public Cliente(string contato)
        {
            Contato = contato;

        }

        public Cliente(int idUsuario, string nomeCompleto, string contato, Email email, DateTime? dataNascimento, string cidade, string sexo )
        {
            IdUsuario = idUsuario;
            NomeCompleto = nomeCompleto;
            Contato = contato;
            Email = email;
            DataNascimento = dataNascimento;
            Cidade = cidade;
            Sexo = sexo;


            AddNotifications(email.Notifications);
        }

        public Cliente(int id, string nomeCompleto, DateTime? dataNascimento, string cidade, string contato, string sexo)
        {
            IdUsuario = id;
            Contato = contato;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Cidade = cidade;

        }

        //DELETA
        //public Cliente(string telefone, string nome, int idUsuario)
        //{
        //    Telefone = telefone;
        //    Nome = nome;
        //    IdUsuario = idUsuario;
        //}
        //DELETA
        //public Cliente(string telefone, string nome, Email email, int id)
        //{
        //    Telefone = telefone;
        //    Nome = nome;
        //    Email = email;
        //    ID = id;
        //}

        //public Cliente(string telefone, int idUsuario)
        //{
        //    Telefone = telefone;
        //    IdUsuario = idUsuario;
        //}

        public int Id { get; private set; }
        public int IdUsuario { get; private set; }
        public string NomeCompleto { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public Email Email { get; private set; }
        public string Contato { get; private set; }
        public string Sexo { get; private set; }
        public Usuario Usuario { get; private set; }
        public string EstadoFidelidade { get; private set; }
        public string Cidade { get; private set; }
        public string Segmentacao { get; private set; } 
        public string SegCustomizado { get; private set; }


       









    }
}
