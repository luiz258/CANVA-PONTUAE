using FluentValidator;
using FluentValidator.Validation;
using PontuaAe.Dominio.FidelidadeContexto.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace PontuaAe.Dominio.FidelidadeContexto.Entidades
{
    public class ConfiguracaoPontos : Notifiable
    {
        public ConfiguracaoPontos()
        {
                
        }

        //metodo para criar programa por pontos e valor gasto
        public ConfiguracaoPontos(string nome, int idEmpresa, decimal pontosFidelidade, decimal reais, int validadePontos, int percentual, int tipoDeProgramaFidelidade)
        {
      
            Nome = nome;
            IdEmpresa = idEmpresa;
            Reais = reais;
            PontosFidelidade = pontosFidelidade;            
            ValidadePontos = validadePontos;
            Percentual = percentual;
            TipoDeProgramaFidelidade = tipoDeProgramaFidelidade;

        }

        //criar programa por cashback
        public ConfiguracaoPontos(string nome, int idEmpresa, decimal reais, int percentual)
        {

            Nome = nome;
            IdEmpresa = idEmpresa;
            Reais = reais;           
            Percentual = percentual;

        }


        //public ConfiguracaoPontos( decimal pontosFidelidade, decimal reais, int validadePontos)            DELETA ESTE BLOCO
        //{

        //    Reais = reais;
        //    PontosFidelidade = pontosFidelidade;
        //    ValidadePontos = validadePontos;

        //}

        public ConfiguracaoPontos(int idEmpresa)
        {
            IdEmpresa = idEmpresa;
        }

        public int IdEmpresa { get;  set; } 
        public string Nome { get;  set; } 
        public decimal Reais { get;  set; } 
        public decimal PontosFidelidade { get;  set; }  
        public int ValidadePontos { get;  set; } 
        public int TipoDeProgramaFidelidade { get;  set; } 
        public int Percentual { get;  set; } 

    }
}
