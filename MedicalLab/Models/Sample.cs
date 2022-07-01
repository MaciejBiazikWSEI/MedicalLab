using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    /// <summary>
    /// Sample used in tests
    /// </summary>
    public partial class Sample
    {
        public Sample()
        {
            Tests = new HashSet<Test>();
        }

        /// <summary>
        /// Code used to identify the sample
        /// </summary>
        [Key]
        public int Code { get; set; }

        /// <summary>
        /// Code of the patient from whom the sample was taken
        /// </summary>
        public int PatientCode { get; set; }

        /// <summary>
        /// Comment describing the sample
        /// </summary>
        [StringLength(200)]
        public string? Comment { get; set; }

        /// <summary>
        /// Patient from whom the sample was taken
        /// </summary>
        [ForeignKey("PatientCode")]
        [InverseProperty("Samples")]
        public virtual Patient PatientCodeNavigation { get; set; } = null!;

        /// <summary>
        /// Tests done on the sample
        /// </summary>
        [InverseProperty("SampleCodeNavigation")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
