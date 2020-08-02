using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PontuaAe.Infra.Repositorios
{
    public class DbConfig
    {
        public SqlConnection Connection { get; set; }

        public DbConfig()
        {
            Connection = new SqlConnection("Server=den1.mssql7.gear.host; Database=pontuaae; User ID=pontuaae; Password=Lz8Nt8mPL~!5;");
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}
