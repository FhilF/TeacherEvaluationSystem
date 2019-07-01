namespace EvaluationSystemProjectFHJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
            AddColumn("dbo.Professors", "DepartmentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Professors", "DepartmentID");
            AddForeignKey("dbo.Professors", "DepartmentID", "dbo.Departments", "DepartmentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Professors", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Professors", new[] { "DepartmentID" });
            DropColumn("dbo.Professors", "DepartmentID");
            DropTable("dbo.Departments");
        }
    }
}
