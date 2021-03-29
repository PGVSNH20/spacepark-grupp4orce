using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkLibrary.Models
{
    class GateKeeper
    {
        private int starshipMaxLength = 200; //I meter

        public bool StarshipLengthChecker(string inputShipName, int inputShipLength)
		{
			if (inputShipLength >= starshipMaxLength)
			{
				Console.WriteLine($"{inputShipName} är för stort! Välj en mindre!");
				return false;
			}
			else
			{
				Console.WriteLine($"{inputShipName} får plats!");
				return true;
			}
		}
        // GateKeepern är våran dörrvakt som släpper in godkända gäster,
        // och håller räkning på dem samt släpper ut dem ifall kreditvärdighet uppnås
        // för att sedan skicka till ekonomiavdelningen för fakturering


    }
}
