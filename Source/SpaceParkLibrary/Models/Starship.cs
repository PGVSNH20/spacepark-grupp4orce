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
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public object DesignatedParkingLot { get; set; }
    }
}
