using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsSystem.Entities
{
    /// <summary>
    /// Position entity - lookup table for employee positions
    /// </summary>
    public partial class Position
    {
        [Key]
        public int PositionID { get; set; }

        [Required]
        [StringLength(50)]
        public string PositionName { get; set; }

        // Navigation Properties
        [InverseProperty("Position")]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
