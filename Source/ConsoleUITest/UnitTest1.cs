using SpaceParkLibrary.Interfaces;
using SpaceParkLibrary.Models;
using SpaceParkLibrary.Utilities;
using System;
using Xunit;

namespace ConsoleUITest
{
    public class UnitTest1
    {
        [Fact]
        public async void Pass_As_A_Valid_StarWars_Name()
        {
            
            Assert.True(await CustomerValidator.NameValidator("Luke Skywalker"));

        }
        [Theory]
        [InlineData("luke")]
        [InlineData("daRth vAdeR")]
        [InlineData("LEIA")]
        public async void Pass_Incomplete_And_Not_Case_Sensitive_Names(string userInputName)
        {

            Assert.True(await CustomerValidator.NameValidator(userInputName));

        }
        [Theory]
        [InlineData("Kalle Anka")]
        [InlineData("Janne Långben")]
        [InlineData("Lucky Luke")]
        [InlineData("Leila")]
        public async void Reject_As_A_Valid_StarWars_Names(string userInputName)
        {

            Assert.False(await CustomerValidator.NameValidator(userInputName));

        }
    }
}
