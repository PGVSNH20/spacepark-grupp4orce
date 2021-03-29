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
        public ParkingOrder ParkingRegistration { get; set; } = new ParkingOrder();
        public Starship Starship { get; set; } = new Starship();
        public bool InvoicePaid { get; set; } // Vara eller icke vara?

        // Navigation Properties - 
        //public List<ParkingOrder> ParkingOrders { get; set; }

        // Navigation Properties - Här vet vi att en specifik kund kan ha deltagit i flera olika parkeringar
        //public List<ParkingOrder> ParkingOrders { get; set; }


        
        public IFluentCustomer VisitParkingHouse(ParkingHouse parkingHouse)
        {
            _parkingHouse = parkingHouse;
            Console.WriteLine("Gästen besöker: " + _parkingHouse.Name); //Måste få in namnet på P-huset här

            Console.WriteLine($"Lediga platser: {_parkingHouse.VacantParkingLots} av totala maxkapaciteten {_parkingHouse.TotalParkingsLots}");
            
            return this;
        }
        public async Task<IFluentCustomer> SelfRegistration()
        {
            this.InvoicePaid = false; // Default värde

            Console.WriteLine("Välkommen til parkeringshuset!");
            Console.WriteLine("Var vänlig och registrera ditt namn och rymdskeppet du önskar parkera");

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
                DbAccess.RegistrateCustomerIntoDB(customerOut);
            }
            else
            {
                Console.WriteLine($"Kunden {customerOut.Name} existerar redan i registret");
 
            }
            Console.WriteLine("Nu har den checkat klart");
            
            //
           

            var customerID = DbAccess.GetCustomerId(customerOut.Name);
            Console.WriteLine("Kund Id" + customerID);
            this.ParkingRegistration.CustomerId = customerID;

            this.Starship = await RegistrateStarship();
            Console.WriteLine("skepp heter i reg" + this.Starship.Name);

            DbAccess.RegistrateStarshipIntoDB(Starship);
            this.ParkingRegistration.StarshipId = DbAccess.GetStarshipId(Starship.Name);

            Console.WriteLine($"Registreringen är slutförd för {Name} och {Starship}.");
            // Här registreras troligtvis skeppet på något sätt

            //parkingOrder.Customer = this; // Våran klass kund och dens ifyllda propeties vi nyss satt åker in i parkeringsorderns kundinfo

            // TODO: Koppla upp oss till DB för att kontrolllera om registrerad person redan finns i kundregistret
            //DataAccess.CheckIfCustomerExistInDB(this.Name);

            //Om kunden inte existerar
            //Lägg till kunden till databasen



            return this;
        }
        public async Task<Starship> RegistrateStarship()
        {
            byte index = 0;

            for (int i = 0; i < 4; i++)
            {

                var objectOfStarships = await CustomerValidator.GetAllStarships(i + 1);
                var ships = objectOfStarships.results;

                Console.WriteLine("Var god och registrera ditt rymdskepp");
                Console.WriteLine("------------------------------");
                foreach (var ship in ships)
                {
                    index++;
                    Console.WriteLine($"[{index}]\t{ship.name}\t\t{ship.length} m");

                }

                Console.WriteLine("Bläddra genom piltangent ner eller annan valfri tangent för att välja skepp");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                //if (keyInfo.Key == ConsoleKey.DownArrow)
                //{
                //    index = 0;
                //    continue;
                //}
                //else
                    Console.WriteLine("Välj ett skepp genom nummer: ");

                byte choosenStarship = byte.Parse(Console.ReadLine());
                
                Console.WriteLine("Skriv in ditt registrerings nummer: ");
                string regNr = Console.ReadLine();
                var newStarship = new Starship(regNr, ships[choosenStarship - 1].name);// Slu7mpa fram ett eget ägarnummer
                return newStarship;
            }
            return null;
         
        }
        public async Task<IFluentCustomer> SelectStarship()
        {
            byte index = 0;

            for (int i = 0; i < 4; i++)
            {

                var objectOfStarships = await CustomerValidator.GetAllStarships(i + 1);
                var ships = objectOfStarships.results;

                Console.WriteLine("Var god och registrera ditt rymdskepp");
                Console.WriteLine("------------------------------");
                foreach (var ship in ships)
                {
                    index++;
                    Console.WriteLine($"[{index}]\t{ship.name}\t\t{ship.length} m");

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

            
                this.Starship = new Starship(regNr, ships[choosenStarship - 1].name);
              
                this.ParkingRegistration.StarshipId = DbAccess.GetStarshipId(Starship.Name);// Slu7mpa fram ett eget ägarnummer
                break;
            }

            return this;
        }


        public IFluentCustomer ParkShip(DateTime arrivalTime)
        {
         
            this.ParkingRegistration.AssignedParkingLotId = DataAccess.DbAccess.GetEmptyParkingLotsFromDB().Id;  //Tilldelar ledig plats this._parkingHouse.GetEmptyParkingLot()

            Console.WriteLine($"Tilldelad plats: {ParkingRegistration.AssignedParkingLotId}");
         

            this.ParkingRegistration.ArrivalTime = arrivalTime;
            Console.WriteLine("Parkeringens starttid påbörjas:" + ParkingRegistration.ArrivalTime);
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


        public IFluentCustomer LeavePark(DateTime departureTime)
        {
            

            parkingTimer.Stop();
            TimeSpan elapsedParkingTime = parkingTimer.Elapsed;
            Console.WriteLine("ParkingTid i sekunder" + elapsedParkingTime.Seconds);

            DateTime elapsedParkingTimeInHours = ParkingRegistration.ArrivalTime.AddHours(elapsedParkingTime.Seconds);
            this.ParkingRegistration.DepartureTime = elapsedParkingTimeInHours;

            Console.WriteLine("Id för plats ska uppdateras:      " + ParkingRegistration.AssignedParkingLotId);
            DbAccess.UpdateVacancyInParkinLot(ParkingRegistration.AssignedParkingLotId, false);


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
