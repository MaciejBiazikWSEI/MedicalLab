using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Samples = new HashSet<Sample>();
        }

        [Key]
        public int Code { get; set; }
        [StringLength(40)]
        public string FirstName { get; set; } = null!;
        [StringLength(20)]
        public string LastName { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        [Column("PESEL")]
        [StringLength(11)]
        [Unicode(false)]
        public string? Pesel { get; set; }

        [InverseProperty("PatientCodeNavigation")]
        public virtual ICollection<Sample> Samples { get; set; }
    }
}
