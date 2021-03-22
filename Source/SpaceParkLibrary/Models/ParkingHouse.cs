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

        private static byte _vacantParkingLotsCounter;

        private static readonly byte maximumParkingLots = 10;

        public ParkingHouse(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public byte TotalParkingsLots { get; set; } = (byte)parkingsLots.Count;

        public byte VacantParkingLots { get; set; }

        public static int CustomerCounter { get; set; }

        public bool Vacancy { get; set; }

        List<ParkingLot> ParkingLots { get; set; }

        private static List<ParkingLot> parkingsLots = new List<ParkingLot>
        {
             //new ParkingLot(1, ),
             //new ParkingLot(),
             //new ParkingLot(),
             //new ParkingLot(),

        };


        //List<Char> Sections { get; set; } = new List<char>() { 'A', 'B', 'C', 'D' };

    }
}
