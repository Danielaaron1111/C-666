//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace RenoSystem
//{
//    public class Room
//    {
//        // Private backing fields
//        private string _project;
//        private string _name;
//        private string _flooring;

//        // Private set collection of walls; use a List to allow add/remove
//        private List<Wall> _walls;

//        // Constants for validation
//        private const int MIN_WALLS = 1; // One or more walls allowed as per assignment

//        // Constructor with walls collection and floor optional
//        public Room(string project, string name, IEnumerable<Wall> walls = null, string flooring = null)
//        {
//            // Validate project (cannot be null)
//            if (string.IsNullOrWhiteSpace(project))
//                throw new ArgumentNullException(nameof(project), "Project identifier is required and cannot be null or empty.");

//            // Validate name (cannot be null)
//            if (string.IsNullOrWhiteSpace(name))
//                throw new ArgumentNullException(nameof(name), "Room Name is required and cannot be null or empty.");

//            _project = project.Trim();
//            _name = name.Trim();

//            // Flooring can be null but if provided, cannot be empty or whitespace only
//            if (!string.IsNullOrWhiteSpace(flooring))
//                _flooring = flooring.Trim();
//            else
//                _flooring = null;

//            // Initialize walls list, ensure no duplicates by planid
//            if (walls != null)
//            {
//                var wallList = walls.ToList();

//                // Validate uniqueness of planid in initial walls
//                var duplicatePlanIds = wallList.GroupBy(w => w.PlanId)
//                    .Where(g => g.Count() > 1)
//                    .Select(g => g.Key).ToList();

//                if (duplicatePlanIds.Count > 0)
//                    throw new ArgumentException("Duplicate Wall PlanId(s) found in initial collection: " + string.Join(", ", duplicatePlanIds));

//                _walls = new List<Wall>(wallList);
//            }
//            else
//            {
//                _walls = new List<Wall>();
//            }
//        }

//        // Public Properties with getters only (immutable except via Add/Remove methods)
//        public string Project => _project;

//        public string Name => _name;

//        public string Flooring => _flooring;

//        public IReadOnlyList<Wall> Walls => _walls.AsReadOnly();

//        public int TotalWalls => _walls.Count;

//        // Methods to add and remove walls with validations

//        public void AddWall(Wall wall)
//        {
//            if (wall == null)
//                throw new ArgumentNullException(nameof(wall), "Cannot add a null Wall instance.");

//            // Check for duplicate planid in current walls collection
//            if (_walls.Any(w => w.PlanId.Equals(wall.PlanId, StringComparison.OrdinalIgnoreCase)))
//                throw new ArgumentException($"A wall with PlanId '{wall.PlanId}' already exists and cannot be added again.");

//            _walls.Add(wall);
//        }

//        public void RemoveWall(string planId)
//        {
//            if (string.IsNullOrWhiteSpace(planId))
//                throw new ArgumentNullException(nameof(planId), "PlanId to remove cannot be null, empty, or whitespace.");

//            // Find matching wall
//            var wallToRemove = _walls.FirstOrDefault(w => w.PlanId.Equals(planId.Trim(), StringComparison.OrdinalIgnoreCase));
//            if (wallToRemove == null)
//                throw new ArgumentException($"No wall with PlanId '{planId}' was found to remove.");

//            _walls.Remove(wallToRemove);
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.Linq;

namespace RenoSystem
{
    public class Room
    {
        // Fields
        private string _flooring;
        private string _name;
        private string _project;
        private List<Wall> _walls;

        // Properties
        public string Flooring
        {
            get { return _flooring; } // get => _flooring;
            set
            {
                if (value != null && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Flooring, if provided, cannot be empty or whitespace.");
                }
                _flooring = value?.Trim();
            }
        }

        public string Name
        {
            get { return _name; }
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
            get { return _project; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value), "Project cannot be null, empty, or whitespace.");
                }
                _project = value.Trim();
            }
        }

        public int TotalWalls => _walls.Count;

        // Expose walls as read-only list to protect from external modification
        public IReadOnlyList<Wall> Walls => _walls.AsReadOnly();

        // Constructor with optional walls parameter
        public Room(string project, string name, string flooring = null, List<Wall> walls = null)
        {
            Project = project;
            Name = name;
            Flooring = flooring;
            _walls = walls ?? new List<Wall>();

            // Validate unique PlanIds in supplied walls
            var duplicatePlanIds = _walls.GroupBy(w => w.PlanId)
                                        .Where(g => g.Count() > 1)
                                        .Select(g => g.Key);

            if (duplicatePlanIds.Any())
            {
                throw new ArgumentException($"Duplicate PlanId(s) found in supplied walls: {string.Join(", ", duplicatePlanIds)}");
            }
        }

        // Methods
        public void AddWall(Wall wall)
        {
            if (wall == null)
            {
                throw new ArgumentNullException(nameof(wall), "Wall cannot be null.");
            }

            if (_walls.Any(w => w.PlanId == wall.PlanId))
            {
                throw new ArgumentException($"Wall with PlanId '{wall.PlanId}' already exists in the room.");
            }

            _walls.Add(wall);
        }

        public void RemoveWall(string planid)
        {
            if (string.IsNullOrWhiteSpace(planid))
            {
                throw new ArgumentNullException(nameof(planid), "PlanId cannot be null, empty, or whitespace.");
            }

            string trimmedPlanId = planid.Trim();
            Wall wallToRemove = _walls.FirstOrDefault(w => w.PlanId == trimmedPlanId);

            if (wallToRemove == null)
            {
                throw new ArgumentException($"Wall with PlanId '{trimmedPlanId}' not found in the room.");
            }

            _walls.Remove(wallToRemove);
        }
    }
}
