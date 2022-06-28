using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Organizacija;
using Microsoft.Data.SqlClient;

class Program
{

    static OrganizacijaContext context;
    static void Main(string[] args) {


        PopulateDatabase();
    }
   
    static void PopulateDatabase() 
    {
        Vlasnik vlasnik = new Vlasnik { Ime = "Vito", Mjesto = "Sestanovac" };
        context = new OrganizacijaContext();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        //context.Vlasnik.Add(vlasnik);
        int noRows = context.SaveChanges();
        Console.WriteLine("Number of rows affected: {0}", noRows);
        Console.WriteLine("{0} Rows affected.", noRows);

       

    }
}