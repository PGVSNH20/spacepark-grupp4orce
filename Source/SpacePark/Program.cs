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
            var  spacePort = new ParkingHouse("Space Port");
            
            
            IFluentCustomer customer1 = new Customer();

   
            customer1
                .SelfRegistration().Wait();

            customer1
                .ParkShip (DateTime.Now)
                .DoingStuffOutsideParkingHousePerMinute(120);

            customer1.LeavePark(DateTime.Now.AddHours(2)); // Total tid för parkering och slutkostnad uppdateras till databasen här


            DbAccess.AddSingleOrderToDatabase(customer1.ParkingRegistration);
            DbAccess.ShowAllParkingsInDatabase();
            //ska slutordern vara här ?
        }

    }
}
