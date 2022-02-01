namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _202201311801398_attendancedataedit5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "StudentCode");
            RenameColumn(table: "dbo.Users", name: "StudentCode1", newName: "StudentCode");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Users", name: "StudentCode", newName: "StudentCode1");
            AddColumn("dbo.Users", "StudentCode", c => c.String());
        }
    }
}
