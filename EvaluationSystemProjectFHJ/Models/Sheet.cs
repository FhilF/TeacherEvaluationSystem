using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluationSystemProjectFHJ.Models
{
    public class Sheet
    {
        [Key]
        public int SheetID { get; set; }

        public string StudentID { get; set; }
        public int ProfessorID { get; set; }
        public string Subject { get; set; }

        public int QuestionOne { get; set; }
        public int QuestionTwo { get; set; }
        public int QuestionThree { get; set; }
        public int QuestionFour { get; set; }
        public int QuestionFive { get; set; }

        public DateTime DateSubmitted { get; set; }

        [ForeignKey("StudentID")]
        public virtual Student Students { get; set; }
        [ForeignKey("ProfessorID")]
        public virtual Professor Professors { get; set; }
    }
}