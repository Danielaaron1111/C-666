using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RenoSystem;
using FluentAssertions;

namespace UnitTestingEx1
{
    public class Opening_Should
    {
        public const int MINIMUMWIDTH = 50;
        public const int MINIMUMHEIGHT = 120;
        public const int MINIMUMEDGING = 10;

        #region Valid Constructors

        [Fact]
        public void Create_Opening_With_Valid_Values()
        {
            //Arrange


            //Act
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Assert
            sut.Type.Should().Be(OpeningType.Door);
            sut.Width.Should().Be(MINIMUMWIDTH);
            sut.Height.Should().Be(MINIMUMHEIGHT);
            sut.Edging.Should().Be(MINIMUMEDGING);
        }

        [Fact]
        public void Create_Opening_With_Valid_Values_And_Default_Edging()
        {
            //Arrange


            //Act
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT);

            //Assert
            sut.Type.Should().Be(OpeningType.Door);
            sut.Width.Should().Be(MINIMUMWIDTH);
            sut.Height.Should().Be(MINIMUMHEIGHT);
            sut.Edging.Should().Be(0);
        }

        #endregion

        #region Area and Perimeter
        [Fact]
        public void Calculate_Area()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);
            int expectedArea = MINIMUMWIDTH * MINIMUMHEIGHT;

            //Act
            int area = sut.Area;

            //Assert
            area.Should().Be(expectedArea);

        }

        [Fact]
        public void Calculate_Perimeter()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);
            int expectedPerimeter = (MINIMUMWIDTH + MINIMUMHEIGHT) * 2;

            //Act
            int perimeter = sut.Perimeter;

            //Assert
            perimeter.Should().Be(expectedPerimeter);

        }
        #endregion

        #region Methods
        [Fact]
        public void Create_ToString_Matching_Constructor_Input_Sequence()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);
            string expectedToString = $"{OpeningType.Door},{MINIMUMWIDTH},{MINIMUMHEIGHT},{MINIMUMEDGING}";

            //Act
            string toString = sut.ToString();

            //Assert
            toString.Should().Be(expectedToString);

        }
        #endregion

        #region Constructor/Property Validation Exceptions
        [Theory]
        [InlineData(0, 121, 11)]
        [InlineData(-1, 121, 11)]
        [InlineData(51, 0, 11)]
        [InlineData(51, -1, 11)]
        [InlineData(51, 121, -1)]

        public void Throw_Exception_Creating_Constructor_With_Invalid_Positive_Values(int width, int height, int edging)
        {
            //Arrange


            //Act
            Action action = () => new Opening(OpeningType.Door, width, height, edging);

            //Assert
            action.Should().Throw<ArgumentException>();
        }
        [Theory]
        [InlineData(49, 121, 11)]
        [InlineData(51, 119, 11)]
        [InlineData(51, 121, 9)]

        public void Throw_Exception_Creating_Constructor_With_Invalid_Minimum_Values(int width, int height, int edging)
        {
            //Arrange


            //Act
            Action action = () => new Opening(OpeningType.Door, width, height, edging);

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Throw_Exception_Changing_Width_With_Invalid_Postive_Value(int width)
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Width = width;

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]


        public void Throw_Exception_Changing_Width_With_Invalid_Minimum_Value()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Width = 49;

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Throw_Exception_Changing_Height_With_Invalid_Postive_Value(int height)
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Height = height;

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]

        public void Throw_Exception_Changing_Height_With_Invalid_Minimum_Value()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Height = 119;

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Throw_Exception_Changing_Edging_With_Invalid_Postive_Value()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Edging = -1;

            //Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]

        public void Throw_Exception_Changing_Edging_With_Invalid_Minimum_Value()
        {
            //Arrange
            Opening sut = new Opening(OpeningType.Door, MINIMUMWIDTH, MINIMUMHEIGHT, MINIMUMEDGING);

            //Act
            Action action = () => sut.Edging = 9;

            //Assert
            action.Should().Throw<ArgumentException>();
        }
        #endregion
    }
}
