using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TK_Projekt
{
    public class Pojazd
    {
        public int Id { get; set; }
        public string MarkaSamochodu { get; set; }
        public string ModelSamochodu { get; set; }
        public string KolorSamochodu { get; set; }
        public int RokProdukcji { get; set; }
        public float Silnik { get; set; }
    }

    internal class SQLClassFull
    {
        public List<Pojazd> Connection(string marka)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("FullList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();

                    param1.ParameterName = "@marka";

                    param1.SqlDbType = SqlDbType.NChar;

                    param1.Value = marka;

                    cmd.Parameters.Add(param1);

                    SqlParameter param2 = new SqlParameter();

                    param2.ParameterName = "@model";

                    param2.SqlDbType = SqlDbType.NChar;

                    param2.Value = "";

                    cmd.Parameters.Add(param2);

                    cmd.Parameters.Add("@jsonOutput", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                    
                    //Console.WriteLine("Connection Open ! ");

                    cmd.ExecuteNonQuery();

                    string json = cmd.Parameters["@jsonOutput"].Value.ToString();

                    var pojazdy = JsonSerializer.Deserialize<List<Pojazd>>(json);

                    conn.Close();

                    return pojazdy;
                }
            }
        }
    }
}
