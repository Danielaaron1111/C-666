using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoSystem
{
    public class Room222
    {
        private string _Name;
        private string _Project;
        private string _Flooring;
        //properties 

        public string Flooring
        {
            get { return _Flooring; }

            set
            {
                //if (value != null && string.IsNullOrWhiteSpace(value))
                //{
                //    throw new ArgumentException("Flooring, if provided, cannot be empty or whitespace.");
                //}

                if (value != null)


                    if (string.IsNullOrWhiteSpace(value))

                        throw new ArgumentException("Flooring, if provided, cannot be empty or whitespace.");
                    else
                        _Flooring = value.Trim();
                //this could be ofc could be ommited.
                else
                    _Flooring = value;

            }



        }
        public string Project
        {
            get { return _Project; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Project cannot be null, empty, or whitespace.");
                }
                _Project = value.Trim();
            }
        }
        public string Name
        {
            get { return _Name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Name cannot be null, empty, or whitespace.");
                }
                _Name = value.Trim();

            }
                
            
        }


        public List<Wall> Walls
        {
            get;
            private set;
        } = new List<Wall>();

        public Room222(string project, string name, string flooring = null, List<Wall> walls = null)
        {
            Project = project;
            Name = name;
            Flooring = flooring;

            if (walls != null)
            {
                Walls = walls;

                //Duplicate detection
                List<string> seenPlanIds = new List<string>();
                foreach (var wall in Walls)
                {
                    if (seenPlanIds.Contains(wall.PlanId))
                    {
                        throw new ArgumentException($"Duplicate PlanId '{wall.PlanId}' found in supplied walls.");
                    }
                    seenPlanIds.Add(wall.PlanId);
                }
            }
            else
            {
                Walls = new List<Wall>();
            }
        }
        public int TotalWalls
        {
            get { return Walls.Count; }
        }




        public void AddWall(Wall wall)
        {
            if (wall is null)
            {
                throw new ArgumentNullException(nameof(wall), "Parameters is missing");
            }
            //looking for a duplicate in list via a linq query
            bool wallexist = false;
            foreach (var item in Walls)
            {
                if (item.PlanId == wall.PlanId)
                {
                    wallexist = true;
                }
            }

            //looking for a duplicate in list via a Linq query.
            //wallexist = Walls.Any(x => x.PlanId == wall.PlainId)

         
            if (wallexist)
            {
                throw new ArgumentException($"Duplicate plan identify");
            }
            Walls.Add(wall);



        }
        public void RemoveWall(string planid)
        {
            if (string.IsNullOrWhiteSpace(planid))
            {
                throw new ArgumentNullException(nameof(planid), "PlanId cannot be null, empty, or whitespace.");
            }

            string trimmedPlanId = planid.Trim();

            Wall wallToRemove = null;
            foreach (var wall in Walls)
            {
                if (wall.PlanId == trimmedPlanId)
                {
                    wallToRemove = wall;
                    break;
                }
            }

            if (wallToRemove == null)
            {
                throw new ArgumentException($"Wall with PlanId '{trimmedPlanId}' not found in the room.");
            }

            Walls.Remove(wallToRemove);
        }
    }
}








        //public Room (String project, string name, string flooring, List<Wall> walls)
        //{
        //    Project = project;
        //    Name = name;
        //    Flooring = flooring;

//    if (walls != null)
//    {
//        Walls = walls;
//    }
//    else
//    {
//        Walls = new List<Wall>();
//    }

//}


