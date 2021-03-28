using System;
using Xunit;
using SpaceParkLibrary;
using SpaceParkLibrary.Utilities;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.DataAccess;
//using SpaceParkConsoleUI;

namespace SpaceParkConsoleUITest
{
    public  class InputTest
    {
        [Fact]
        public async void Pass_As__A_Valid_SW_Name()
        {

            bool isValidated = await CustomerValidator.NameValidator("Luke Skywalker");
            Assert.True(isValidated, "The name Luke Skywalker should pass as a valid name");
        }

        [Theory]
        [InlineData("luke")]
        [InlineData("darTh vAder")]
        [InlineData("LEIA ORGANA")]
        public async void Pass_As__A_Valid_SW_Name_Due_Diff_In_Cases_And_Not_Complete_Name(string userInput)
        {

            bool isValidated = await CustomerValidator.NameValidator(userInput);
            Assert.True(isValidated, "The name given should pass as a valid nam,e despite minor difference in capitaliaztion");
        }
        [Theory]
        [InlineData("Lucky Luke")]
        [InlineData("Janne Långben")]
        //[InlineData("Darth")]
        [InlineData("Leila Oregano")]
        public async void Reject_As__A_Valid_SW_Name(string userInput)
        {

            bool isValidated = await CustomerValidator.NameValidator(userInput);
            Assert.False(isValidated, "The name given should NOT PASS  as a valid nam, because they are completly wrong or not complete");
        }
        [Fact]
        public void Check_If_Customer_Already_Exist_In_DB()
        {
            // Här låtsas jag registrera en användare

            //1	Luke Skywalker	force@jedi.temple	False
            var customerInRegistration = new Customer();
            customerInRegistration.Name = "Luke Skywalker";

            var customerFromDB = DbAccess.GetExistingCustomer(customerInRegistration);

            Assert.Equal(customerFromDB.Name, customerInRegistration.Name);
        }

    }
}
