namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendancedataedit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AttendanceDatas", "Data", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AttendanceDatas", "Data", c => c.String(nullable: false));
        }
    }
}
