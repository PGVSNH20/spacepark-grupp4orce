using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Utilities;
using System;
using System.Threading.Tasks;
using SpaceParkLibrary.DataAccess;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

namespace SpaceParkLibrary.Models
{
    public class Customer : IFluentCustomer
    {
        private ParkingHouse _parkingHouse;
        ParkingOrder parkingOrder = new ParkingOrder(); // Private??

        public Stopwatch parkingTimer = new Stopwatch();


        // Våran kund som parkerar med skepp, ankomstid och sluttid för parkering,
        // har kreditvärdighet, samt betalat faktura eller ej

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
        public Starship Starship { get; set; }
        public bool InvoicePaid { get; set; } // Vara eller icke vara?

        // Navigation Properties - 
        public List<ParkingOrder> ParkingOrders { get; set; }

        // Navigation Properties - Här vet vi att en specifik kund kan ha deltagit i flera olika parkeringar
        //public List<ParkingOrder> ParkingOrders { get; set; }


        public async Task<IFluentCustomer> SelectStarship()
        {
            byte index = 0;

            for (int i = 0; i < 4; i++)
            {
                
                var objectOfStarships = await CustomerValidator.GetAllStarships(i + 1);
                var ships = objectOfStarships.results;

                Console.WriteLine("Var god och välj ett rymdskepp");
                Console.WriteLine("------------------------------");
                foreach (var ship in ships)
                {
                    index++;
                    Console.WriteLine($"[{index}]\t{ship.name}\t{ship.length} m");

                }

                Console.WriteLine("Bläddra genom piltangent ner eller annan valfri tangent för att välja skepp");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                if (keyInfo.Key == ConsoleKey.DownArrow)    
                {
                    index = 0;
                    continue;
                }
                else
                Console.WriteLine("Välj ett skepp genom nummer: ");

                byte choosenStarship = byte.Parse(Console.ReadLine());

                Console.WriteLine("Skriv in ditt registrerings nummer: ");
                string regNr = Console.ReadLine();

                // Kunden för skriva in sitt egna regnummber
                this.Starship = new Starship(regNr, ships[choosenStarship - 1].name); // Slu7mpa fram ett eget ägarnummer
                break;
            }

            return this;
        }
        public IFluentCustomer VisitParkingHouse(ParkingHouse parkingHouse)
        {
            _parkingHouse = parkingHouse;
            Console.WriteLine("Gästen besöker: " + _parkingHouse.Name); //Måste få in namnet på P-huset här

            Console.WriteLine($"Lediga platser: {_parkingHouse.VacantParkingLots} av totala maxkapaciteten {_parkingHouse.TotalParkingsLots}");
            
            return this;
        }
        public async Task< IFluentCustomer> SelfRegistration(Starship starship, ParkingOrder parkingOrder)
        {
            this.InvoicePaid = false; // Default värde


            Console.WriteLine("Här registrerar vi Namn och skepp och är det godkända stegar vi vidare...");

            string inputName = string.Empty;
   
            bool validated = false;

            while (validated == false)
            {
                Console.WriteLine("Skriv in ditt namn: ");
                inputName = Console.ReadLine();

                validated  = await CustomerValidator.NameValidator(inputName);

                string message;
                message= (validated) ? "Valid name": "Invalid name";
				Console.WriteLine($"Valt namn: { CustomerValidator.RegisteredName}");

                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();
            }

            // Ifall loopen stegas  ur är vi fullt validerade
            // Nu vill vi testa om namnet redan registrerat i databasen

            this.Name = CustomerValidator.RegisteredName;

            var customerOut = new Customer();

            customerOut = DbAccess.TryToGetExistingCustomer(this);

            if (customerOut.Email == null)
            {
                Console.WriteLine("Skriv in din emailadress: ");
                this.Email = Console.ReadLine();
                parkingOrder.Customer = this;

            }
            else
            {
                Console.WriteLine($"Kunden {customerOut.Name} existerar redan i registret");
                this.Email = customerOut.Email;
                this.Id = customerOut.Id;
                parkingOrder.Customer = customerOut;
                //return customerOut; returnererar den här klassen eventurellt, skulle va praktiskt
              
            }
            Console.WriteLine("Nu har den checkat klart");

            // Här registreras troligtvis skeppet på något sätt

            //parkingOrder.Customer = this; // Våran klass kund och dens ifyllda propeties vi nyss satt åker in i parkeringsorderns kundinfo

            // TODO: Koppla upp oss till DB för att kontrolllera om registrerad person redan finns i kundregistret
            //DataAccess.CheckIfCustomerExistInDB(this.Name);

            //Om kunden inte existerar
            //Lägg till kunden till databasen
            


            return this;
        }

     

        public IFluentCustomer ParkShip( Starship vehicle, DateTime arrivalTime, ParkingOrder parkingOrder)
        {
            Console.WriteLine("Här tilldelar vi platsnummer och registrerar det i databasen...");

            // TODO: koppla upp oss till databasen och hämta en ledigplats
            Console.WriteLine("Här hämtar vi id på parkeringsplats");
            parkingOrder.AssignedParkingLotId = DataAccess.DbAccess.GetEmptyParkingLotsFromDB().Id;  //Tilldelar ledig plats this._parkingHouse.GetEmptyParkingLot()

            Console.WriteLine("Tilldelad plats     " + parkingOrder.AssignedParkingLotId);
            parkingOrder.Starship = vehicle;
            parkingOrder.ArrivalTime = arrivalTime;


            //Console.WriteLine($"Välkommen {Name}!");
            //Console.WriteLine($"Din {vehicle.Name} är parkerad på plats {parkingOrder.AssignedParkingLotId} och därmed är den klassas som occupied: {parkingOrder.AssignedParkingLotId}");



            //Console.WriteLine($"Antal lediga platser för nuvarande är: {_parkingHouse.VacantParkingLots}");

            //Console.WriteLine("Här startas fejklockan...");

            this.parkingTimer.Start();

            return this;
        }

        public IFluentCustomer DoingStuffOutsideParkingHousePerMinute(int minutes)
        {
            double seconds = (minutes / 60) * (60 * 60);
            Console.WriteLine($"Gästen gör nått annat *tra-tralalla la-la i {seconds} i millsekunder");
            Thread.Sleep((int)seconds);
            return this;
        }


        public IFluentCustomer LeavePark(DateTime departureTime, ParkingOrder parkingOrder)
        {
            Console.WriteLine("Här beslutar vi att hämta bilen och fejklockan stoppas...");


            parkingTimer.Stop();
            TimeSpan elapsedParkingTime = parkingTimer.Elapsed;
            Console.WriteLine("ParkingTid i sekunder" + elapsedParkingTime.Seconds);

            DateTime elapsedParkingTimeInHours = parkingOrder.ArrivalTime.AddHours(elapsedParkingTime.Seconds);
            parkingOrder.DepartureTime = elapsedParkingTimeInHours;

            Console.WriteLine("Id för plats ska uppdateras:      " + parkingOrder.AssignedParkingLotId);
            DbAccess.UpdateVacancyInParkinLot(parkingOrder.AssignedParkingLotId, false);


            //Console.WriteLine("Är vi kreditvärdiga så genereras en faktura baserad på ankomsttid och avgångstid, adress lämnas här ev. ...");
            //Console.WriteLine("En ledig plats registreras som öppen i P-huset...");
            // Parking lots uppdateras och  uppvisar totala antalet lediga platser efter gästen försvunnit

            return this;
        }
        public IFluentCustomer DisplayCreditWorthiness()
        {
            Console.WriteLine("Kreditvärdighet måste alltid kollas innan gästen lämnar...");
            return this;
        }

        public IFluentCustomer ReceiveInvoice()
        {
            Console.WriteLine("InvoiceTracker skickar faktura till kunden baserad på planet te x");
            return this;
        }


        public override string ToString()
        {
            return this.Id.ToString(); 
        }
    }
}
