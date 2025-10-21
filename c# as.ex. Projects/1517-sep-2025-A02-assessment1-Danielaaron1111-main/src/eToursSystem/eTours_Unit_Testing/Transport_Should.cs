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
    public class Transport_Should
    {
        //TODO: Activity 2
        //place your unit tests here


        
        [Fact]
        public void Successfully_Change_Name_Via_Property()
        {
            // Arrange
            Transport sut = new Transport("Initial", 50);
            string expectedName = "Updated Name";

            // Act
            sut.Name = "  Updated Name   ";

            // Assert
            sut.Name.Should().Be(expectedName);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Throw_ArgumentException_When_Creating_Instance_With_Invalid_Capacity(int invalidCapacity)
        {
            // Arrange
            string validName = "Valid Transport";

            // Act
            Action act = () => new Transport(validName, invalidCapacity);

            // Assert
            act.Should().Throw<ArgumentException>()
               .WithMessage($"*Capacity value {invalidCapacity}*");
        }
    }
}



//is this what you mean : 
//Transport_Should
//Create the following unit tests within the file in the supplied xUnit Testing project. Use FluentAssertions for your unit tests. Only the requested unit tests need to be coded.

//Prove the validity of your Transport class by writing tests for the following:

//Test that the Name data has been successfully stored to its property when changing the Name via its property directly.
//Test that an appropriate exception is throw when Capacity has invalid data when creating a new instance.Check the exception message contains the invalid value.

