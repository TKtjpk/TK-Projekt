using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

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
        public bool CzyMetallic { get; set; }
        public Byte[] Image { get; set; }
    }

    internal class SQLClassFull
    {
        public List<Pojazd> Connection(string marka = "", string model = "", string kolor = "", int rok = 0, float silnik = 0, bool metalic = false)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                Console.WriteLine("Connected !!!");

                using (SqlCommand cmd = new SqlCommand("FullList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();
                    SqlParameter param2 = new SqlParameter();
                    SqlParameter param3 = new SqlParameter();
                    SqlParameter param4 = new SqlParameter();
                    SqlParameter param5 = new SqlParameter();
                    SqlParameter param6 = new SqlParameter();

                    param1.ParameterName = "@marka";
                    param2.ParameterName = "@model";
                    param3.ParameterName = "@kolor";
                    param4.ParameterName = "@rok";
                    param5.ParameterName = "@silnik";
                    param6.ParameterName = "@metalic";

                    param1.SqlDbType = SqlDbType.NVarChar;
                    param2.SqlDbType = SqlDbType.NVarChar;
                    param3.SqlDbType = SqlDbType.NVarChar;
                    param4.SqlDbType = SqlDbType.Int;
                    param5.SqlDbType = SqlDbType.Decimal;
                    param6.SqlDbType = SqlDbType.Bit;

                    param1.Value = marka;
                    param2.Value = model;
                    param3.Value = kolor;
                    param4.Value = rok;
                    param5.Value = silnik;
                    param6.Value = metalic;

                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);
                    cmd.Parameters.Add(param3);
                    cmd.Parameters.Add(param4);
                    cmd.Parameters.Add(param5);
                    cmd.Parameters.Add(param6);

                    cmd.Parameters.Add("@jsonOutput", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    string json = cmd.Parameters["@jsonOutput"].Value.ToString();

                    var pojazdy = new List<Pojazd>();

                    try
                    {
                        pojazdy = JsonSerializer.Deserialize<List<Pojazd>>(json);
                    }
                    catch (Exception ex) { }

                    conn.Close();

                    return pojazdy;
                }
            }
        }
    }
}
