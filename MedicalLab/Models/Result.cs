using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class Result
    {
        [Key]
        public int TestCode { get; set; }
        [Key]
        public int ParameterId { get; set; }
        [Column(TypeName = "numeric(8, 2)")]
        public decimal Value { get; set; }

        [ForeignKey("ParameterId")]
        [InverseProperty("Results")]
        public virtual Parameter Parameter { get; set; } = null!;
        [ForeignKey("TestCode")]
        [InverseProperty("Results")]
        public virtual Test TestCodeNavigation { get; set; } = null!;
    }
}
