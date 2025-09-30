using RenoSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace RenoSystem
{
    public class Room
    {
        //fields 
        private string _flooring;
        private string _name;
        private string _project;

        public string Flooring
        {
            get => _flooring;

            set
            {
                if (value != null && string.IsNullOrWhiteSpace(value))
                {

                    throw new ArgumentException("Flooring, if provided, cannot be empty or whitespace.");

                }
                _flooring = value?.Trim(); // ? = //if value is not null, then call. trim method on it and return the result, but if value is null, then return null immeadiately instead of throwing a nullreferenceexception
            } // i dont agree that flooring can be null is annoying :c
            
        }

        public string Name
        {
            get => _name;

            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Name cannot be null, empty, or whitespace.");

                }
                _name = value.Trim(); // i coul even throw an exeption to check only-letters value because this stuff allow numeric values as a string like "macbook is better than windows 666" but is not in the instruction :(

            }
        }

        public string Project
        {
            get => _project; 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentNullException(nameof(value), "Project cannot be null, empty or whitespace.");
  
                }
                _project = value.Trim();

            }
        }

        public int TotalWalls => Walls.Count; // read only
        public List<Wall> Walls { get; private set; } //privat set 
        
        // this definately should be private but ok ill follow the diagram 7.7 where is my encapsulation stuff call the police quickly.



        // Methods
        public void AddWall(Wall wall)
        {
            if (wall == null)
            {
                throw new ArgumentNullException(nameof(wall), "Wall cannot be null.");
            }

            Predicate<Wall> duplicateCheck = w => w.PlanId == wall.PlanId;
            if (Walls.Exists(duplicateCheck))
            {
                throw new ArgumentException($"Wall with PlanId '{wall.PlanId}' already exists in the room.");
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

            Predicate<Wall> match = w => w.PlanId == trimmedPlanId;
            Wall wallToRemove = Walls.Find(match);

            if (wallToRemove == null)
            {
                throw new ArgumentException($"Wall with PlanId '{trimmedPlanId}' not found in the room.");
            }

            Walls.Remove(wallToRemove);
        }

        public Room(string project, string name, string flooring = null, List<Wall> walls = null)
        {
            Project = project;
            Name = name;
            Flooring = flooring;
            Walls = walls ?? new List<Wall>();

          
            var seenPlanIds = new List<string>();
            foreach (var wall in Walls)
            {
                if (seenPlanIds.Contains(wall.PlanId))
                {
                    throw new ArgumentException($"Duplicate PlanId '{wall.PlanId}' found in supplied walls.");
                }
                seenPlanIds.Add(wall.PlanId);
            }
        }


    }
  
    
}

//public void AddEmployment(Employment employment)
//{
//    if (employment == null)
//        throw new ArgumentNullException("Employment", "No employment supplied, missing data");

//    //one could code a loop to examine each item in the collection to determine if there
//    //  is a duplicate history instance
//    //However, lets used methods that have already been built to do searching of a collection
//    //First step: determine if you need a copy of the instance
//    //  in this case: only the knowledge that an instance exist is needed
//    //  (do not actual need the instance)
//    //  condition: only at least one needs to exist: .Any()

//    //within the method one can place one or more delegates (conditions) that
//    //  determine if the action is true or false
//    //delegate syntax structure:
//    //      collectionplaceholderlabel => collectionplaceholderlabel[.property] [condition] value 
//    //                  [ && or || another condition ...]
//    //typically the collectionplaceholderlabel is very short such x
//    //the collectionplaceholderlabel represents any instance in your collection at any time

//    if (EmploymentPositions.Any(x => x.Title == employment.Title
//                                && x.StartDate.Equals(employment.StartDate)))
//    {
//        throw new ArgumentException($"Duplicate employment: {employment.Title} on {employment.StartDate}", "Employment");
//    }

//    EmploymentPositions.Add(employment);
//}


////properties 
//public string Flooring
//{
//    get { return _Flooring; }
//    set
//    {
//        if (value != null)
//        {
//            if (string.IsNullOrWhiteSpace(value))
//            {
//                throw new ArgumentNullException("Flooring can not be null, empty or blank ");
//            }


//        }
//        else
//        { value = null; }
//        _Flooring = value;
//    }



//}


//public string Name
//{

//    get { return _Name; }

//    set
//    {

//        if (string.IsNullOrWhiteSpace(value))
//        {
//            throw new ArgumentNullException("Name can not be null empty or blank");
//        }
//        _Name = value.Trim();
//    }

//}

//public string Project
//{
//    get { return _Project; }

//    set
//    {
//        if (string.IsNullOrEmpty(value))
//        {
//            throw new ArgumentNullException("Project field can not be null empty or blank");
//        }
//        _Project = value.Trim();
//    }
//}


//this constructors works better because the LINQ
//public Room(string project, string name, string flooring, List<Wall> walls)
//{
//    Project = project;
//    Name = name;
//    Flooring = flooring;

//    // Assign walls or initialize empty list if null
//    Walls = walls ?? new List<Wall>();

//    // Validate unique PlanIds in supplied walls 
//    var duplicatePlanIds = Walls.GroupBy(w => w.PlanId)
//                                .Where(g => g.Count() > 1) // delegates partying in SQL house. :) 
//                                .Select(g => g.Key);

//    if (duplicatePlanIds.Any())
//    {
//        throw new ArgumentException($"Duplicate PlanId(s) found in supplied walls: {string.Join(", ", duplicatePlanIds)}");
//    }
//}