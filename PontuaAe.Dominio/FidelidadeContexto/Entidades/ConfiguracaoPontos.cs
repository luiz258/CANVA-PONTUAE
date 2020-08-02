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
        public ConfiguracaoPontos(int idEmpresa, int percentual)
        {
            IdEmpresa = idEmpresa;
            Percentual = percentual;
            TipoProgramaFidelidade = 1;

        }

        public ConfiguracaoPontos(string nome, int idEmpresa, decimal pontosFidelidade, decimal reais, int validadePontos)
        {
            Nome = nome;
            IdEmpresa = idEmpresa;
            Reais = reais;
            PontosFidelidade = pontosFidelidade;            
            ValidadePontos = validadePontos;
            TipoProgramaFidelidade = 2;


        }

        public ConfiguracaoPontos( decimal pontosFidelidade, decimal reais, int validadePontos)
        {
            
            Reais = reais;
            PontosFidelidade = pontosFidelidade;
            ValidadePontos = validadePontos;

        }

        public int IdEmpresa { get;  set; } 
        public string Nome { get;  set; } 
        public decimal Reais { get;  set; } 
        public decimal PontosFidelidade { get;  set; }  
        public int ValidadePontos { get;  set; } 
        public int TipoProgramaFidelidade { get;  set; } 
        public int Percentual { get;  set; } 
        //public decimal PontosInicial { get; private set; } 

    }
}
