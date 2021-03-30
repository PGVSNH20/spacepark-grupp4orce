using SpaceParkLibrary.DataAccess;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;
using System;
using System.Threading.Tasks;


namespace SpacePark
{
    public class Program
    {
       
        static async Task Main(string[] args)
        {
            var spacePort = new ParkingHouse("Space Port");

            IFluentCustomer customer1 = new Customer(spacePort);

            customer1
                .SelfRegistration().Wait();

            customer1
                .ParkShip(DateTime.Now);

            double parkingTimeElapsed = await customer1.ParkingTimeInMinutesSimulator(72);
            DateTime departureTime = customer1.ParkingRegistration.ArrivalTime.AddMinutes(parkingTimeElapsed);

            customer1.LeavePark(departureTime).Wait(); // Total tid för parkering och slutkostnad uppdateras till databasen här

            DbAccess.ShowAllParkingsInDatabase();

        }

    }
}
