using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Department
    {
        public Department()
        {
            this.Professors = new HashSet<Professor>();
        }

        [Key]
        public int DepartmentID { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string DepartmentName { get; set; }

        public virtual ICollection<Professor> Professors { get; set; }
    }
}