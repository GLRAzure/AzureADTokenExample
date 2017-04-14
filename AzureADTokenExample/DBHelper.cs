using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AzureADTokenExample
{
    class DBHelper
    {
        public static void SQLDBTest(string token)
        {
            try
            {
                var sqlConnectionString = UserModeConstants.DBConnection;
                using (var conn = new SqlConnection(sqlConnectionString))
                {
                    conn.AccessToken = token;
                    conn.Open();

                    using (var cmd = new SqlCommand("SELECT SUSER_SNAME()", conn))
                    {
                        var result = cmd.ExecuteScalar();
                        Console.WriteLine("Logged into SQL as: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
