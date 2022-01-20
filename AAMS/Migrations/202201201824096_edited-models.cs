namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData");
            RenameColumn(table: "dbo.AttendanceSheet", name: "Students_StdId", newName: "Students_StudentCode");
            RenameIndex(table: "dbo.AttendanceSheet", name: "IX_Students_StdId", newName: "IX_Students_StudentCode");
            DropPrimaryKey("dbo.StudentData");
            AddColumn("dbo.StudentData", "StudentCode", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.StudentData", "StudentCode");
            AddForeignKey("dbo.AttendanceSheet", "Students_StudentCode", "dbo.StudentData", "StudentCode");
            DropColumn("dbo.StudentData", "StdId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentData", "StdId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.AttendanceSheet", "Students_StudentCode", "dbo.StudentData");
            DropPrimaryKey("dbo.StudentData");
            DropColumn("dbo.StudentData", "StudentCode");
            AddPrimaryKey("dbo.StudentData", "StdId");
            RenameIndex(table: "dbo.AttendanceSheet", name: "IX_Students_StudentCode", newName: "IX_Students_StdId");
            RenameColumn(table: "dbo.AttendanceSheet", name: "Students_StudentCode", newName: "Students_StdId");
            AddForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData", "StdId");
        }
    }
}
