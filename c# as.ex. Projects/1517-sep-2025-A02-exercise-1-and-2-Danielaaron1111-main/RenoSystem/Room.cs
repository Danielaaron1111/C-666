using RenoSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

//https://github.com/CPSC-1517/Take-Home-Exercises-Sep-2025
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

            set // assign a value 
            {
                if (value != null && string.IsNullOrWhiteSpace(value)) // if is n ot equal to null and 
                { // value != null +> false , sytring.isnullorwhitespace true => false does not thron 
                    //an exception. both cases empty or white is thro and exeption (true plus true true)


                    throw new ArgumentException("Flooring, if provided, cannot be empty or whitespace.");

                } //Trim() = removes leading and trailing whitespace
                _flooring = value?.Trim(); // ? = trim oonly if value is not null >>@(?)@
            } // i dont agree that flooring can be null is annoying :c / null conditional operator 
            
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
                _name = value.Trim(); 

            }
        }

        public string Project
        {
            get => _project; // return => 
            set
            {
                if (string.IsNullOrWhiteSpace(value)) 
                {
                    throw new ArgumentNullException(nameof(value), "Project cannot be null, empty or whitespace.");
                    //@naomeof# gets the parameter name as a string ("value") - safer than hardcoding
                }
                _project = value.Trim();

            }
        }

        public int TotalWalls => Walls.Count; //(no setter, read-only)
        // wall.count gets the numbers of items in the walls list
        public List<Wall> Walls { get; private set; } //privat set to prevent outside writing 
        
        // check the name of the list ***************



        // Methods
        public void AddWall(Wall wall)
        {
            if (wall == null) // testing if its null or not and throw and exception 
            {
                throw new ArgumentNullException(nameof(wall), "Wall cannot be null.");
            }

            // if any wall return true throw and exeption
            if (Walls.Exists(w => w.PlanId == wall.PlanId)) //summary wall duplicates
                //walls.exists search for an item matching a condition, w represents each wall in the lists as its checked lambda operator
                //the condition returns true if plainid match so throw an exeption for duplicates

            {
                throw new ArgumentException($"Wall with PlanId '{wall.PlanId}' already exists in the room.");
            }

            Walls.Add(wall); //  if pass all validation add the wall to Walls
        }

        public void RemoveWall(string planid)
        {
            if (string.IsNullOrWhiteSpace(planid))
            {
                throw new ArgumentNullException(nameof(planid), "PlanId cannot be null, empty, or whitespace.");
            }

            string trimmedPlanId = planid.Trim();

            
            Wall wallToRemove = Walls.Find(w => w.PlanId == trimmedPlanId); // if a find a match and is nt null i can remove the wall
             //sanitation of string bla bla 
            if (wallToRemove == null) 
            {
                throw new ArgumentException($"Wall with PlanId '{trimmedPlanId}' not found in the room.");
            }

            Walls.Remove(wallToRemove);
        }

        //groooovy constructor
        public Room(string project, string name, string flooring = null, List<Wall> walls = null) // i make a mistake here i should have use a overleaded contructor and i declare values nuells 
        {
            Project = project;
            Name = name;
            Flooring = flooring;
            Walls = walls ?? new List<Wall>(); // if ther eis now walls i return a empty list created here to avoid nulls and ensure the room alwayhs has a list.

          
            // this was a misstypofrom another sutff i should delete this: 
            var seenPlanIds = new List<string>(); // tracking planids 
            foreach (var wall in Walls)
            {
                if (seenPlanIds.Contains(wall.PlanId))
                {
                    throw new ArgumentException($"Duplicate PlanId '{wall.PlanId}' found in supplied walls."); // trhwo expection if duplicate
                }
                seenPlanIds.Add(wall.PlanId); 
            }


            //this is another good idea but probably i dont have to track plain ids on the constructor and never do it with a list because it cost too much memory

            //// Validate for duplicate PlanIds WITHOUT creating new list
            //var duplicatePlanId = Walls
            //    .GroupBy(w => w.PlanId)
            //    .Where(g => g.Count() > 1)
            //    .Select(g => g.Key)
            //    .FirstOrDefault();

            //if (duplicatePlanId != null)
            //{
            //    throw new ArgumentException($"Duplicate PlanId '{duplicatePlanId}' found in supplied walls.");
            //}






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