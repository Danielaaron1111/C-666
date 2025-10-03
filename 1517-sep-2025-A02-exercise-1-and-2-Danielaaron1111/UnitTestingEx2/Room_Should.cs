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
    public class Room_Should
    { //1.-	Create a room with a collection of Wall instances and Floor value. Assert the collection loaded as supplied.
        [Fact]
        public void Create_Room_With_Walls_And_Floor()
        {
            // Arrange: 
            var walls = new List<Wall> // create a room
            {
            new Wall("Wall1", 200, 300, "Blue", null),  // Unique PlanId, valid dimensions
            new Wall("Wall2", 200, 300, "Green", null) // with a collection of wall instances
            };
            string project = "ProjectA";
            string name = "Kitchen";
            string flooring = "Tile"; //values: 

            // Act
            Room sut = new Room(project, name, flooring, walls); // pass it to constructor

            // Assert
            sut.Project.Should().Be(project);
            sut.Name.Should().Be(name);
            sut.Flooring.Should().Be(flooring);
            sut.Walls.Should().BeEquivalentTo(walls);  // loaded 1
            sut.TotalWalls.Should().Be(2);  // with this beauty i check that my collection is loaded
        }
        //2.- Create a room without a collection of Wall instances but with a Floor value.
        [Fact]
        public void Create_Room_Without_Walls_With_Floor()
        {
            // Arrange
            string project = "ProjectA";
            string name = "Kitchen";
            string flooring = "Tile";

            // Act
            Room sut = new Room(project, name, flooring);

            // Assert
            sut.Project.Should().Be(project);
            sut.Name.Should().Be(name);
            sut.Flooring.Should().Be(flooring);
            sut.Walls.Should().BeEmpty();
            sut.TotalWalls.Should().Be(0);
        }



        //public void Create_A_Room_Wihthout_Walls_With_Floor()
        //{
        //    //arrange Sets up the test data
        //    string project = "ProjectB";
        //    string name = "asasdas";
        //    string flooring = "title";

        //    //Act : Constructor is called with 3 arguments no WALLSSSSSS UPSIDE IS THE PARAMETERS;
        //    Room sut = new Room(project, name, flooring); // create a room required
        //    //without collection of wall instances (no lists here) and no list 
        //    //created in the arrange
        //    // i dont pass wall in myy constructor so should be empty
        //    //my method include floor with A VALUEEEEEE the default is null.


        //    //Assert : this is what i am testing 
        //    sut.Project.Should().Be(project);
        //    sut.Name.Should().Be(name);
        //    sut.Flooring.Should().Be(flooring);
        //    sut.Walls.Should().BeEmpty();
        //    sut.TotalWalls.Should().Be(0);


        //}


        //3.- Create a room without a collection of Wall instances and no floor value. Hint: check flooring with null, empty and blank strings.
        [Fact]
        public void Create_Room_Without_Walls_And_Without_Floor()
        {
            // Arrange
            string project = "ProjectA";
            string name = "Kitchen";

            // Act
            Room sut = new Room(project, name);

            // Assert
            sut.Project.Should().Be(project);
            sut.Name.Should().Be(name);
            sut.Flooring.Should().BeNull(); // here 
            sut.Walls.Should().BeEmpty();
            sut.TotalWalls.Should().Be(0);
        }
        //4.- A missing Name will throw an ArgumentNullException
        [Theory] // we are testing three escenarios, theory here with InLineData run the same 3 test times with different values each in inlinData (reference to the data notes (put the description here to remember) // i hate you InlineData i wish you were null, empty or white or purple no purple no i like purple
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_ArgumentNullException_When_Name_Is_Invalid(String invalidName)
        {
            // Arrange
            string project = "ProjectA";
            string flooring = "Tile";



            // Act
            Action action = () => new Room(project, invalidName, flooring);

            // Assert (what should happen)
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("*Name cannot be null*"); // any message is ok.

        }
        //5.- Add a wall to the Wall collection.
        [Fact]
        public void Add_Wall_To_Room()
        {
            // Arrange 
            Room sut = new Room("ProjectA", "Kitchen");
            var wall = new Wall("Wall", 200, 300, "Blue", null);
            int expectedTotalWalls = 1; // counter is 0 here is one.

            // Act
            sut.AddWall(wall);

            // Assert
            sut.Walls.Should().Contain(wall);
            sut.TotalWalls.Should().Be(expectedTotalWalls);
        }


        //6.- Missing wall instance parameter value (ArgumentNullException)
        //add a null wall throw and exeption 

        [Fact]
        public void Throw_ArgumentNullException_When_Adding_Null_Wall()
        {
            //Arrange
            Room sut = new Room("ProjectA", "Kitchen");
            Wall nullWall = null;

            //Act
            Action action = () => sut.AddWall(nullWall);

            //Assert
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("*Wall cannot be null*"); // nop need .withmessage but its ok.


        }
        //7.- 	Wall planid parameter value already exists (ArgumentExpection). Check planid value is in the thrown message.
        [Fact]
        public void Throw_ArgumentException_When_Adding_Duplicate_PlanId()
        {
            // Arrange
            string duplicatePlanId = "Wall1";
            Room sut = new Room("ProjectA", "Kitchen");
            var wall1 = new Wall(duplicatePlanId, 200, 300, "Blue", null);
            var wall2 = new Wall(duplicatePlanId, 250, 300, "Green", null);

            sut.AddWall(wall1);

            // Act
            Action action = () => sut.AddWall(wall2);

            // Assert
            action.Should().Throw<ArgumentException>()
                .WithMessage($"*{duplicatePlanId}*");
        }

        //        //Predicate<Wall> predicate = wall => wall.PlanId == wall.PlanId.Trim(); // OLD WAY TO USE DELEGATES ON THE BOOK PAGE 323
        //            if (Walls.Exists(predicate))
        //{
        //    throw new ArgumentException($"Wall with PlanId '{wall.PlanId}' already exists.");
        //}

        //8.- Removes a wall from the Room Instance

        [Fact]
        public void Remove_Wall_From_Room_Succesfully()
        {
            //arrange
            string plainIdToRemove = "Wall1";
            var wall1 = new Wall(plainIdToRemove, 200, 300, "blue", null);
            var wall2 = new Wall("Wall2", 250, 300, "Green", null);
            var walls = new List<Wall> { wall1, wall2 }; // shorthand
            Room sut = new Room("ProjectA", "Kitchen", "Title", walls);
            int expectedTotalWalls = 1;



            //act
            sut.RemoveWall(plainIdToRemove);

            //assert
            sut.Walls.Should().NotContain(wall1);
            sut.Walls.Should().Contain(wall2);
            sut.TotalWalls.Should().Be(expectedTotalWalls);
        }

        //9.- Missing planid parameter value (ArgumentNullException). Hint: check with null, empty and blank strings.
        [Theory]
        [InlineData(null)] // as my skill to flex stuff in web
        [InlineData("")] //empty as my hearth
        [InlineData("   ")]
        public void Throw_ArgumentNullException_When_Removing_With_Invalid_PlanId(string invalidPlanId)
        {
            // Arrange
            Room sut = new Room("ProjectA", "Kitchen");

            // Act
            Action action = () => sut.RemoveWall(invalidPlanId);

            // Assert
            action.Should().Throw<ArgumentNullException>()
                .WithMessage("*PlanId cannot be null*");
        }

        //        //if (string.IsNullOrWhiteSpace(planid))
        //{
        //    throw new ArgumentNullException("PlanId cannot be null or empty");
        //}

        //10.------- PlanId not found (ArgumentExpection). Check planid value is in the thrown message.
        [Theory]
        [InlineData("Wall666")]
        [InlineData("GhostWall")]
        [InlineData("NotARealWall")]
        [InlineData("AnotherMissingWall")]

        public void Throw_ArgumentException_When_PlainId_Not_Found(string nonExistentPlainId)
        {
            //Arrange 
            var wall = new Wall("Wall1", 200, 300, "Blue", null);
            var walls = new List<Wall> { wall };
            Room sut = new Room("Project", "Kitchen", "Title", walls);
            //act 
            Action action = () => sut.RemoveWall(nonExistentPlainId);

            //assert 
            action.Should().Throw<ArgumentException>()
                .WithMessage($"*{nonExistentPlainId}*");

        }



        //[Fact]
        //public void Throw_ArgumentException_When_PlainId_Not_Found()
        //{
        //    //ARRANGEEEE
        //    string nonExistentPlainId = "Wall666";
        //    var wall = new Wall("Wall1", 200, 300, "Blue", null);
        //    var walls = new List<Wall> { wall };
        //    Room sut = new Room("Project", "Kitchen", "Title", walls);

        //    //ACTTTTTTT
        //    Action action = () => sut.RemoveWall(nonExistentPlainId);

        //    //ASSERTTTTTT
        //    action.Should().Throw<ArgumentException>()
        //        .WithMessage($"*{nonExistentPlainId}*");
        //}
        //        Wall wallToRemove = Walls.Find(predicate);
        //if (wallToRemove == null)
        //{
        //    throw new ArgumentException($"Wall with PlanId '{planid}' not found.");
        //}

        [Fact]
        public void Add_Returns_Correct_Sum()
        {
            //arrange 
            var calculator = new Calculator();
            //act 
            int result = calculator.Add(2, 3);

            //assert 
            result.Should().Be(5);
        }

        [Fact]

        public void Substract_Returns_Correct_Difference()
        {
            //arrange 
            var calculator = new Calculator();
            // act
            int result = calculator.Subtract(8, 5);
            //Assert 
            result.Should().Be(3);
        }

        //arrange 

        // act
        //Assert 


        [Fact]
        public void Calculator_Add_And_Substract_Work_Correctly()
        {
            //arrange 
            var calculator = new Calculator();
            

            // act
            int Addresult =  calculator.Add(1, 2);
            int SubResult = calculator.Subtract(1, 3);
            //Assert 
            Addresult.Should().Be(3);
            SubResult.Should().Be(-2);

        }

        [Fact]
        public void Substract_Add_No0_And_ThrowandException()
        { 
            //arrange
            var calculator = new Calculator();
           

            
        //Action
            int  result = calculator.Divide(10, 2);

        //assert
        result.Should().Be(5);


        //act and Assert for exception 
            Action action = () => calculator.Divide(10, 0);
            action.Should().Throw<ArgumentException>()
            .WithMessage("Cannot divide by zero.");
      

        }
        
        

        // act
        //Assert 


        //[Fact]
        //public void Add_Wall_With_Valid_Instance()
        //{
        //    //Arrange
        //    Room sut = new Room("ProjectA", "Kithchen");//start with empty walls
        //    var wall = new Wall("Wall1", 200, 300, "Blue", null); // Valid Wall with unique PlanId
        //    int expectedTotalWalls = 1;
        //    //ACT
        //    sut.AddWall(wall);

        //    //Asset 
        //    sut.Walls.Should().Contain(wall); // make sure wall was added :O
        //    sut.TotalWalls.Should().Be(expectedTotalWalls); //varofy count update
        //}
    }

}
