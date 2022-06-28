using Microsoft.Data.SqlClient;
using Organizacija;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizacija
{
    public abstract class Životinja
    {
        public string Ime { get; set; }
    }


    public class Bik : Životinja
    {
        public int ID { get; set; }
      //  public string Ime { get; set; }
        public string Kategorija { get; set; }
        public string Slika { get; set; }

        public Borba borba { get; set; }


        public static int DohvatibikID(string name, int vlID)
        {

            int bikID = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT ID FROM bik WHERE ime = N'" + name + "' AND VlasnikID =" + vlID;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bikID = (int)reader.GetInt32(0);
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
            return bikID;
        }


        public static string DohvatibikIme(int ID)
        {

            string bikIme = null;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT Ime FROM bik WHERE ID =" + ID;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                bikIme = reader.GetString(0);
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
            return bikIme;
        }

        public static string DohvatiSliku(string slika)
        {
            string slika1 = null;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT Slika FROM bik WHERE Ime = N'" + slika+"'";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                slika1 = reader.GetString(0);
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
            return slika1;
        }

        }
    

public class Pivac : Životinja
{
    public int ID { get; set; }
    //public string Ime { get; set; }
    public string Vlasnik { get; set; }

    public static void UbaciPivca(Pivac pvc)
    {

        String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
        SqlConnection connection = new SqlConnection(connectionStr);

        string insert = @"INSERT INTO pivac VALUES (N'" + pvc.Vlasnik + "', N'" + pvc.Ime + "')";

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
        public static List<Pivac> DohvatiPivce(int pvcIDmax)
        {
            int pomID;
            string pomime;
            string pomvl;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection1 = new SqlConnection(connectionStr);

            List<Pivac> pivci = new List<Pivac>();

            for (int i = 1; i < pvcIDmax + 1; i++)
            {
                try
                {

                    string query1 = @"SELECT * FROM pivac WHERE ID=" + i;

                    connection1.Open();
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection1;
                    command1.CommandText = query1;
                    SqlDataReader reader1 = command1.ExecuteReader();

                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        pomID = reader1.GetInt32(0);
                        pomime = reader1.GetString(2);
                        pomvl = reader1.GetString(1);
                        reader1.Close();

                        Pivac pivac = new Pivac
                        {
                            ID = pomID,
                            Ime = pomime,
                            Vlasnik = pomvl,
                        };

                        pivci.Add(pivac);
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

            return pivci;
        }

        public static int DohvatipvcIDmax()
        {

            int pvcIDmax = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {
                String query1 = @"SELECT TOP 1 ID FROM pivac ORDER BY ID DESC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                pvcIDmax = (int)reader.GetInt32(0);
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
            return pvcIDmax;
        }
    }
}

