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

            var test = objectOfStarships.results[0];

            var skepp = new Starship();
            skepp.Id = objectOfStarships.results.IndexOf(test);
            skepp.Name = test.name;

            Console.WriteLine(skepp.Name);

            customer.SelectStarship(objectOfStarships.results);
            //customer.SelectStarship(objectOfStarships)

            var parkingHouse = new SpacePort();
            //IFluentCustomer customer = new Customer(parkingHouse);

            var vehicle = new Starship();

            customer
                .VisitParkingHouse(parkingHouse)
                .SelfRegistration()
                .ParkShip(vehicle, DateTime.Now)
                .LeavePark(DateTime.Now.AddHours(2))
                .DisplayCreditWorthiness()
                .ReceiveInvoice();

            // Denna ger tillbaka myCustomerEnterTrueOrFalse = false
            //string inputName = "Bosse";


            // // Denna ger tillbaka myCustomerEnterTrueOrFalse = true
            string inputName = "Luke";

            
            var myCustomerEnterTrueOrFalse = await CustomerValidator.NameValidator(inputName);
          
            // Vill få in Mattias namnvalidator i min selfregistrerare men den är inte async och måste returnera void inte IFluentCustomer, ev. en task måste läggas till
            



        }

        

    }
}
