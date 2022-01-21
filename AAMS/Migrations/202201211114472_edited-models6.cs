namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "Year", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Course", "Year", c => c.DateTime(nullable: false));
        }
    }
}
