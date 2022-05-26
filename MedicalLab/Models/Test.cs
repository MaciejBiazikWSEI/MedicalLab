using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class Test
    {
        public Test()
        {
            Results = new HashSet<Result>();
        }

        [Key]
        public int Code { get; set; }
        [StringLength(40)]
        public string TestTypeName { get; set; } = null!;
        public int SampleCode { get; set; }
        public int TesterId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateFinished { get; set; }
        [StringLength(200)]
        public string? Comment { get; set; }

        [ForeignKey("SampleCode")]
        [InverseProperty("Tests")]
        public virtual Sample SampleCodeNavigation { get; set; } = null!;
        [ForeignKey("TestTypeName")]
        [InverseProperty("Tests")]
        public virtual TestType TestTypeNameNavigation { get; set; } = null!;
        [ForeignKey("TesterId")]
        [InverseProperty("Tests")]
        public virtual Tester Tester { get; set; } = null!;
        [InverseProperty("TestCodeNavigation")]
        public virtual ICollection<Result> Results { get; set; }
    }
}
