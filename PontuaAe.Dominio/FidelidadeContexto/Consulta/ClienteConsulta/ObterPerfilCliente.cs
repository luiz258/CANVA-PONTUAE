﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Dominio.FidelidadeContexto.Consulta.ClienteConsulta
{
    
    public class ObterPerfilCliente
    {
        public int ID { get; set; }
        public int IdUsuario { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Contato { get; private set; }


    }
}
