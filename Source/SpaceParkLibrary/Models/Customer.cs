using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class Customer
    {
        public static int TotalCustomer { get; set; }

        // Våran kund som parkerar med skepp, ankomstid och sluttid för parkering,
        // har kreditvärdighet, samt betalat faktura eller ej
        public Customer()
        {
            TotalCustomer++; // Testar funktion
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }

        public bool CreditWorthiness { get; set; } // Kanske null direkt?

        public bool SelfRegistration { get; set; }

        public bool InvoicePaid { get; set; } // Vara eller icke vara?

        // public Object Starship { get; set; } // Vara eller icke vara?


    }
}
