using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RenoSystem;
using FluentAssertions;

namespace UnitTestingEx1
{
    public class Utilities_Should
    {
        public const int MINIMUMWIDTH = 50;
        public const int MINIMUMHEIGHT = 120;
        public const int MINIMUMEDGING = 10;

        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        public void Accept_Valid_Non_Zero_Positive_Value(int value)
        {
            //Arrange

            //Act
            bool result = Utilities.IsNonZeroPositive(value);
            //Assert
            result.Should().BeTrue();

        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-200)]
        public void Reject_Invalid_Non_Zero_Positive_Value(int value)
        {
            //Arrange

            //Act
            bool result = Utilities.IsNonZeroPositive(value);
            //Assert
            result.Should().BeFalse();

        }

        [Theory]
        [InlineData(50, MINIMUMWIDTH)]
        [InlineData(51, MINIMUMWIDTH)]
        [InlineData(120, MINIMUMHEIGHT)]
        [InlineData(121, MINIMUMHEIGHT)]
        [InlineData(10, MINIMUMEDGING)]
        [InlineData(11, MINIMUMEDGING)]
        public void Accept_Valid_Minimum_Criteria_Value(int value, int criteria)
        {
            //Arrange

            //Act
            bool result = Utilities.MeetsMinimumCriteria(value, criteria);
            //Assert
            result.Should().BeTrue();

        }
        [Theory]
        [InlineData(49, MINIMUMWIDTH)]
        [InlineData(-51, MINIMUMWIDTH)]
        [InlineData(119, MINIMUMHEIGHT)]
        [InlineData(-121, MINIMUMHEIGHT)]
        [InlineData(9, MINIMUMEDGING)]
        [InlineData(-11, MINIMUMEDGING)]
        public void Reject_Invalid_Minimum_Criteria_Value(int value, int criteria)
        {
            //Arrange

            //Act
            bool result = Utilities.MeetsMinimumCriteria(value, criteria);
            //Assert
            result.Should().BeFalse();

        }
    }
}
