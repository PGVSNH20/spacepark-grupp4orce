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
            //var  spacePort = new ParkingHouse("Space Port");
            
            
            //IFluentCustomer customer1 = new Customer();
   
            //customer1
            //    .SelfRegistration().Wait();

            //customer1
            //    .ParkShip(DateTime.Now);

            //double parkingTimeElapsed = await customer1.ParkingTimeInMinutesSimulator(139);
            //DateTime departureTime = customer1.ParkingRegistration.ArrivalTime.AddMinutes(parkingTimeElapsed);

            //customer1.LeavePark(departureTime).Wait(); // Total tid för parkering och slutkostnad uppdateras till databasen här

            DbAccess.ShowAllParkingsInDatabase();
            //ska slutordern vara här ?
        }

    }
}
