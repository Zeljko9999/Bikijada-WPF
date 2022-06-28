using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using Organizacija;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static OrganizacijaContext context;

    static void Main(string[] args)
    {
        
        PopulateDatabase();

    }


    static void PopulateDatabase()
    { 

        context = new OrganizacijaContext();

       //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
}

}