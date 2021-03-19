using System;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;

namespace SpacePark
{
    class Program
    {
        static void Main(string[] args)
        {
 
            var parkingHouse = new SpacePort();
           
            IFluentCustomer customer = new Customer(parkingHouse);
            var vehicle = new Starship();

            customer
                .SelfRegistration()
                .ParkShip(vehicle, DateTime.Now)
                .LeavePark(DateTime.Now.AddHours(2))
                .DisplayCreditWorthiness()
                .ReciveInvoice();
            
            // Tjena tjabba hallå kära Jedis!

            // Testar//MAttias

            /*Patrik Testar
             var client = new RestClient("https://swapi.dev/api/");
             var request = new RestRequest("people/", DataFormat.Json);*/
        }
    }
}
