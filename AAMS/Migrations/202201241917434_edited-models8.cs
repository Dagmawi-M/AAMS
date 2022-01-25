namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceSheet", "StdId", "dbo.StudentData");
            DropIndex("dbo.AttendanceSheet", new[] { "StdId" });
            RenameColumn(table: "dbo.AttendanceSheet", name: "StdId", newName: "Students_StdId");
            AddColumn("dbo.AttendanceSheet", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.AttendanceSheet", "Students_StdId", c => c.Int());
            CreateIndex("dbo.AttendanceSheet", "Students_StdId");
            AddForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData", "StdId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData");
            DropIndex("dbo.AttendanceSheet", new[] { "Students_StdId" });
            AlterColumn("dbo.AttendanceSheet", "Students_StdId", c => c.Int(nullable: false));
            DropColumn("dbo.AttendanceSheet", "StudentId");
            RenameColumn(table: "dbo.AttendanceSheet", name: "Students_StdId", newName: "StdId");
            CreateIndex("dbo.AttendanceSheet", "StdId");
            AddForeignKey("dbo.AttendanceSheet", "StdId", "dbo.StudentData", "StdId", cascadeDelete: true);
        }
    }
}
