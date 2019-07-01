namespace EvaluationSystemProjectFHJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.CourseID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentID = c.String(nullable: false, maxLength: 60),
                        Lastname = c.String(nullable: false, maxLength: 60),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Middlename = c.String(),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Sheets",
                c => new
                    {
                        SheetID = c.Int(nullable: false, identity: true),
                        StudentID = c.String(maxLength: 60),
                        ProfessorID = c.Int(nullable: false),
                        Subject = c.String(),
                        QuestionOne = c.Int(nullable: false),
                        QuestionTwo = c.Int(nullable: false),
                        QuestionThree = c.Int(nullable: false),
                        QuestionFour = c.Int(nullable: false),
                        QuestionFive = c.Int(nullable: false),
                        DateSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SheetID)
                .ForeignKey("dbo.Professors", t => t.ProfessorID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentID)
                .Index(t => t.StudentID)
                .Index(t => t.ProfessorID);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        ProfessorID = c.Int(nullable: false, identity: true),
                        Lastname = c.String(nullable: false, maxLength: 60),
                        Firstname = c.String(nullable: false, maxLength: 100),
                        Middlename = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.ProfessorID);
            
            CreateTable(
                "dbo.ProfessorSubjects",
                c => new
                    {
                        ProfessorSubjectID = c.Int(nullable: false, identity: true),
                        ProfessorID = c.Int(nullable: false),
                        SubjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProfessorSubjectID)
                .ForeignKey("dbo.Professors", t => t.ProfessorID, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectID, cascadeDelete: true)
                .Index(t => t.ProfessorID)
                .Index(t => t.SubjectID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectID = c.Int(nullable: false, identity: true),
                        SubjectCode = c.String(nullable: false, maxLength: 20),
                        SubjectName = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.SubjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sheets", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Sheets", "ProfessorID", "dbo.Professors");
            DropForeignKey("dbo.ProfessorSubjects", "SubjectID", "dbo.Subjects");
            DropForeignKey("dbo.ProfessorSubjects", "ProfessorID", "dbo.Professors");
            DropForeignKey("dbo.Students", "CourseID", "dbo.Courses");
            DropIndex("dbo.ProfessorSubjects", new[] { "SubjectID" });
            DropIndex("dbo.ProfessorSubjects", new[] { "ProfessorID" });
            DropIndex("dbo.Sheets", new[] { "ProfessorID" });
            DropIndex("dbo.Sheets", new[] { "StudentID" });
            DropIndex("dbo.Students", new[] { "CourseID" });
            DropTable("dbo.Subjects");
            DropTable("dbo.ProfessorSubjects");
            DropTable("dbo.Professors");
            DropTable("dbo.Sheets");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
        }
    }
}
