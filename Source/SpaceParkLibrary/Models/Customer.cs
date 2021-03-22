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
        private ParkingHouse parkingHouse;

        // Våran kund som parkerar med skepp, ankomstid och sluttid för parkering,
        // har kreditvärdighet, samt betalat faktura eller ej

        public Customer()
        {

        }
        public Customer(ParkingHouse vistingParkingHouse)
        {
            this.parkingHouse = vistingParkingHouse;
            ParkingHouse.CustomerCounter++; // Räknar kunder så man får fram ifall huset är fullt

        }

        public int Id { get; set; }
        public string Name { get; set; }

        public Starship Starship { get; set; } // Vara eller icke vara?

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public int AssignedParkingLotNr { get; set; }

        //public bool CreditWorthiness { get; set; } // Kanske null direkt?

        public bool SelfRegistered { get; set; } // Vad menas med denna?

        public decimal InvoiceSum { get; set; }
        //public bool InvoicePaid { get; set; } // Vara eller icke vara?

        public IFluentCustomer SelectStarship(List<StarshipResult> results)
        {
            byte index = 0;
        
            Console.WriteLine("Var god och välj ett rymdskepp");
            Console.WriteLine("------------------------------");
            foreach (var ship in results)
            {
                index++;
                Console.WriteLine($"[{index}]\t{ship.name}");
            }

            Console.WriteLine("Välj ett skepp genom nummer: ");
            byte choosenStarship = byte.Parse(Console.ReadLine());

            this.Starship = new Starship(2342342, results[choosenStarship - 1].name); // Slu7mpa fram ett eget ägarnummer


            return this;
        }
        public IFluentCustomer VisitParkingHouse(ParkingHouse parkingHouse)
        {
            Console.WriteLine("Gästen besöker: " + parkingHouse.Name); //Måste få in namnet på P-huset här
            return this;
        }
        public async Task< IFluentCustomer> SelfRegistration()
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
            this.Id = 0001;             // Här ska vi kanske slumpa fram ett unikt id nummer
            
                // Här registreras troligtvis skeppet på något sätt

            //Person

            return this;
        }

     
        public IFluentCustomer ParkShip(Starship vehicle, DateTime arrivalTime)
        {
       
            Console.WriteLine("Här tilldelar vi platsnummer och registrerar det i databasen...");

            this.AssignedParkingLotNr = 1; // Random nr här


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
