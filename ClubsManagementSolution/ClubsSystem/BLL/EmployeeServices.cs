using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

#region Additional Namespaces
using ClubsSystem.DAL;
using ClubsSystem.Entities;
#endregion

namespace ClubsSystem.BLL
{
    /// <summary>
    /// EmployeeServices - Business Logic Layer for Employee operations
    /// Implements queries needed for Club management
    /// Follows WestWind service pattern with internal constructor
    /// </summary>
    public class EmployeeServices
    {
        #region Setup of the context connection variable and class constructor
        private readonly ClubsContext _context;

        /// <summary>
        /// Internal constructor - context is injected via dependency injection
        /// </summary>
        internal EmployeeServices(ClubsContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Query Services

        /// <summary>
        /// QUERY: Employee Club List
        /// README Requirement: "Create a query that will return an ordered list of
        /// Employees who are currently associated with a club. Order the list by last name."
        /// </summary>
        /// <returns>List of employees who are assigned to clubs, ordered by last name</returns>
        public List<Employee> GetEmployeesWithClubs()
        {
            return _context.Employees
                .Include(e => e.Clubs)                  // Include associated clubs
                .Include(e => e.Position)               // Include position information
                .Include(e => e.Program)                // Include program information
                .Where(e => e.Clubs.Any())              // Filter: only employees with clubs
                .OrderBy(e => e.LastName)               // Order by last name
                .ToList();
        }

        /// <summary>
        /// QUERY: Available Staff for Clubs
        /// README Requirement: "Create a query that will return an ordered list of
        /// Employees who are currently in one of the following positions: instructors,
        /// office administrator or technical support. Order the list by last name."
        ///
        /// This is used for the employee dropdown in the CRUD component.
        /// </summary>
        /// <returns>List of eligible employees for club assignment, ordered by last name</returns>
        public List<Employee> GetAvailableStaffForClubs()
        {
            // Define the valid position names for club staff
            var validPositions = new[] { "Instructor", "Office Administrator", "Technical Support" };

            return _context.Employees
                .Include(e => e.Position)               // Include position to filter
                .Include(e => e.Program)                // Include program for display
                .Where(e => validPositions.Contains(e.Position.PositionName))  // Filter by position
                .Where(e => e.ReleaseDate == null)      // Only active employees
                .OrderBy(e => e.LastName)               // Order by last name
                .ToList();
        }

        /// <summary>
        /// Get employee by ID with related data
        /// Useful for displaying employee details
        /// </summary>
        /// <param name="employeeId">The employee ID to retrieve</param>
        /// <returns>Employee entity with related data</returns>
        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Program)
                .Include(e => e.Clubs)
                .FirstOrDefault(e => e.EmployeeID == employeeId);
        }

        /// <summary>
        /// Get all active employees (not released)
        /// </summary>
        /// <returns>List of all active employees</returns>
        public List<Employee> GetAllActiveEmployees()
        {
            return _context.Employees
                .Include(e => e.Position)
                .Include(e => e.Program)
                .Where(e => e.ReleaseDate == null)
                .OrderBy(e => e.LastName)
                .ToList();
        }

        /// <summary>
        /// Get all positions for lookup purposes
        /// </summary>
        /// <returns>List of all positions</returns>
        public List<Position> GetAllPositions()
        {
            return _context.Positions
                .OrderBy(p => p.PositionName)
                .ToList();
        }

        /// <summary>
        /// Get all programs for lookup purposes
        /// </summary>
        /// <returns>List of all programs</returns>
        public List<Program> GetAllPrograms()
        {
            return _context.Programs
                .OrderBy(p => p.ProgramName)
                .ToList();
        }

        #endregion
    }
}
