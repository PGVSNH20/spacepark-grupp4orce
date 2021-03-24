using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool InvoicePaid { get; set; } // Vara eller icke vara?

        public async Task<IFluentCustomer> SelectStarship(Starship starship)
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
              
                //Console.WriteLine("Bläddra genom piltangent");
                //int input = int.Parse(Console.ReadLine());
                //if (input == 2)
                //{
                //    continue;
                //}
                //else
                Console.WriteLine("Välj ett skepp genom nummer: ");

                byte choosenStarship = byte.Parse(Console.ReadLine());

                starship = new Starship(2342342, ships[choosenStarship - 1].name); // Slu7mpa fram ett eget ägarnummer
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
        public async Task< IFluentCustomer> SelfRegistration(ParkingOrder parkingOrder, Starship starship)
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
            this.Name = inputName;
			Console.WriteLine("Submit your information");
            this.Email = Console.ReadLine();
            InvoicePaid = false;
            //Kolla om kunden finns i databasen
            //Om kunden inte existerar
            //Lägg till kunden till databasen
            
            // Här registreras troligtvis skeppet på något sätt

            //Person

            return this;
        }

     
        public IFluentCustomer ParkShip(Starship vehicle, DateTime arrivalTime)
        {
            Console.WriteLine("Här tilldelar vi platsnummer och registrerar det i databasen...");

            this.AssignedParkingLot = this._parkingHouse.GetEmptyParkingLot();  //Tilldelar ledig plats

            Console.WriteLine($"Välkommen {Name}!");
            Console.WriteLine($"Din {Starship.Name} är parkerad på plats {AssignedParkingLot.Id} och därmed är den klassas som occupied: {AssignedParkingLot.Occupied}");
            Console.WriteLine("Antal lediga platser för nuvarande är: " + _parkingHouse.VacantParkingLots);



            Console.WriteLine("Här startas fejklockan...");


            return this;
        }
        public IFluentCustomer LeavePark(DateTime departureTime)
        {

            Console.WriteLine("Här beslutar vi att hämta bilen och fejklockan stoppas...");
            Console.WriteLine("Är vi kreditvärdiga så genereras en faktura baserad på ankomsttid och avgångstid, adress lämnas här ev. ...");
            Console.WriteLine("En ledig plats registreras som öppen i P-huset...");
            ParkingHouse.CustomerCounter--;
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



    }
}
