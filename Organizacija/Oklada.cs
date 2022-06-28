using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizacija
{
    public class Oklada
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public float Iznos { get; set; }

        public Bik bik { get; set; }

        public Vlasnik vlasnik { get; set; }

          
       
        public static void UbaciOkladu(Oklada okl)
        {

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr); 

            int vlID = Vlasnik.DohvativlID(okl.vlasnik.Ime);
            int bikID = Bik.DohvatibikID(okl.bik.Ime, vlID);
           
            string insert = @"INSERT INTO oklada VALUES (N'" + okl.Ime + "', " + okl.Iznos + ", " + bikID + ", " + vlID + ")";
           
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                command.CommandText = insert;
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

        public static List<Oklada> DohvatiOklade(int oklIDmax)
        {
            int pomID;
            string pomime;
            float pomiznos;
            string pombik;
            string pomvl;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connectionStr);

            List<Oklada> oklade = new List<Oklada>();

            for (int i = 1; i < oklIDmax+1; i++)
            {
                try
                {

                    string query1 = @"SELECT * FROM oklada WHERE ID=" + i;

                    connection1.Open();
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection1;
                    command1.CommandText = query1;
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {

                        reader1.Read();
                        pomID = reader1.GetInt32(0);
                        pomime = reader1.GetString(1);
                        pomiznos = reader1.GetFloat(2);
                        pombik = Bik.DohvatibikIme(reader1.GetInt32(3));
                        pomvl = Vlasnik.DohvativlIme(reader1.GetInt32(4));
                        reader1.Close();

                        Bik bik1 = new Bik()
                        {
                            Ime = pombik,
                        };

                        Vlasnik vlasnik1 = new Vlasnik()
                        {
                            Ime = pomvl,
                        };

                        Oklada oklada = new Oklada
                        {
                            ID = pomID,
                            Ime = pomime,
                            Iznos = pomiznos,
                            bik = bik1,
                            vlasnik = vlasnik1,
                           
                        };

                        oklade.Add(oklada);
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

            return oklade;
        }

        public static int DohvatioklIDmax()
        {

            int oklIDmax = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT TOP 1 ID FROM oklada ORDER BY ID DESC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                oklIDmax = (int)reader.GetInt32(0);
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
            return oklIDmax;
        }
        public static double DohvatiUkupniIznos(string imebik, string imevl)
        {

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            int vlID = Vlasnik.DohvativlID(imevl);
            int bikID = Bik.DohvatibikID(imebik, vlID);
            double UkIznos = 0;
            
                try
            {
                String query = @"SELECT SUM(Iznos) FROM oklada WHERE bikID = " + bikID + " AND vlasnikID="+ vlID;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                UkIznos = reader.GetDouble(0);
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
            
            return UkIznos;
        }
    }

}
