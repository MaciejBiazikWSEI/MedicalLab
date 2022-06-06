using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class Tester
    {
        public Tester()
        {
            Tests = new HashSet<Test>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(40)]
        public string FirstName { get; set; } = null!;
        [StringLength(20)]
        public string LastName { get; set; } = null!;

        [InverseProperty("Tester")]
        public virtual ICollection<Test> Tests { get; set; }

        public override string ToString() => $"{LastName}, {FirstName}";
    }
}
