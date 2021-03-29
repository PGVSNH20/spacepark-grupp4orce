using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading.Tasks;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.DataAccess;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;


namespace SpacePark
{
    public class Program
    {
        static async Task Main(string[] args)
        {

            IFluentCustomer customer1 = new Customer();

            customer1.SelectStarship().Wait();
            Starship vehicle = customer1.Starship;

            var spacePort = new ParkingHouse("Space Port");
            var parkingOrder = new ParkingOrder();



            customer1
                .SelfRegistration(vehicle, parkingOrder).Wait();

            customer1
                .ParkShip(vehicle, DateTime.Now, parkingOrder)
                .DoingStuffOutsideParkingHousePerMinute(120);

            customer1.LeavePark(DateTime.Now.AddHours(2), parkingOrder); // Total tid för parkering och slutkostnad uppdateras till databasen här


            DbAccess.AddSingleOrderToDatabase(parkingOrder);
            DbAccess.ShowAllParkingsInDatabase();
            //ska slutordern vara här ?
        }

    }
}
