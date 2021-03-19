using SpaceParkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class Customer : IFluentCustomer
    {
        private SpacePort parkingHouse;

        // Våran kund som parkerar med skepp, ankomstid och sluttid för parkering,
        // har kreditvärdighet, samt betalat faktura eller ej
        public Customer(SpacePort vistingParkingHouse)
        {
            this.parkingHouse = vistingParkingHouse;
            SpacePort.CustomerCounter++; // Räknar kunder så man får fram ifall huset är fullt
            
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public Starship Starship { get; set; } // Vara eller icke vara?

     

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public bool CreditWorthiness { get; set; } // Kanske null direkt?

        public bool SelfRegistrated { get; set; }

        public bool InvoicePaid { get; set; } // Vara eller icke vara?


        public IFluentCustomer SelfRegistration()
        {
            Console.WriteLine("Här registrerar vi Namn och skepp och är det godkända stegar vi vidare...");
            Console.WriteLine("Här valideras våra uppgifter av gateKeepern");
            return this;
        }
        public IFluentCustomer ParkShip(Starship vechicle, DateTime arrivalTime)
        {
            this.Starship = vechicle;
            Console.WriteLine("Här tildelas vi platsnummer och registreras i databasen...");
            Console.WriteLine("Här startas fejklockan...");
            return this;
        }
        public IFluentCustomer LeavePark(DateTime departureTime)
        {
            Console.WriteLine("Här beslutar vi att hämta bilen och fejklockan stoppas...");
            Console.WriteLine("Är vi kreditvärdiga så genereras en faktura baserad på ankomsttid och avgångstid, adress lämnas här ev. ...");
            Console.WriteLine("En ledigp lats registreras som öppen i P-huset...");
            SpacePort.CustomerCounter--;
            return this;
        }
        public IFluentCustomer DisplayCreditWorthiness()
        {
            Console.WriteLine("Kreditvärdighet måste alltid kollas innan gästen lämnar...");
            return this;
        }
      
        public IFluentCustomer ReciveInvoice()
        {
            Console.WriteLine("InvoiceTracker skickar faktura till kunden baserad på planet te x");
            return this;
        }



    }
}
