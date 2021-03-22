using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    public class Starship
    {
        // Rymdskepp har en ägare samt den plats den står på, ev. värde för kreditupplysning
        public int VIN { get; set; }
        public string Name { get; set; }
        //public int OwnerId { get; set; }
        //public object DesignatedParkingLot { get; set; }


        public Starship(int vin, string name )
        {
            VIN = vin;
            Name = name;
        }
    }
}
