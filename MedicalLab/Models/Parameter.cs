using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    [Index("Name", "TestTypeName", Name = "UniqueParameter", IsUnique = true)]
    public partial class Parameter
    {
        public Parameter()
        {
            Results = new HashSet<Result>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; } = null!;
        [StringLength(40)]
        public string TestTypeName { get; set; } = null!;

        [ForeignKey("TestTypeName")]
        [InverseProperty("Parameters")]
        public virtual TestType TestTypeNameNavigation { get; set; } = null!;
        [InverseProperty("Parameter")]
        public virtual ICollection<Result> Results { get; set; }

        public override string ToString() => Name;
    }
}
