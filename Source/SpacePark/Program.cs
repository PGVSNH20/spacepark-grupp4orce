using System;
using System.Threading.Tasks;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;

namespace SpacePark
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IFluentCustomer customer = new Customer();

            Starship vehicle = new Starship(null, null);
            customer.SelectStarship(vehicle).Wait();

            var spacePort = new ParkingHouse("Space Port");
            var parkingOrder = new ParkingOrder();

            customer
                .VisitParkingHouse(spacePort)
                .SelfRegistration(vehicle, parkingOrder).Wait();

            customer
                .ParkShip(vehicle, DateTime.Now, parkingOrder) // Lägga till parkeringsordern i databasen här
                .LeavePark(DateTime.Now.AddHours(2), parkingOrder); // Total tid för parkering och slutkostnad uppdateras till databasen här

            // ska slutordern vara här ?
        }
    }
}
