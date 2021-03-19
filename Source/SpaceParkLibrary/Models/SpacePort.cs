using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    class SpacePort
    {
        // Parkeringshus, totala platser, lista med platser
        // Samt räknare för lediga platser som skickas till dörrvakten

        private byte _vacantParkingLotsCounter;

        public string Name { get; set; }
        public byte TotalParkingsLots { get; set; } = 10;

        public byte VacantParkingLots { get; set; }

        List<ParkingLot> ParkingLots { get; set; }
        List<Char> Sections { get; set; } = new List<char>() { 'A', 'B', 'C', 'D' };

    }
}
