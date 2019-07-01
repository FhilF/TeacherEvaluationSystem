using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Professor
    {

        public Professor()
        {
            this.ProfessorSubjects = new HashSet<ProfessorSubject>();
            this.Sheets = new HashSet<Sheet>();

        }

        [Key]
        public int ProfessorID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Firstname { get; set; }
        public string Middlename { get; set; }

        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department departments { get; set; }

        public virtual ICollection<ProfessorSubject> ProfessorSubjects { get; set; }
        public virtual ICollection<Sheet> Sheets { get; set; }
    }
}