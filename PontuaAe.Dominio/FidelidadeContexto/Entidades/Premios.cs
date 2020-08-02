
using PontuaAe.Dominio.FidelidadeContexto.ObjetoValor;

using System;
using System.Collections.Generic;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class Premios
    {
        public Premios()
        {

        }
        public Premios(int id, int idEmpresa, string nome, Descricao descricao, decimal pontosNescessario, decimal qtdPremio, string imagem)
        {
            ID = id;
            IdEmpresa = idEmpresa;
            Nome = nome;
            PontosNecessario = pontosNescessario;
            Descricao = descricao;
            Quantidade = qtdPremio;
            Imagem = imagem;
        }
        public Premios(int idEmpresa, string nome, Descricao descricao, decimal pontosNescessario)
        {
       
            IdEmpresa = idEmpresa;
            Nome = nome;
            PontosNecessario = pontosNescessario;
            Descricao = descricao;
          
          
        }
        public Premios(int id, int idEmpresa, string nome, Descricao descricao, decimal pontosNescessario)
        {
            ID = id;
            IdEmpresa = idEmpresa;
            Nome = nome;
            PontosNecessario = pontosNescessario;
            Descricao = descricao;


        }

        public int ID { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCliente{ get; set; }
        public string  Nome { get; set; }
        public decimal PontosNecessario { get; set; }
        public Descricao Descricao { get; private set; }
        public decimal Quantidade { get; private set; }
        public string Imagem { get; private set; }
        public string Validade { get; private set; }
       
        
    }
}
