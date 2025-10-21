using eTours_Unit_Testing;
using eToursLib;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTours_Unit_Testing
{
    public class TourGuide_Should
    {
        [Fact]
        public void Successfully_Create_Instance_And_Validate_Properties_And_ToString()
        {
            //arrange
            string inputFullName = "  Alice Smith  ";
            string inputLanguage = " English ";
            string ExpectedFullName = "Alice Smith";
            string ExpectedLanguage = "English";
            string expectedToString = "Alice Smith,English";

            //ACTION
            var sut = new TourGuide(inputFullName, inputLanguage); //instantiation of my object 
            // to check the state or behavior of the object after creation or after calling its methods.
            //assert
            sut.FullName.Should().Be(ExpectedFullName);
            sut.Language.Should().Be(ExpectedLanguage);
            sut.ToString().Should().Be(expectedToString);

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArgumentNullException_For_Invalid_FullName(string invalidFullName)
        {
            // Arrange
            var validLanguage = "English";

            // Act
            Action act = () => new TourGuide(invalidFullName, validLanguage);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*FullName must have a value*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArgumentNullException_For_Invalid_Language(string invalidLanguage)
        {
            // Arrange
            var validFullName = "Alice Smith";

            // Act
            Action act = () => new TourGuide(validFullName, invalidLanguage);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*Language must have a value*");
        }
        //arrange

        //action
        //assert







    }
}



