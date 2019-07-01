using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        public int CourseID { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 1)]
        public string CourseCode { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}