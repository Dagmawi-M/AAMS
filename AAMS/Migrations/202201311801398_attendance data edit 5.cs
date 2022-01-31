namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendancedataedit5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttendanceDatas", "Date", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AttendanceDatas", "Date", c => c.DateTime(nullable: false));
        }
    }
}
