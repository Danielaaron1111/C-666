using FluentAssertions;
using RenoSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestingEx2
{
    public class Room_Should
    {
        [Fact]
        public void Create_Room_With_Walls_And_Floor()
        {
            // Arrange
            var walls = new List<Wall>
    {
            new Wall("Wall1", 200, 300, "Blue", null),  // Unique PlanId, valid dimensions
            new Wall("Wall2", 200, 300, "Green", null) // if this is 1 the test fail
    };
            string project = "ProjectA";
            string name = "Kitchen";
            string flooring = "Tile";

            // Act
            Room sut = new Room(project, name, flooring, walls);

            // Assert
            sut.Project.Should().Be(project);
            sut.Name.Should().Be(name);
            sut.Flooring.Should().Be(flooring);
            sut.Walls.Should().BeEquivalentTo(walls);  // Confirms collection loaded as supplied
            sut.TotalWalls.Should().Be(2);  // Verifies count
        }
    }

}
