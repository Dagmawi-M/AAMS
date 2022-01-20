namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.AttendanceData");
            AddColumn("dbo.AttendanceData", "AttendanceDataId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AttendanceData", "AttendanceSheetId", c => c.String(nullable: false));
            AddPrimaryKey("dbo.AttendanceData", "AttendanceDataId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.AttendanceData");
            AlterColumn("dbo.AttendanceData", "AttendanceSheetId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AttendanceData", "AttendanceDataId");
            AddPrimaryKey("dbo.AttendanceData", new[] { "AttendanceSheetId", "Date" });
        }
    }
}
