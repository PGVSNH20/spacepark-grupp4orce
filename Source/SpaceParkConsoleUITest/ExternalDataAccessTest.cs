using RestSharp;
using SpaceParkLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpaceParkConsoleUITest
{
    public class ExternalDataAccessTest
    {
        [Theory]
        [InlineData("https://swapi.dev/api/")]
        [InlineData("https://swapi.dev/api/people/")]
        [InlineData("https://swapi.dev/api/starships/")]
        public void Successfully_Have_Connection_To_Rest_API(string getUrl)
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = new RestRequest(getUrl);

            IRestResponse restResponse = restClient.Get(restRequest);

            Assert.True(restResponse.IsSuccessful);
        }

        [Fact]
        public void Check_If_Database_Exists_From_Context_In_Use()
        {

            Assert.True(new ParkingContext().Database.Exists());
        }
    }
}
