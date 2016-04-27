namespace mytinycollege.Migrations.CollegeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "FirstName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Person", "FristName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "FristName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Person", "FirstName");
        }
    }
}
