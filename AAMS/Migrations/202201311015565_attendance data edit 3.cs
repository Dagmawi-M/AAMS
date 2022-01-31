namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attendancedataedit3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AttendanceDatas", new[] { "AttendanceSheetID" });
            CreateIndex("dbo.AttendanceDatas", "AttendanceSheetId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AttendanceDatas", new[] { "AttendanceSheetId" });
            CreateIndex("dbo.AttendanceDatas", "AttendanceSheetID");
        }
    }
}
