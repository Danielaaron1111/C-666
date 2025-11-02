using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eToursLib;
using FluentAssertions;

namespace eTours_Unit_Testing
{

    
    public class Destination_Should
    {
        [Fact]
        public void Successfully_Create_Instance_And_Validate_Properties_And_ToString()
        {
            // Arrange
            string expectedLocation = "Paris";
            DateTime expectedVisitDate = new DateTime(2025, 12, 25); // whaetvet date actual current Oct 08, 2025
            string expectedToString = "Paris,Dec 25 2025";

            // Act
            Destination sut = new Destination("  Paris  ", expectedVisitDate);
            //sut not act in this case (not sure double check at the end of the 
            //assigment 

            // Assert
            sut.Location.Should().Be(expectedLocation);
            sut.VisitDate.Should().Be(expectedVisitDate);
            sut.ToString().Should().Be(expectedToString);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArgumentNullException_When_Creating_Instance_With_Missing_Location(string invalidLocation)
        {
            // Arrange
            DateTime validDate = DateTime.Today;

            // Act
            Action act = () => new Destination(invalidLocation, validDate);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("*Location must have a value*");
        }
    }
}

//Prove the validity of your Destination class by writing tests for the following:

//Test that the Location and VisitDate data has been successfully stored to its property when creating a new instance.Validate each property return value and the ToString value.


//Test that an appropriate exception is throw when Location has missing data when creating a new instance.


//The xUnit Testing : Destination_Should
//Create the following unit tests within the file in the supplied xUnit Testing project. Use FluentAssertions for your unit tests. Only the requested unit tests need to be coded.

//Prove the validity of your Destination class by writing tests for the following:

//Test that the Location and VisitDate data has been successfully stored to its property when creating a new instance.Validate each property return value and the ToString value.
//Test that an appropriate exception is throw when Location has missing data when creating a new instance.
//Remember: When you are finished each activity that you commit the work.















