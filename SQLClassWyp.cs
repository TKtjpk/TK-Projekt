using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;

namespace TK_Projekt
{
    public class Wyposazenie
    {
        public int Id { get; set; }
        public bool Klimatyzacja { get; set; }
        public bool Radio { get; set; }
        public bool Szyberdach { get; set; }
        public bool Nawigacja { get; set; }
        public bool CarPlay { get; set; }
    }
    internal class SQLClassWyp
    {
        public Wyposazenie Connection(int id = 1)
        {
            string connString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\tomaszkubicki\\source\\repos\\TK Projekt\\Database1.mdf; Integrated Security=True";
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                Console.WriteLine("Connected !!!");

                using (SqlCommand cmd = new SqlCommand("Wyp", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter param1 = new SqlParameter();

                    param1.ParameterName = "@id";

                    param1.SqlDbType = SqlDbType.Int;

                    param1.Value = id;

                    cmd.Parameters.Add(param1);

                    cmd.Parameters.Add("@jsonOut", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    var json = cmd.Parameters["@jsonOut"].Value.ToString();

                    var wyposazenie = new Wyposazenie();

                    try
                    {
                        List<Wyposazenie> lista = JsonSerializer.Deserialize<List<Wyposazenie>>(json);
                        wyposazenie = lista[0];
                    }
                    catch (Exception ex) { }

                    conn.Close();

                    return wyposazenie;
                }
            }
        }
    }
}
