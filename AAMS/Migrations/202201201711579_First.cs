namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Roles", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Roles");
        }
    }
}
