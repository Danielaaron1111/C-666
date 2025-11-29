using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsSystem.Entities
{
    /// <summary>
    /// Employee entity - represents staff members who can manage clubs
    /// Generated based on the Employees table schema
    /// </summary>
    public partial class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(20, ErrorMessage = "First name cannot exceed 20 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(20, ErrorMessage = "Last name cannot exceed 20 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date hired is required")]
        [Column(TypeName = "datetime")]
        public DateTime DateHired { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public int PositionID { get; set; }

        [Required(ErrorMessage = "Program is required")]
        public int ProgramID { get; set; }

        [Required(ErrorMessage = "Login ID is required")]
        [StringLength(30, ErrorMessage = "Login ID cannot exceed 30 characters")]
        public string LoginID { get; set; }

        // Navigation Properties
        [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

        [ForeignKey("ProgramID")]
        public virtual Program Program { get; set; }

        [InverseProperty("Employee")]
        public virtual ICollection<Club> Clubs { get; set; } = new List<Club>();

        // Computed Property (Assignment Requirement - Entity Employee Entity section)
        // README: "Add the following to the entity for use in your application:
        // public string FullName { get {return LastName + ", " + FirstName;}}"
        [NotMapped]
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }
    }
}
