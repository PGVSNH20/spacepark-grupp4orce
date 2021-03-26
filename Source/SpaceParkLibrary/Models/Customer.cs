﻿using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Utilities;
using System;
using System.Threading.Tasks;
using SpaceParkLibrary.DataAccess;

namespace SpaceParkLibrary.Models
{
    public class Customer : IFluentCustomer
    {
        private ParkingHouse _parkingHouse;
        

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

        ParkingOrder parkingOrder = new ParkingOrder();

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
                    Console.WriteLine($"[{index}]\t{ship.name}");
                }

                Console.WriteLine("Bläddra genom piltangent");
                int input = int.Parse(Console.ReadLine());
                if (input == 2 )
                {
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

                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();
            }

            // Ifall loopen stegas  ur är vi fullt validerade
            this.Name = CustomerValidator.RegisteredName;
			Console.WriteLine("Skriv in din emailadress: ");
            this.Email = Console.ReadLine();
            InvoicePaid = false;

            // Här registreras troligtvis skeppet på något sätt

            parkingOrder.CustomerId = this; // Våran klass kund och dens ifyllda propeties vi nyss satt åker in i parkeringsorderns kundinfo

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

            parkingOrder.AssignedParkingLot = this._parkingHouse.GetEmptyParkingLot();  //Tilldelar ledig plats

            Console.WriteLine($"Välkommen {Name}!");
            Console.WriteLine($"Din {vehicle.Name} är parkerad på plats {parkingOrder.AssignedParkingLot.Id} och därmed är den klassas som occupied: {parkingOrder.AssignedParkingLot.Occupied}");

            parkingOrder.StarshipId = vehicle;

            Console.WriteLine($"Antal lediga platser för nuvarande är: {_parkingHouse.VacantParkingLots}");

            Console.WriteLine("Här startas fejklockan...");

            parkingOrder.ArrivalTime = arrivalTime;

            return this;
        }
        public IFluentCustomer LeavePark(DateTime departureTime, ParkingOrder parkingOrder)
        {

            Console.WriteLine("Här beslutar vi att hämta bilen och fejklockan stoppas...");
            parkingOrder.DepartureTime = departureTime;

            Console.WriteLine("Är vi kreditvärdiga så genereras en faktura baserad på ankomsttid och avgångstid, adress lämnas här ev. ...");
            
            Console.WriteLine("En ledig plats registreras som öppen i P-huset...");
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
