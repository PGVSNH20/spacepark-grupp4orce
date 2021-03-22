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

            // Objekt med alla skepp
            var objectOfStarships = await CustomerValidator.GetAllStarships();
            customer.SelectStarship(objectOfStarships.results);
            
            var vehicle = customer.Starship;

            var spacePort = new ParkingHouse("Space Port");


            customer
                .VisitParkingHouse(spacePort)
                .SelfRegistration().Wait();

            customer
                .ParkShip(vehicle, DateTime.Now)
                .LeavePark(DateTime.Now.AddHours(2));


        }



    }
}
