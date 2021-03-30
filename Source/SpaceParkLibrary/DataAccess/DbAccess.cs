using SpaceParkLibrary.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkLibrary.DataAccess
{
    public class DbAccess
    {
        public static void CheckIfCustomerExistInDB(string name)
        {
            throw new NotImplementedException();
        }
        public static void RegistrateCustomerIntoDB(Customer newCustomer)
        {
            var context = new ParkingContext();
            context.Customers.Add(newCustomer);
            context.SaveChanges();

        }
        public static int GetCustomerId(string customerName)
        {
            var context = new ParkingContext();

            var newCustomer = context.Customers.Where(n => n.Name == customerName).FirstOrDefault();
            return newCustomer.Id;

        }
        public static void RegistrateStarshipIntoDB(Starship newShip)
        {
            var context = new ParkingContext();
            context.Starships.Add(newShip);
            context.SaveChanges();

        }
        public static int GetStarshipId(string shipName)
        {
            var context = new ParkingContext();

            var newShip = context.Starships.Where(n => n.Name == shipName).FirstOrDefault();

            return newShip.Id;

        }
        public static ParkingLot GetEmptyParkingLotsFromDB()
        {

            var context = new ParkingContext();
            var allEmptyParkingLots = context.ParkingLots.Where(lot => lot.Occupied == false).ToList();

            Console.WriteLine("\nLediga platser");
            Console.WriteLine("----------------------------------------------------------------------------------------");
            foreach (var lot in allEmptyParkingLots)
            {
                Console.WriteLine(lot);
            }
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine();
            ParkingLot emptySingle = allEmptyParkingLots.First();
            UpdateVacancyInParkinLot(emptySingle.Id, true);

            context.SaveChanges();
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

            if (singleCustomer != null) // singleCustomer is not null
            {

                return singleCustomer;
            }
            else
                return inputCustomer;
        }


        public static void ShowAllParkingsInDatabase()
        {

            Console.WriteLine("\nFetch and show all orders");

            // Access dbset
            var context = new ParkingContext();

            Console.WriteLine("Press a key to print orders\n");
            Console.ReadKey();

            try
            {
                var allCustomers = context.Customers;//.ToList();
                var allParkings = context.ParkingOrders;// .ToList();
                var allShips = context.Starships;

                var dataDb = (from cust in allCustomers
                              join park in allParkings
                              on cust.Id equals park.CustomerId
                              join ship in allShips
                              on park.StarshipId equals ship.Id
                              select new
                              {
                                  Id = cust.Id,
                                  Name = cust.Name,
                                  Ship = ship.Name,
                                  ShipId = park.StarshipId,
                                  Email = cust.Email,
                                  ParkingOrderId = park.Id,
                                  ParkIngLot = park.AssignedParkingLotId,
                                  Arrival = park.ArrivalTime,
                                  Departure = park.DepartureTime,
                                  Fee = park.ParkingFee
                              }).ToList();


               
                foreach (var parking in allParkings)
                {

                    Console.WriteLine(parking);

                }
                Console.WriteLine();
                Console.WriteLine("| KundID | Namn | SkeppID | Skepp | ParkeringsId | Ankomstid | Avgångstid | Avgift |");
                foreach (var cust in dataDb)
                {
                 
                    Console.WriteLine($"{cust.Id} - {cust.Name} - {cust.Email} - {cust.ShipId} - {cust.Ship}  - " +
                        $"{cust.ParkingOrderId} - {cust.Arrival} - {cust.Departure} - {cust.Fee}");

                }


            }
            catch (Exception)
            {

                throw new ArgumentNullException();
            }

        }
        public static bool ValidateCustomerCreditWorthiness(int customerID)
        {
            var context = new ParkingContext();

            var customer = context.Customers.Where(x => x.Id == customerID).FirstOrDefault();

            bool invoiceNotPaid = customer.InvoicePaid == false;
            var num = context.ParkingOrders.Where(x => x.CustomerId == customerID).Count();

            if (invoiceNotPaid && num > 1)
            {
                Console.WriteLine("Din senaste faktura är inte betald. Gravity Lock initieras.\n");
                return false;
            }
            else
            {
                Console.WriteLine("Tack och välkommen åter!\n");
                return true;

            }

        }

        public async Task SendInvoiceThroughMail(int customerID)
        {
            var context = new ParkingContext();

            var customer = context.Customers.Where(x => x.Id == customerID).FirstOrDefault();
            var email = customer.Email;

            Console.WriteLine("\nRäkning skickad till " + email);
        }

        public static void AddSingleOrderToDatabase(ParkingOrder order)
        {

            Console.WriteLine("\nAdd order to database");
            var context = new ParkingContext();
            context.ParkingOrders.Add(order);

            Console.WriteLine("Press a key to save to database");
            Console.ReadKey();
            // DONE: Save change in database
            context.SaveChanges();
            Console.WriteLine("\nOrder saved to database");
        }

        public static int GetAmountOfEmptyParkingLots()
        {
            var context = new ParkingContext();

            var availableParkingLots = context.ParkingLots.Where(x => x.Occupied == false).Count();
            context.SaveChanges();
            return availableParkingLots;


        }
    }
}
