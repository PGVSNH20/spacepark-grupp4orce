using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class SpacePort
    {
        // Parkeringshus, totala platser, lista med platser
        // Samt räknare för lediga platser som skickas till dörrvakten

        private static byte _vacantParkingLotsCounter;

        private static readonly byte maxiumParkingLots = 10;

        public string Name { get; set; }
        public byte TotalParkingsLots { get; set; } = maxiumParkingLots;

        public byte VacantParkingLots { get; set; }

        public static int CustomerCounter { get; set; }

        public bool Vacancy { get; set; }

        List<ParkingLot> ParkingLots { get; set; }
        List<Char> Sections { get; set; } = new List<char>() { 'A', 'B', 'C', 'D' };

    }
}
