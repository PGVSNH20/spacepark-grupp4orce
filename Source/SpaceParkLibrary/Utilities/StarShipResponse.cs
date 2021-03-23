using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Utilities
{
    public class StarshipResponse
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<StarshipResult> results { get; set; }

    }
}
