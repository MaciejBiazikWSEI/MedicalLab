using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MedicalLab.Models
{
    public partial class TestType
    {
        public TestType()
        {
            Parameters = new HashSet<Parameter>();
            Tests = new HashSet<Test>();
        }

        [Key]
        [StringLength(40)]
        public string Name { get; set; } = null!;

        [InverseProperty("TestTypeNameNavigation")]
        public virtual ICollection<Parameter> Parameters { get; set; }
        [InverseProperty("TestTypeNameNavigation")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
