using System;
using System.Data.Entity;
using System.Threading.Tasks;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;
using UI;

namespace SpacePark
{
    class Program
    {       
        static async Task Main(string[] args)
        {

            IFluentCustomer customer = new Customer();

            
            customer.SelectStarship().Wait();
            Starship vehicle = customer.Starship;

            var spacePort = new ParkingHouse("Space Port");
            var parkingOrder = new ParkingOrder();

            customer
                .VisitParkingHouse(spacePort)
                .SelfRegistration(vehicle, parkingOrder).Wait();

            customer
                .ParkShip(vehicle, DateTime.Now, parkingOrder) // Lägga till parkeringsordern i databasen här
                .LeavePark(DateTime.Now.AddHours(2), parkingOrder); // Total tid för parkering och slutkostnad uppdateras till databasen här


            AddSingleOrderToDatabase(parkingOrder);
            // ska slutordern vara här ?
        }
        private static void AddSingleOrderToDatabase(ParkingOrder order)
        {
            Console.WriteLine("Add order to database");

            var context = new Context();

            context.ParkingOrders.Add(order);

            Console.WriteLine("Press a key to save to database");
            Console.ReadKey();
            // DONE: Save change in database
            context.SaveChanges();
            Console.WriteLine("Order saved to database");
        }
    }
}
