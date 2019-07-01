using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvaluationSystemProjectFHJ.Models
{
    public class StudentSheetData
    {
        public IEnumerable<Sheet> CollectionSheet { get; set; }
        public IEnumerable<Student> CollectionStudent { get; set; }
    }
}