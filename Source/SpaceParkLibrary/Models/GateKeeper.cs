using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SpaceParkLibrary.Models
{
    class GateKeeper
    {
        private static int starshipMaxLength = 200; //I meter

        public static bool IsStarshipToLongForParkinglot(string inputShipName, double inputShipLength)
        {
            if (inputShipLength >= starshipMaxLength)
            {
                Console.WriteLine($"Valda skeppet {inputShipName} är för stort för att parkera här. Vänlig välj ett annat skepp.");
                return false;
            }
            else
            {
                Console.WriteLine($"{inputShipName} är vald.");
                return true;
            }
        }

        public static bool IsRegistrationNumberValid(string regNumber)
        {
            string pattern = "(^B[A-Z]{3}[0-9]{3}$)";
            var matching = Regex.Match(pattern, regNumber);
       
            if (matching.Success)
            {
                return true;
            }
            else if (regNumber.Length == 6 && regNumber.Any(char.IsDigit))
            {
                return true;
            }
            else if (regNumber.Length != 6 && regNumber.Any(char.IsDigit) == false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public static Decimal CalculateParkingFee(double hours) => (decimal)hours * ParkingHouse.PricePerHour;


        // GateKeepern är våran dörrvakt som släpper in godkända gäster,
        // och håller räkning på dem samt släpper ut dem ifall kreditvärdighet uppnås
        // för att sedan skicka till ekonomiavdelningen för fakturering
    }
}
