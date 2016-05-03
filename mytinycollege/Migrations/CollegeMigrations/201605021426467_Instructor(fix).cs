namespace mytinycollege.Migrations.CollegeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Instructorfix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Department", name: "Administrator_ID", newName: "InstructorID");
            RenameIndex(table: "dbo.Department", name: "IX_Administrator_ID", newName: "IX_InstructorID");
            DropColumn("dbo.Department", "InstrustorID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Department", "InstrustorID", c => c.Int());
            RenameIndex(table: "dbo.Department", name: "IX_InstructorID", newName: "IX_Administrator_ID");
            RenameColumn(table: "dbo.Department", name: "InstructorID", newName: "Administrator_ID");
        }
    }
}
