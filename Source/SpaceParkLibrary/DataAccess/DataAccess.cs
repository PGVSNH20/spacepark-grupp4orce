using SpaceParkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.DataAccess
{
    public class DataAccess
    {
        public static void CheckIfCustomerExistInDB(string name)
        {
            throw new NotImplementedException();
        }
        public static void GetParkingLotsFromDB()
        {
            throw new NotImplementedException();
        }

        public static void ShowAllParkingsInDatabase()
        {
            Console.WriteLine("Fetch and show all orders");

            // Access dbset
            var context = new ParkingContext();

            // DONE: Make query och få ut lista

            var allCustomers = context.Customers.ToList();
            var allShips = context.Starships.ToList();


            var allParkings = context.ParkingOrders.ToList();

            //(from b in db.Blogs
            // orderby b.Name
            // select b)

            Console.WriteLine();

            Console.WriteLine("Press a key to print orders");
            Console.ReadKey();
            foreach (var customer in allCustomers)
            {
                Console.WriteLine($"{customer.Id}");
                Console.WriteLine($"{customer.Name}");
                Console.WriteLine($"{customer.Email}");
            }

            Console.ReadKey();

            foreach (var parking in allParkings)
            {
                //TimeSpan duration = parking.DepartureTime - parking.ArrivalTime;
                Console.WriteLine(parking);
                Console.WriteLine(parking.StarshipId.Name);
                
            }

        }

        public static void AddSingleOrderToDatabase(ParkingOrder order)
        {
            Console.WriteLine("Add order to database");

            var context = new ParkingContext();

            context.ParkingOrders.Add(order);

            Console.WriteLine("Press a key to save to database");
            Console.ReadKey();
            // DONE: Save change in database
            context.SaveChanges();
            Console.WriteLine("Order saved to database");
        }
    }
}
