using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class ParkingLot
    {
        public int Id { get; set; }
        //public char SectionId { get; set; }

        public bool Occupied { get; set; }

        public ParkingLot(int id, bool occupied)
        {
            Id = id;
            Occupied = occupied;
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }
    }
}
