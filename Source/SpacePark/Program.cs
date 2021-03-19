using System;
using System.Threading.Tasks;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;

namespace SpacePark
{
    class Program
    {
        static async Task Main(string[] args)
        {
           
            var customer = new Customer();


            // Denna ger tillbaka myCustomerEnterTrueOrFalse = false
            //string inputName = "Bosse";


            // // Denna ger tillbaka myCustomerEnterTrueOrFalse = true
            string inputName = "Luke";

            
            
            var myCustomerEnterTrueOrFalse = await CustomerValidator.NameValidator(inputName);



            //Objekt med alla skepp
            var objectOfStarships = await CustomerValidator.GetAllStarships();

            



        }

        

    }
}
