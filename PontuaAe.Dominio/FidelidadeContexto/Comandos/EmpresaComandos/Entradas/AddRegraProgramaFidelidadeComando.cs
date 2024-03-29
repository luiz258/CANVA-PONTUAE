﻿using FluentValidator;
using PontuaAe.Compartilhado.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Comandos.EmpresaComandos.Entradas
{
    public class AddRegraProgramaFidelidadeComando : Notifiable, IComando
    {
        public int IdEmpresa { get; set; }
        //public decimal Percentual { get; set; }
        public decimal Reais { get; set; } 
        public string Nome { get; set; } 
        public decimal PontosFidelidade { get; set; } 
        public int ValidadePontos { get; set; }  
        public int Percentual { get; set; }
        public int TipoDeProgramaFidelidade => 2;

        public bool Valida()
        {
            throw new NotImplementedException();
        }
    }
}
