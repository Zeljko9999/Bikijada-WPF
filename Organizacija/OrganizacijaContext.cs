using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Organizacija

{
    public class OrganizacijaContext : DbContext
{
        public DbSet<Vlasnik> Vlasnik { get; set; }
        public DbSet<Bik> Bik { get; set; }
        public DbSet<Pivac> Pivac { get; set; }
        public DbSet<Borba> Borba { get; set; }
        public DbSet<Oklada> Oklada { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database" +
            "= Bikijada; Integrated Security = True; Trusted_Connection =" +
            "true; MultipleActiveResultSets = True");
 }

    }
}
