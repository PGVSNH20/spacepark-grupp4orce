using SpaceParkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.DataAccess
{
    public class ParkingContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ParkingOrder> ParkingOrders { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<Starship> Starships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Ignore(b => b.Starship); // Kan var fel här?
            //modelBuilder.Entity<ParkingOrder>().Ignore(b => b.AssignedParkingLot);


        }

        
    }
}
