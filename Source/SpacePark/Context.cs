using SpaceParkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class Context : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ParkingOrder> ParkingOrders { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<Starship> Starships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Ignore(b => b.Starship);
        }
    }
}
