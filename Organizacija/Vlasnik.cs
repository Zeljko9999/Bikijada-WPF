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
    public class Vlasnik
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Mjesto { get; set; }

        public virtual ICollection<Bik> bikovi { get; set; }


        public static int DohvativlIDmax()
        {

            int vlIDmax = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT TOP 1 ID FROM vlasnik ORDER BY ID DESC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                vlIDmax = (int)reader.GetInt32(0);
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
            return vlIDmax;
        }

        public static int DohvativlID(string name)
        {

            int vlID = 0;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT TOP 1 ID FROM vlasnik WHERE ime = N'" + name + "' ORDER BY ID DESC";
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                vlID = (int)reader.GetInt32(0);
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
            return vlID;
        }

        public static string DohvativlIme(int ID)
        {

            string vlIme=null;

            String connectionStr = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Bikijada; Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionStr);

            try
            {

                String query1 = @"SELECT Ime FROM vlasnik WHERE ID =" + ID;
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = query1;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                vlIme = reader.GetString(0);
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
            return vlIme;
        }
    }
}
