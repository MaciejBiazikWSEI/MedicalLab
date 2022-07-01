using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    /// <summary>
    /// Tester working in the lab
    /// </summary>
    public partial class Tester
    {
        public Tester()
        {
            Tests = new HashSet<Test>();
        }

        /// <summary>
        /// Tester's id in the database (only for use as primary key)
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Tester's first name
        /// </summary>
        [StringLength(40)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Tester's last name
        /// </summary>
        [StringLength(20)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Tests to which the tester is assigned
        /// </summary>

        [InverseProperty("Tester")]
        public virtual ICollection<Test> Tests { get; set; }

        public override string ToString() => Id == 0 ? "<Wszyscy>" : $"{LastName}, {FirstName}";
    }
}
