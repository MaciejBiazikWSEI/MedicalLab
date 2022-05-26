using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class Sample
    {
        public Sample()
        {
            Tests = new HashSet<Test>();
        }

        [Key]
        public int Code { get; set; }
        public int PatientCode { get; set; }
        [StringLength(200)]
        public string? Comment { get; set; }

        [ForeignKey("PatientCode")]
        [InverseProperty("Samples")]
        public virtual Patient PatientCodeNavigation { get; set; } = null!;
        [InverseProperty("SampleCodeNavigation")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
