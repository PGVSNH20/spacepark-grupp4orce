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

            IFluentCustomer customer = new Customer();


            customer.SelectStarship().Wait();
            Starship vehicle = customer.Starship;

            var spacePort = new ParkingHouse("Space Port");
            var parkingOrder = new ParkingOrder();

            customer
                .VisitParkingHouse(spacePort)
                .SelfRegistration(vehicle, parkingOrder).Wait();


            customer
                .ParkShip(vehicle, DateTime.Now, parkingOrder)
                .DoingStuffOutsideParkingHousePerMinute(120);

            customer.LeavePark(DateTime.Now.AddHours(2), parkingOrder); // Total tid för parkering och slutkostnad uppdateras till databasen här



            DbAccess.AddSingleOrderToDatabase(parkingOrder);
            DbAccess.ShowAllParkingsInDatabase();
            //ska slutordern vara här ?
        }

    }
}
