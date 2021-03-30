using SpaceParkLibrary.DataAccess;
using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Utilities;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class Customer : IFluentCustomer
    {
        private ParkingHouse _parkingHouse;


        public Stopwatch parkingTimer = new Stopwatch();

        public Customer()
        {

        }
        public Customer(ParkingHouse vistingParkingHouse)
        {
            this._parkingHouse = vistingParkingHouse;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ParkingOrder ParkingRegistration { get; set; } = new ParkingOrder();
        public Starship Starship { get; set; } = new Starship();
        public bool InvoicePaid { get; set; } // Vara eller icke vara?

        // Navigation Properties - 
        //public List<ParkingOrder> ParkingOrders { get; set; }

        // Navigation Properties - Här vet vi att en specifik kund kan ha deltagit i flera olika parkeringar
        //public List<ParkingOrder> ParkingOrders { get; set; }


        public async Task<IFluentCustomer> SelfRegistration()
        {
            this.InvoicePaid = false; // Default värde

            Console.WriteLine($"Välkommen till rymdparkeringshuset {_parkingHouse.Name}!");
            Console.WriteLine("=========================================================================================\n");
            Console.WriteLine($"Just nu har vi {ParkingHouse.AmountOfEmptylots} av totalt {ParkingHouse.TotalParkingsLots} platser.\n");
            Console.WriteLine("Var vänlig och registrera ditt namn och rymdskeppet du önskar parkera.\n");

            string inputName = string.Empty;
            bool validated = false;

            while (validated == false)
            {
                Console.WriteLine("Skriv in ditt namn: ");
                inputName = Console.ReadLine();

                validated = await CustomerValidator.NameValidator(inputName);

                string message;
                message = (validated) ? "Valid name" : "Invalid name";
                Console.WriteLine($"Valt namn: { CustomerValidator.RegisteredName}");

                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();
            }

            this.Name = CustomerValidator.RegisteredName;


            var customerOut = new Customer();

            customerOut = DbAccess.TryToGetExistingCustomer(this);

            if (customerOut.Email == null)
            {
                Console.Write("Skriv in din emailadress: ");
                this.Email = Console.ReadLine();
                DbAccess.RegistrateCustomerIntoDB(customerOut);
            }
            else
            {
                Console.WriteLine($"Kunden {customerOut.Name} existerar redan i registret");

            }


            var customerID = DbAccess.GetCustomerId(customerOut.Name);

            Console.WriteLine($"\nNamnvalidering godkänd. Du har KundId: {customerID}");
            this.ParkingRegistration.CustomerId = customerID;

            this.Starship = await RegistrateStarship();


            DbAccess.RegistrateStarshipIntoDB(Starship);
            this.ParkingRegistration.StarshipId = DbAccess.GetStarshipId(Starship.Name);

            Console.WriteLine($"Registreringen är nu slutförd för {Name} och skeppet som tilldelats ID-nummer: {Starship}.\n");
            Console.WriteLine();
            return this;
        }
        public async Task<Starship> RegistrateStarship()
        {
            int index = 0;
            int choosenStarship = 0;
            double shipLenght;
            string regNr;

            for (int i = 0; i < 4; i++)
            {

                var objectOfStarships = await CustomerValidator.GetAllStarships(i + 1);
                var ships = objectOfStarships.results;

                Console.WriteLine("\nVar god och registrera ditt rymdskepp.");
                Console.WriteLine("----------------------------------------------------------------------------------------");
                foreach (var ship in ships)
                {
                    const string format = "[{0,2}]{1,50}{2,10}m";

                    index++;
                    Console.WriteLine(string.Format(format, index, ship.name, ship.length));

                }
                Console.WriteLine("----------------------------------------------------------------------------------------\n");
                Console.WriteLine("Bläddra genom nedåt- eller uppåtpilen för att navigera bland förvalda skepp.\n");
                Console.WriteLine("Tryck på enter för att välja skepp.");
                Console.WriteLine( );
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.DownArrow && i != ships.Count - 1)
                {
                    index = 0;
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.UpArrow && i != 0)
                {
                    index = 0;
                    i--;
                    continue;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    Console.Write("Välj skepp genom index: ");
                    choosenStarship = int.Parse(Console.ReadLine());
                }
                else { index = 0; i = 0; continue; } // Default för felnavigering

                string length = ships[choosenStarship - 1].length;
                if (length.Contains(','))
                {
                    length = length.Remove(length.IndexOf(','), 1);
                }

                shipLenght = double.Parse(length, CultureInfo.InvariantCulture);


                if (GateKeeper.IsStarshipToLongForParkinglot(ships[choosenStarship - 1].name, shipLenght) == false)
                {
                    index = 0;
                    i--;
                    continue;
                }


                do
                {
                    Console.WriteLine("Skriv in ditt registreringsnummer: ");
                    regNr = Console.ReadLine();

                } while (GateKeeper.IsRegistrationNumberValid(regNr) == false);


                var newStarship = new Starship(regNr, ships[choosenStarship - 1].name);// Slu7mpa fram ett eget ägarnummer
                return newStarship;
            }
            return null;

        }

        public IFluentCustomer ParkShip(DateTime arrivalTime)
        {
            Console.WriteLine($"{ParkingHouse.AmountOfEmptylots} antal lediga platser nu tillgängliga.\n");
            this.ParkingRegistration.AssignedParkingLotId = DataAccess.DbAccess.GetEmptyParkingLotsFromDB().Id;  //Tilldelar ledig plats this._parkingHouse.GetEmptyParkingLot()

            Console.WriteLine($"Tilldelad parkeringsplats: {ParkingRegistration.AssignedParkingLotId}");
            Console.WriteLine($"{ParkingHouse.AmountOfEmptylots} antal lediga platser nu tillgängliga\n");
            this.ParkingRegistration.ArrivalTime = arrivalTime;
            Console.WriteLine("Parkeringens starttid påbörjas:" + ParkingRegistration.ArrivalTime);
            this.parkingTimer.Start();

            return this;
        }

        public async Task<double> ParkingTimeInMinutesSimulator(int minutes)
        {
            double simulatedTimeInMilliSec = (minutes * 100);
            Console.WriteLine($"Gästen gör nått annat. Var god vänta *tra-la-la lalalalalalaaaaaa*");
            Thread.Sleep((int)simulatedTimeInMilliSec);

            return simulatedTimeInMilliSec / 100;
        }


        public async Task<IFluentCustomer> LeavePark(DateTime departureTime)
        {
            parkingTimer.Stop();
            this.ParkingRegistration.DepartureTime = departureTime;
            TimeSpan totalMinutesElapsed = departureTime - this.ParkingRegistration.ArrivalTime;

            TimeSpan elapsedParkingTime = parkingTimer.Elapsed;
            Console.WriteLine("Parkeringstid i verkliga sekunder: " + elapsedParkingTime.Seconds);

            decimal invoiceSum = GateKeeper.CalculateParkingFee(totalMinutesElapsed.TotalHours);
            this.ParkingRegistration.ParkingFee = invoiceSum;

            Console.WriteLine($"Totalparkeringtid: {totalMinutesElapsed.Hours}:{totalMinutesElapsed.Minutes} h");
            Console.WriteLine();

            if (DbAccess.ValidateCustomerCreditWorthiness(ParkingRegistration.CustomerId) == false)
                Console.WriteLine("Du är tvungen att betala kontant för att kunna lämna parkeringen annars beslagtags ditt fordon.");


            var access = new DbAccess();
            await access.SendInvoiceThroughMail(ParkingRegistration.CustomerId);


            Console.WriteLine($"\nPlats {ParkingRegistration.AssignedParkingLotId} är nu ledig igen! ");
            DbAccess.UpdateVacancyInParkinLot(ParkingRegistration.AssignedParkingLotId, false);

            Console.WriteLine($"Just nu är {ParkingHouse.AmountOfEmptylots} platser lediga av totalt {ParkingHouse.TotalParkingsLots}.\n");

            Console.WriteLine();

            DbAccess.AddSingleOrderToDatabase(this.ParkingRegistration);

            return this;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
