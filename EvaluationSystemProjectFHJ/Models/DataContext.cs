using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EvaluationSystemProjectFHJ.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ProfessorSubject> ProfessorSubjects {get; set;}
        public DbSet<Admin> Admins { get; set; }

        public System.Data.Entity.DbSet<EvaluationSystemProjectFHJ.Models.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<EvaluationSystemProjectFHJ.Models.Department> Departments { get; set; }
    }
}