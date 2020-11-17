using PontuaAe.Compartilhado.DbConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.FidelidadeContexto.DataContexto
{
    public class PontuaAeDataContexto : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public PontuaAeDataContexto()
        {
            Connection = new SqlConnection("Server=db-pontuaae.database.windows.net,1433;Database=Db-Pontuaae;User ID=pontuaae-sa;Password=1q2w3e4r!@#$;Trusted_Connection=False;Encrypt=True;");
            Connection.Open();
        }
        /// <summary>
        /// ODBC Driver 17 for SQL Serve
        /// </summary>

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

