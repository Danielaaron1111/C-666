using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using RenoSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace UnitTestingEx2
{
    public class Wall_Should
    {

        // Constants for testing - match your Wall class minimums

        public const int MINIMUNWIDTH = 26;
        public const int MINIMUMHEIGHT = 100;

        //Test valic constructors 
        [Fact]
        public void Create_Wall_With_Valid_Values_Without_Opening()
        {
            // Arrange - prepare test data
            string expectedPlanId = "Plan123";
            int expectedWidth = 200;
            int expectedHeight = 250;
            string expectedColor = "Blue";

            // Act - create the wall (system under test = sut)
            Wall sut = new Wall(expectedPlanId, expectedWidth, expectedHeight, expectedColor, null);

            // Assert - verify all properties are set correctly
            sut.PlanId.Should().Be(expectedPlanId);
            sut.Width.Should().Be(expectedWidth);
            sut.Height.Should().Be(expectedHeight);
            sut.Color.Should().Be(expectedColor);
            sut.WallOpening.Should().BeNull(); // No opening
        }

        // TEST 2: Create a valid wall WITH an opening
        [Fact]
        public void Create_Wall_With_Valid_Values_With_Opening()
        {
            // Arrange
            string expectedPlanId = "Plan456";
            int expectedWidth = 300;
            int expectedHeight = 250;
            string expectedColor = "White";
            // Create a small opening (10x10 = 100 cm², much less than 90% of wall)
            Opening opening = new Opening(OpeningType.Window, 50, 120, 10);

            // Act
            Wall sut = new Wall(expectedPlanId, expectedWidth, expectedHeight, expectedColor, opening);

            // Assert
            sut.PlanId.Should().Be(expectedPlanId);
            sut.Width.Should().Be(expectedWidth);
            sut.Height.Should().Be(expectedHeight);
            sut.Color.Should().Be(expectedColor);
            sut.WallOpening.Should().Be(opening);
        }
        //TEST 3: PlainId cannot be null 
        [Fact]
        public void Throw_Exception_For_Null_PlainId()
        {
            //act try to create a wall with null plainid
            Action act = () => new Wall(null, 200, 250, "Blue", null);

            //assert - expected ArgumentNullException
            act.Should().Throw<ArgumentNullException>().WithMessage("*PlanId*");
        }
        //Test 4 : PlainId cannot be empty or whitespace (Using theory for multiple values)
        [Theory]
        [InlineData("")] // empty string
        [InlineData("     ")]//whitespace only 
        public void Throw_Exception_For_Empty_Or_WhiteSpace_PlanId(string planId)
        {
            //act
            Action action  = () => new Wall(planId, 200, 250, "Blue", null);

            //assert 
            action.Should().Throw<ArgumentNullException>().WithMessage("*PlanId*");
        }

        // TEST 5: Width must be positive and non-zero
        [Theory]
        [InlineData(0)] // Zero is invalid
        [InlineData(-1)] // Negative is invalid
        [InlineData(-100)] // More negative
        public void Throw_Exception_For_Invalid_Positive_Width(int width)
        {
            // Act
            Action action = () => new Wall("Plan123", width, 250, "Blue", null);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*width*");
        }

        // TEST 6: Width must meet minimum criteria (26 cm)
        [Theory]
        [InlineData(25)] // Just below minimum
        [InlineData(20)]
        [InlineData(1)]
        public void Throw_Exception_For_Width_Below_Minimum(int width)
        {
            // Act
            Action action = () => new Wall("Plan123", width, 250, "Blue", null);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*width*");
        }
        // TEST 7: Height must be positive and non-zero
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-50)]
        public void Throw_Exception_For_Invalid_Positive_Height(int height)
        {
            // Act
            Action action = () => new Wall("Plan123", 200, height, "Blue", null);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*height*");
        }

        // TEST 8: Height must meet minimum criteria (100 cm)
        [Theory]
        [InlineData(99)] // Just below minimum
        [InlineData(50)]
        [InlineData(1)]
        public void Throw_Exception_For_Height_Below_Minimum(int height)
        {
            // Act
            Action action = () => new Wall("Plan123", 200, height, "Blue", null);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*height*");
        }

        // TEST 9: Color cannot be null
        [Fact]
        public void Throw_Exception_For_Null_Color()
        {
            // Act
            Action action = () => new Wall("Plan123", 200, 250, null, null);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*Color*");
        }

        // TEST 10: Color cannot be empty or whitespace
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_For_Empty_Or_Whitespace_Color(string color)
        {
            // Act
            Action action = () => new Wall("Plan123", 200, 250, color, null);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*Color*");
        }

        // TEST 11: Opening cannot be 90% or more of wall area
        [Fact]
        public void Throw_Exception_When_Opening_Exceeds_90_Percent_Of_Wall()
        {
            // Arrange
            // Wall: 200 x 250 = 50,000 cm²
            // 90% = 45,000 cm²
            // Opening: 200 x 225 = 45,000 cm² (exactly 90%, should fail)
            Opening largeOpening = new Opening(OpeningType.Window, 200, 225, 10);

            // Act
            Action action = () => new Wall("Plan123", 200, 250, "Blue", largeOpening);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*Opening limit exceeded*");
        }

        // TEST 12: Opening just under 90% should be valid
        [Fact]
        public void Create_Wall_When_Opening_Is_Just_Under_90_Percent()
        {
            // Arrange
            // Wall: 200 x 250 = 50,000 cm²
            // 90% = 45,000 cm²
            // Opening: 200 x 220 = 44,000 cm² (88%, should pass)
            Opening opening = new Opening(OpeningType.Window, 200, 220, 10);

            // Act
            Wall sut = new Wall("Plan123", 200, 250, "Blue", opening);

            // Assert
            sut.WallOpening.Should().Be(opening);
        }
        // TEST 13: Calculate surface area without opening
        [Fact]
        public void Calculate_SurfaceArea_Without_Opening()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);
            int expectedArea = 200 * 250; // 50,000 cm²

            // Act
            int area = sut.SurfaceArea;

            // Assert
            area.Should().Be(expectedArea);
        }

        // TEST 14: Calculate surface area with opening
        [Fact]
        public void Calculate_SurfaceArea_With_Opening()
        {
            // Arrange
            Opening opening = new Opening(OpeningType.Window, 50, 120, 10); // 6,000 cm²
            Wall sut = new Wall("Plan123", 200, 250, "Blue", opening);
            int expectedArea = (200 * 250) - 6000; // 50,000 - 6,000 = 44,000 cm²

            // Act
            int area = sut.SurfaceArea;

            // Assert
            area.Should().Be(expectedArea);
        }

        // TEST 15: ToString without opening
        [Fact]
        public void Create_ToString_Without_Opening()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);
            string expectedString = "Plan123,200,250,Blue";

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expectedString);
        }

        // TEST 16: ToString with opening
        [Fact]
        public void Create_ToString_With_Opening()
        {
            // Arrange
            Opening opening = new Opening(OpeningType.Window, 50, 120, 10);
            Wall sut = new Wall("Plan123", 200, 250, "Blue", opening);
            string expectedString = $"Plan123,200,250,Blue,{opening.ToString()}";

            // Act
            string result = sut.ToString();

            // Assert
            result.Should().Be(expectedString);
        }
        // TEST 17: Successfully change color
        [Fact]
        public void Change_Color_Successfully()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            sut.ChangeColor("Red");

            // Assert
            sut.Color.Should().Be("Red");
        }

        // TEST 18: ChangeColor validates input
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_When_Changing_To_Invalid_Color(string color)
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            Action action = () => sut.ChangeColor(color);

            // Assert
            action.Should().Throw<ArgumentNullException>().WithMessage("*Color*");
        }
        // TEST 19: Successfully change width
        [Fact]
        public void Change_Width_Successfully()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            sut.ChangeWallWidth(300);

            // Assert
            sut.Width.Should().Be(300);
        }

        // TEST 20: ChangeWallWidth validates minimum
        [Fact]
        public void Throw_Exception_When_Changing_Width_Below_Minimum()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            Action action = () => sut.ChangeWallWidth(25);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*width*");
        }

        // TEST 21: ChangeWallWidth re-validates opening ratio
        [Fact]
        public void Throw_Exception_When_Changing_Width_Makes_Opening_Too_Large()
        {
            // Arrange
            // Initial wall: 300 x 250 = 75,000 cm²
            // Opening: 200 x 220 = 44,000 cm² (58.7% of wall, OK)
            Opening opening = new Opening(OpeningType.Window, 200, 220, 10);
            Wall sut = new Wall("Plan123", 300, 250, "Blue", opening);

            // Act - reduce width to 220
            // New wall: 220 x 250 = 55,000 cm²
            // Opening: 44,000 cm² (80% of wall, still OK)
            // But let's try 210: 210 x 250 = 52,500 cm²
            // Opening: 44,000 cm² (83.8%, still OK)
            // Try 205: 205 x 250 = 51,250 cm²
            // Opening: 44,000 cm² (85.9%, still OK)
            // Try 201: 201 x 250 = 50,250 cm²
            // 90% of 50,250 = 45,225
            // Opening is 44,000, which is less than 45,225, so it should pass
            // Try 200: 200 x 250 = 50,000 cm²
            // 90% = 45,000
            // Opening is 44,000 < 45,000, should pass
            // Try 195: 195 x 250 = 48,750 cm²
            // 90% = 43,875
            // Opening is 44,000 > 43,875, should FAIL
            Action action = () => sut.ChangeWallWidth(195);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*Opening limit exceeded*");
        }
        // TEST 22: Successfully change height
        [Fact]
        public void Change_Height_Successfully()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            sut.ChangeWallHeight(300);

            // Assert
            sut.Height.Should().Be(300);
        }

        // TEST 23: ChangeWallHeight validates minimum
        [Fact]
        public void Throw_Exception_When_Changing_Height_Below_Minimum()
        {
            // Arrange
            Wall sut = new Wall("Plan123", 200, 250, "Blue", null);

            // Act
            Action action = () => sut.ChangeWallHeight(99);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*height*");
        }

        // TEST 24: ChangeWallHeight re-validates opening ratio
        [Fact]
        public void Throw_Exception_When_Changing_Height_Makes_Opening_Too_Large()
        {
            // Arrange
            // Initial wall: 200 x 300 = 60,000 cm²
            // Opening: 200 x 220 = 44,000 cm² (73.3%, OK)
            Opening opening = new Opening(OpeningType.Window, 200, 220, 10);
            Wall sut = new Wall("Plan123", 200, 300, "Blue", opening);

            // Act - reduce height
            // Try 240: 200 x 240 = 48,000 cm²
            // 90% = 43,200
            // Opening is 44,000 > 43,200, should FAIL
            Action action = () => sut.ChangeWallHeight(240);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage("*Opening limit exceeded*");
        }


       

        

        
    

    }
}
