using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClubsSystem.Entities
{
    /// <summary>
    /// Program entity - lookup table for academic programs
    /// </summary>
    public partial class Program
    {
        [Key]
        public int ProgramID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProgramName { get; set; }

        // Navigation Properties
        [InverseProperty("Program")]
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
