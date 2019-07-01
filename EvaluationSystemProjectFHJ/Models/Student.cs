using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Student
    {

        public Student()
        {
            this.Sheets = new HashSet<Sheet>();
        }

        [Key]
        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string StudentID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public int CourseID { get; set; }

        [ForeignKey("CourseID")]
        public virtual Course Courses { get; set; }

        public virtual ICollection<Sheet> Sheets { get; set; }
    }
}