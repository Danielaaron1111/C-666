using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsSystem.Entities
{
    /// <summary>
    /// Club entity - represents student clubs
    /// Generated based on the Clubs table schema
    /// </summary>
    public partial class Club
    {
        [Key]
        [Required(ErrorMessage = "Club ID is required")]
        [StringLength(10, ErrorMessage = "Club ID cannot exceed 10 characters")]
        public string ClubID { get; set; }

        [Required(ErrorMessage = "Club name is required")]
        [StringLength(50, ErrorMessage = "Club name cannot exceed 50 characters")]
        public string ClubName { get; set; }

        [Required]
        public bool Active { get; set; }

        // EmployeeID is nullable - a club might not have a staff member assigned
        public int? EmployeeID { get; set; }

        [Required(ErrorMessage = "Fee is required")]
        [Column(TypeName = "money")]
        [Range(0, double.MaxValue, ErrorMessage = "Fee cannot be negative")]
        public decimal Fee { get; set; }

        // Navigation Property
        [ForeignKey("EmployeeID")]
        [InverseProperty("Clubs")]
        public virtual Employee Employee { get; set; }

        // Helper property to display staff information
        [NotMapped]
        public string StaffDisplay
        {
            get
            {
                return Employee != null ? Employee.FullName : "No staff member";
            }
        }
    }
}
