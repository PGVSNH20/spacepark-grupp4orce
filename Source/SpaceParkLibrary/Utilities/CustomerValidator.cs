using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace SpaceParkLibrary.Utilities
{
    public class CustomerValidator
    {
        public bool NameIsValid { get; set; }
        public bool ShipIsValid { get; set; }
        public int PageSpaceship { get; set; }
        public static string RegisteredName { get; set; }

        public CustomerValidator()
        {
            
        }

        public static async Task<bool> NameValidator(string name)
        {
            var validator = new CustomerValidator();
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest("people/", DataFormat.Json).AddParameter("search", name);
            // NOTE: The Swreponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object
            var peopleResponse = await client.GetAsync<PeopleResponse>(request);

            if (peopleResponse.count == 1)
            {
                RegisteredName = peopleResponse.results[0].name;
                return validator.NameIsValid = true;
            }
            else if (peopleResponse.count > 1)
            {
                Console.WriteLine($"Det finns flera som har ¨{name}¨ i sitt namn!");
                int i = 0;
				foreach (var people in peopleResponse.results)
				{
					Console.WriteLine($"[{i++}] {people.name}");
				}
				Console.WriteLine("Välj ditt namn: ");
                string input = Console.ReadLine();
				int.TryParse(input, out int value);

                //TODO: value out of range

                RegisteredName = peopleResponse.results[value].name;
                return validator.NameIsValid = true;
            }
            else
            {
                return validator.NameIsValid = false;
            }
        }

        public static async Task<StarshipResponse> GetAllStarships(int changePage)
        {

            var test = new CustomerValidator();
            test.PageSpaceship = changePage;
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest("starships/", DataFormat.Json).AddParameter("page", test.PageSpaceship);
            // NOTE: The Swresponse is a custom class which represents the data returned by the API, RestClient have buildin ORM which maps the data from the reponse into a given type of object
            var StarShipResponse = await client.GetAsync<StarshipResponse>(request);

            return StarShipResponse;
        }
    }
}
