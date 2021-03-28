using SpaceParkLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.DataAccess
{
    public class DbAccess
    {
        public static void CheckIfCustomerExistInDB(string name)
        {
            throw new NotImplementedException();
        }
        public static ParkingLot GetEmptyParkingLotsFromDB()
        {
            
            // Where(lot => lot.Id <= 10 && lot.Occupied == false)
            var context = new ParkingContext();
            var allEmptyParkingLots = context.ParkingLots.Where(lot => lot.Id <= 10 && lot.Occupied == false).ToList();
            
            Console.WriteLine("Lediga platser");
            foreach (var lot in allEmptyParkingLots)
            {
                Console.WriteLine(lot);
            }
            Console.ReadKey();

            ParkingLot emptySingle = allEmptyParkingLots.First();
            UpdateVacancyInParkinLot(emptySingle.Id, true);

            return emptySingle;
        }

        public static void UpdateVacancyInParkinLot(int lotToUpdateId, bool state)
        {

            var context = new ParkingContext();
            var parkingLotToUpdate = context.ParkingLots.Where(lot => lot.Id == lotToUpdateId).Single();

            parkingLotToUpdate.Occupied = state;
            context.SaveChanges();

        }

        public static Customer TryToGetExistingCustomer(Customer inputCustomer)
        {
            var context = new ParkingContext();

            // Ändrat från single till first
            var singleCustomer = context.Customers.Where(n => n.Name == inputCustomer.Name).FirstOrDefault();

            string name = (singleCustomer is null) ? inputCustomer.Name + "\t[Ny kund]" : singleCustomer.Name;
            Console.WriteLine("Kund heter" + name);

            if (singleCustomer != null) // singleCustomer is not null
            {

                return singleCustomer;
            }
            else
                return inputCustomer;
        }

        public static void ShowAllParkingsInDatabase()
        {
            Console.WriteLine("Fetch and show all orders");

            // Access dbset
            var context = new ParkingContext();


            //var allShips = context.Starships.ToList();
            Console.WriteLine("Press a key to print orders\n");
            Console.ReadKey();

            try
            {
                var allParkings = context.ParkingOrders.ToList();


                foreach (var parking in allParkings)
                {
                    //TimeSpan duration = parking.DepartureTime - parking.ArrivalTime;
                    Console.WriteLine(parking);
                    //Console.WriteLine(parking.Customer.Name);
                    //Console.WriteLine(parking.Starship.Name);


                }
            }
            catch (Exception)
            {

                throw new ArgumentNullException();
            }
           
           

            //var allCustomers = context.Customers.ToList();

        
            //foreach (var customer in allCustomers)
            //{
            //    Console.WriteLine($"{customer.Id}");
            //    Console.WriteLine($"{customer.Name}");
            //    Console.WriteLine($"{customer.Email}");
            //}



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
