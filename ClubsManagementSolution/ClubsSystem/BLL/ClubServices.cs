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
    /// ClubServices - Business Logic Layer for Club operations
    /// Implements all CRUD operations and queries for Clubs
    /// Follows WestWind service pattern with internal constructor
    /// </summary>
    public class ClubServices
    {
        #region Setup of the context connection variable and class constructor
        private readonly ClubsContext _context;

        /// <summary>
        /// Internal constructor - context is injected via dependency injection
        /// </summary>
        internal ClubServices(ClubsContext registeredcontext)
        {
            _context = registeredcontext;
        }
        #endregion

        #region Query Services

        /// <summary>
        /// QUERY: Club by Status
        /// README Requirement: "Create a query that will return an ordered list of
        /// Clubs depending on the Active status. Order the list by clubname."
        /// </summary>
        /// <param name="activeStatus">True for active clubs, false for inactive</param>
        /// <returns>List of clubs filtered by status and ordered by ClubName</returns>
        public List<Club> GetClubsByStatus(bool activeStatus)
        {
            return _context.Clubs
                .Include(c => c.Employee)                   // Include employee info for display
                    .ThenInclude(e => e.Position)           // Include position for staff display
                .Where(c => c.Active == activeStatus)       // Filter by active status
                .OrderBy(c => c.ClubName)                   // Order by club name
                .ToList();
        }

        /// <summary>
        /// QUERY: Club by ID
        /// README Requirement: "Create a query that will return a club given a club id."
        /// </summary>
        /// <param name="clubId">The club ID to search for</param>
        /// <returns>Club entity if found, null otherwise</returns>
        public Club GetClubById(string clubId)
        {
            if (string.IsNullOrWhiteSpace(clubId))
            {
                throw new ArgumentNullException(nameof(clubId), "Club ID cannot be null or empty.");
            }

            return _context.Clubs
                .Include(c => c.Employee)           // Include employee for full club details
                    .ThenInclude(e => e.Position)
                .FirstOrDefault(c => c.ClubID == clubId);
        }

        /// <summary>
        /// Get all clubs for dropdown/selection purposes
        /// </summary>
        /// <returns>List of all clubs ordered by name</returns>
        public List<Club> GetAllClubs()
        {
            return _context.Clubs
                .Include(c => c.Employee)
                .OrderBy(c => c.ClubName)
                .ToList();
        }

        #endregion

        #region CRUD Services

        /// <summary>
        /// CREATE: Add a new club
        /// README Requirement: Implements business rules:
        /// - ClubID must be unique (primary key)
        /// - Club names must be unique
        /// - Club fees cannot be negative
        /// </summary>
        /// <param name="club">The club to add</param>
        /// <returns>The added club with any generated values</returns>
        public Club AddClub(Club club)
        {
            // Validation: Check if club object is null
            if (club == null)
            {
                throw new ArgumentNullException(nameof(club), "Club cannot be null.");
            }

            // Validation: ClubID is required and must be unique
            if (string.IsNullOrWhiteSpace(club.ClubID))
            {
                throw new ArgumentException("Club ID is required.");
            }

            if (_context.Clubs.Any(c => c.ClubID == club.ClubID))
            {
                throw new ArgumentException($"Club ID '{club.ClubID}' already exists. ClubID must be unique.");
            }

            // Validation: ClubName must be unique
            if (_context.Clubs.Any(c => c.ClubName == club.ClubName))
            {
                throw new ArgumentException($"Club name '{club.ClubName}' already exists. Club names must be unique.");
            }

            // Validation: Fee cannot be negative
            if (club.Fee < 0)
            {
                throw new ArgumentException("Club fee cannot be negative.");
            }

            // Validation: If EmployeeID is provided, ensure employee exists
            if (club.EmployeeID.HasValue)
            {
                var employee = _context.Employees.Find(club.EmployeeID.Value);
                if (employee == null)
                {
                    throw new ArgumentException($"Employee with ID {club.EmployeeID} does not exist.");
                }
            }

            // Add the club to the context
            _context.Clubs.Add(club);
            _context.SaveChanges();

            return club;
        }

        /// <summary>
        /// UPDATE: Modify an existing club
        /// README Requirement: Implements business rules for updates
        /// </summary>
        /// <param name="club">The club with updated information</param>
        /// <returns>The updated club</returns>
        public Club UpdateClub(Club club)
        {
            // Validation: Check if club object is null
            if (club == null)
            {
                throw new ArgumentNullException(nameof(club), "Club cannot be null.");
            }

            // Find the existing club
            var existingClub = _context.Clubs.Find(club.ClubID);
            if (existingClub == null)
            {
                throw new ArgumentException($"Club with ID '{club.ClubID}' does not exist.");
            }

            // Validation: ClubName must be unique (excluding current club)
            if (_context.Clubs.Any(c => c.ClubName == club.ClubName && c.ClubID != club.ClubID))
            {
                throw new ArgumentException($"Club name '{club.ClubName}' already exists. Club names must be unique.");
            }

            // Validation: Fee cannot be negative
            if (club.Fee < 0)
            {
                throw new ArgumentException("Club fee cannot be negative.");
            }

            // Validation: If EmployeeID is provided, ensure employee exists
            if (club.EmployeeID.HasValue)
            {
                var employee = _context.Employees.Find(club.EmployeeID.Value);
                if (employee == null)
                {
                    throw new ArgumentException($"Employee with ID {club.EmployeeID} does not exist.");
                }
            }

            // Update the properties
            existingClub.ClubName = club.ClubName;
            existingClub.Active = club.Active;
            existingClub.EmployeeID = club.EmployeeID;
            existingClub.Fee = club.Fee;

            _context.SaveChanges();

            return existingClub;
        }

        /// <summary>
        /// DEACTIVATE: Soft delete a club by setting Active = false
        /// README Requirement: "Club entries are not deleted. Rather than providing a
        /// 'delete' button, include a 'Deactivate' button and in the BLL method, change
        /// the Active flag to false and update the database for that entry."
        /// </summary>
        /// <param name="clubId">The ID of the club to deactivate</param>
        public void DeactivateClub(string clubId)
        {
            if (string.IsNullOrWhiteSpace(clubId))
            {
                throw new ArgumentNullException(nameof(clubId), "Club ID cannot be null or empty.");
            }

            var club = _context.Clubs.Find(clubId);
            if (club == null)
            {
                throw new ArgumentException($"Club with ID '{clubId}' does not exist.");
            }

            // Set Active to false (soft delete)
            club.Active = false;
            _context.SaveChanges();
        }

        /// <summary>
        /// REACTIVATE: Restore a deactivated club by setting Active = true
        /// README Requirement: "Provide a second button to reactivate a deactivated club.
        /// Reactivation will change the Active flag to true and update the database for
        /// that entry."
        /// </summary>
        /// <param name="clubId">The ID of the club to reactivate</param>
        public void ReactivateClub(string clubId)
        {
            if (string.IsNullOrWhiteSpace(clubId))
            {
                throw new ArgumentNullException(nameof(clubId), "Club ID cannot be null or empty.");
            }

            var club = _context.Clubs.Find(clubId);
            if (club == null)
            {
                throw new ArgumentException($"Club with ID '{clubId}' does not exist.");
            }

            // Set Active to true (reactivate)
            club.Active = true;
            _context.SaveChanges();
        }

        #endregion
    }
}
