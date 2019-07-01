namespace EvaluationSystemProjectFHJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mymigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false, maxLength: 20),
                        Firstname = c.String(nullable: false, maxLength: 80),
                        Lastname = c.String(nullable: false, maxLength: 80),
                        Middlename = c.String(),
                        Position = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Username);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Admins");
        }
    }
}
