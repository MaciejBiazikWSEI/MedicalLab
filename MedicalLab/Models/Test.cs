using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    /// <summary>
    /// Test done in the lab
    /// </summary>
    public partial class Test
    {
        /// <summary>
        /// Code used to identify the test
        /// </summary>
        [Key]
        public int Code { get; set; }

        /// <summary>
        /// Code of the sample, on which the test is done
        /// </summary>
        public int SampleCode { get; set; }

        /// <summary>
        /// Code of the tester assigned to the test
        /// </summary>
        public int TesterId { get; set; }

        /// <summary>
        /// Date when test was finished (null if ongoing)
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? DateFinished { get; set; }

        /// <summary>
        /// Comment describing the test
        /// </summary>
        [StringLength(200)]
        public string? Comment { get; set; }

        /// <summary>
        /// Sample, on which the test is done
        /// </summary>
        [ForeignKey("SampleCode")]
        [InverseProperty("Tests")]
        public virtual Sample SampleCodeNavigation { get; set; } = null!;
        
        /// <summary>
        /// Tester assigned to the test
        /// </summary>
        [ForeignKey("TesterId")]
        [InverseProperty("Tests")]
        public virtual Tester Tester { get; set; } = null!;
    }
}
