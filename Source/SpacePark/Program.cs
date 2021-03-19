using System;
using RestSharp;
using SpacePark;
using SpaceParkLibrary;
using SpaceParkLibrary.Models;

namespace SpacePark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var customer = new Customer();

            string inputName = Console.ReadLine();
            customer.Name = inputName;
            
            // Tjena tjabba hallå kära Jedis!

            // Testar//MAttias

            /*Patrik Testar
             var client = new RestClient("https://swapi.dev/api/");
             var request = new RestRequest("people/", DataFormat.Json);*/
        }
    }
}
