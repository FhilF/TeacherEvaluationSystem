namespace EvaluationSystemProjectFHJ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professors", "Middlename", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professors", "Middlename", c => c.String(nullable: false, maxLength: 60));
        }
    }
}
