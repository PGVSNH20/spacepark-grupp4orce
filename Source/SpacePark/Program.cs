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

            Starship vehicle = new Starship(0, null);
            customer.SelectStarship(vehicle).Wait();

            var spacePort = new ParkingHouse("Space Port");
            var parkingOrder = new ParkingOrder();

            customer
                .VisitParkingHouse(spacePort)
                .SelfRegistration(parkingOrder, vehicle).Wait();

            customer
                .ParkShip(vehicle, DateTime.Now)
                .LeavePark(DateTime.Now.AddHours(2));
        }
    }
}
