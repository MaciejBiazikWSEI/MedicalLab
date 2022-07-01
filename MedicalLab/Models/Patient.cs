using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    /// <summary>
    /// Patient of the Medical Lab
    /// </summary>
    public partial class Patient
    {
        public Patient()
        {
            Samples = new HashSet<Sample>();
        }

        /// <summary>
        /// Code used to identify the patient
        /// </summary>
        [Key]
        public int Code { get; set; }

        /// <summary>
        /// Patient's first name
        /// </summary>
        [StringLength(40)]
        public string FirstName { get; set; } = null!;

        /// <summary>
        /// Patient's last name
        /// </summary>
        [StringLength(20)]
        public string LastName { get; set; } = null!;

        /// <summary>
        /// Patient's date of birth
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Samples taken from the patient
        /// </summary>
        [InverseProperty("PatientCodeNavigation")]
        public virtual ICollection<Sample> Samples { get; set; }
    }
}
