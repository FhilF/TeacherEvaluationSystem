using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class ProfessorSubject
    {
        [Key]
        public int ProfessorSubjectID { get; set; }

        public int ProfessorID { get; set; }
        public int SubjectID { get; set; }

        [ForeignKey("ProfessorID")]
        public virtual Professor Professors { get; set; }
        [ForeignKey("SubjectID")]
        public virtual Subject Subjects { get; set; }
    }
}