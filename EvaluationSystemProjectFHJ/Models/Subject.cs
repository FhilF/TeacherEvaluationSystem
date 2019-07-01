using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Subject
    {
        public Subject()
        {
            this.ProfessorSubjects = new HashSet<ProfessorSubject>();
        }

        [Key]
        public int SubjectID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string SubjectCode { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string SubjectName { get; set; }

        public virtual ICollection<ProfessorSubject> ProfessorSubjects { get; set; }
    }
}