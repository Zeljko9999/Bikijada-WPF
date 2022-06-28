using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;

namespace Organizacija
{
    public class Borba
    {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public static List<BorbaPom> DohvatiBorbu(int brbIDmin, int brbIDmax)
        {
            int pomID;
            DateTime pomvrijeme;
            string pomime1;
            string pomkat;
            string pomime2;
            string pomvl1;
            string pommj1;
            string pomvl2;
            string pommj2;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connectionStr);

            List<BorbaPom> borbe = new List<BorbaPom>();

            for (int i = brbIDmin; i < brbIDmax + 1; i++)
            {
                try
                {

                    string query1 = @"SELECT * FROM borba WHERE ID=" + i;

                    connection1.Open();
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection1;
                    command1.CommandText = query1;
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        reader1.Read();
                        pomID = reader1.GetInt32(0);
                        pomvrijeme = reader1.GetDateTime(1);
                        reader1.Close();

                        string query2 = @"SELECT * FROM bik WHERE borbaID=" + pomID + "AND vlasnikID=" + ((pomID * 2) - 1);
                        command1.CommandText = query2;
                        reader1 = command1.ExecuteReader();
                        reader1.Read();
                        pomime1 = reader1.GetString(5);
                        pomkat = reader1.GetString(1);
                        reader1.Close();

                        string query3 = @"SELECT * FROM bik WHERE borbaID=" + pomID + "AND vlasnikID=" + (pomID * 2);
                        command1.CommandText = query3;
                        reader1 = command1.ExecuteReader();
                        reader1.Read();
                        pomime2 = reader1.GetString(5);
                        reader1.Close();

                        string query4 = @"SELECT * FROM vlasnik WHERE ID=" + ((pomID * 2) - 1);
                        command1.CommandText = query4;
                        reader1 = command1.ExecuteReader();
                        reader1.Read();
                        pomvl1 = reader1.GetString(1);
                        pommj1 = reader1.GetString(2);
                        reader1.Close();

                        string query5 = @"SELECT * FROM vlasnik WHERE ID=" + (pomID * 2);
                        command1.CommandText = query5;
                        reader1 = command1.ExecuteReader();
                        reader1.Read();
                        pomvl2 = reader1.GetString(1);
                        pommj2 = reader1.GetString(2);
                        reader1.Close();


                        BorbaPom borba = new BorbaPom
                        {
                            bID = pomID,
                            vrijeme = pomvrijeme,
                            ime1 = pomime1,
                            kategorija = pomkat,
                            ime2 = pomime2,
                            vlasnik1 = pomvl1,
                            mjesto1 = pommj1,
                            vlasnik2 = pomvl2,
                            mjesto2 = pommj2
                        };

                        borbe.Add(borba);
                    }

                    else
                    {
                        reader1.Close();
                    }
                }

                catch (SqlException x)
                {
                    Console.WriteLine(x.Message);
                }
                catch (Exception x)
                {
                    Console.WriteLine(x.Message);
                }
                finally
                {
                    connection1.Close();

                }

            }

            return borbe;
        }


        public static int DohvatibrbIDmin()
        {

            int brbIDmin = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT TOP 1 ID FROM borba ORDER BY ID ASC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                brbIDmin = (int)reader.GetInt32(0);
                reader.Close();

            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                connection.Close();

            }
            return brbIDmin;
        }


        public static int DohvatibrbIDmax()
        {

            int brbIDmax = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT TOP 1 ID FROM borba ORDER BY ID DESC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                brbIDmax = (int)reader.GetInt32(0);
                reader.Close();

            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            finally
            {
                connection.Close();

            }
            return brbIDmax;
        }

        public static void UbaciBorbu(BorbaPom b) 
            {

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            string insert1 = @"INSERT INTO borba VALUES ('" + b.vrijeme.ToString("yyyy-MM-dd HH:mm:ss") + "')";

            string insert2 = @"INSERT INTO Vlasnik VALUES (N'" + b.vlasnik1 + "', N'" + b.mjesto1 + "') " +
            "INSERT INTO Vlasnik VALUES (N'" + b.vlasnik2 + "', N'" + b.mjesto2 + "')";

            try
            {

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = insert1;
                command.ExecuteNonQuery();

                command.CommandText = insert2;
                command.ExecuteNonQuery();

                int brbID = Borba.DohvatibrbIDmax();
                int vlID = Vlasnik.DohvativlIDmax();

                string insert3 = @"INSERT INTO Bik VALUES (N'" + b.kategorija + "', N'" + b.ime1 + "', " + brbID + ", " + (vlID - 1) + ", N'" + b.ime1 + "')" +
                "INSERT INTO Bik VALUES (N'" + b.kategorija + "', N'" + b.ime2 + "', " + brbID + ", " + vlID + ", N'" + b.ime2 + "')";
                command.CommandText = insert3;
                command.ExecuteNonQuery();

            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            
            finally
            {
                connection.Close();
            }
        }

        public static void IzbrišiBorbu(int id) 
        {
            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            string insert1 = @"DELETE Borba WHERE ID =" + id;
            string read1 = @"SELECT TOP 1 VlasnikID FROM bik WHERE borbaID="+ id + "ORDER BY ID ASC";
            string read2 = @"SELECT TOP 1 VlasnikID FROM bik WHERE borbaID=" + id + "ORDER BY ID DESC";

            try
            {

                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = read1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                int vlID1 = (int)reader.GetInt32(0);
                reader.Close();

                command.CommandText = read2;
                reader = command.ExecuteReader();
                reader.Read();
                int vlID2 = (int)reader.GetInt32(0);
                reader.Close();

                
                command.CommandText = insert1;
                command.ExecuteNonQuery();

                string insert2 = @"DELETE Vlasnik WHERE ID =" + vlID1;
                command.CommandText = insert2;
                command.ExecuteNonQuery();

                string insert3 = @"DELETE Vlasnik WHERE ID =" + vlID2;
                command.CommandText = insert3;
                command.ExecuteNonQuery();

            }
            catch (SqlException x)
            {
                Console.WriteLine(x.Message);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }

            finally
            {
                connection.Close();
            }

        }
    }

  
public class BorbaPom
{
    public int bID;
    public string vlasnik1;
    public string mjesto1;
    public string ime1;
    public string kategorija;
    public DateTime vrijeme;
    public string vlasnik2;
    public string mjesto2;
    public string ime2;
}

}