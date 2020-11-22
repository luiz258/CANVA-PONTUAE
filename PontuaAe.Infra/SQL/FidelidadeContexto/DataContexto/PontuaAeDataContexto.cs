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
        //classe nao utilizada
        public PontuaAeDataContexto()
        {

            Connection = new SqlConnection("Data Source=SQL5097.site4now.net;Initial Catalog=DB_A6977E_pontuaae;User Id=DB_A6977E_pontuaae_admin;Password=3412@Sousa");

            Connection.Open();
        }
        /// <summary>
        /// ODBC Driver 17 for SQL Serve
        /// </summary>

        /// <summary>
        ///  Server=sql5097.site4now.net Catalog=DB_A6977E_pontuaae;Persist Security Info=False;User ID=DB_A6977E_pontuaae_admin;Password=3412@Sousa;
        /// Server=tcp:db-pontuaae.database.windows.net,1433;Initial Catalog=Db-Pontuaae;Persist Security Info=False;User ID=pontuaae-sa;Password=1q2w3e4r!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
        /// </summary>
        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

