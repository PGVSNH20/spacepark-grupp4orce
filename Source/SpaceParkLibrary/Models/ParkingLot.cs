using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    class ParkingLot
    {
        public int Id { get; set; }
        public char SectionId { get; set; }

        public bool Occupied { get; set; }

    }
}
