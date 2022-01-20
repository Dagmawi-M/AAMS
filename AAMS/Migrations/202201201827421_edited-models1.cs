namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceSheet", "Students_StudentCode", "dbo.StudentData");
            DropIndex("dbo.AttendanceSheet", new[] { "Students_StudentCode" });
            RenameColumn(table: "dbo.AttendanceSheet", name: "Students_StudentCode", newName: "Students_StdId");
            DropPrimaryKey("dbo.StudentData");
            AddColumn("dbo.StudentData", "StdId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AttendanceSheet", "Students_StdId", c => c.Int());
            AlterColumn("dbo.StudentData", "StudentCode", c => c.String(nullable: false));
            AddPrimaryKey("dbo.StudentData", "StdId");
            CreateIndex("dbo.AttendanceSheet", "Students_StdId");
            AddForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData", "StdId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData");
            DropIndex("dbo.AttendanceSheet", new[] { "Students_StdId" });
            DropPrimaryKey("dbo.StudentData");
            AlterColumn("dbo.StudentData", "StudentCode", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AttendanceSheet", "Students_StdId", c => c.String(maxLength: 128));
            DropColumn("dbo.StudentData", "StdId");
            AddPrimaryKey("dbo.StudentData", "StudentCode");
            RenameColumn(table: "dbo.AttendanceSheet", name: "Students_StdId", newName: "Students_StudentCode");
            CreateIndex("dbo.AttendanceSheet", "Students_StudentCode");
            AddForeignKey("dbo.AttendanceSheet", "Students_StudentCode", "dbo.StudentData", "StudentCode");
        }
    }
}
