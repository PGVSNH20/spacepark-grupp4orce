using SpaceParkLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingHouse
    {
        // Parkeringshus, totala platser, lista med platser
        // Samt räknare för lediga platser som skickas till dörrvakten

   

        public ParkingHouse(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public static Decimal PricePerHour { get; set; } = 20;
        public static byte TotalParkingsLots { get; set; } = 10;

        public static int AmountOfEmptylots { get; set; } = DbAccess.GetAmountOfEmptyParkingLots();

        public bool Vacancy { get; set; } = true; // Måste beräknas först, men sätter som true just nu. 

    }
}
