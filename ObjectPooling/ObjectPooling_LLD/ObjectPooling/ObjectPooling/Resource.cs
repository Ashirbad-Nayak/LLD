using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPooling
{
   public class Resource
    {
        SqlConnection sqlConnection;
        public Resource()
        {
            sqlConnection = new SqlConnection();
        }
    }
}
