namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedmodels7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AttendanceSheet", "Courses_CourseId", "dbo.Course");
            DropIndex("dbo.AttendanceSheet", new[] { "Courses_CourseId" });
            DropColumn("dbo.AttendanceSheet", "CourseId");
            RenameColumn(table: "dbo.AttendanceSheet", name: "Courses_CourseId", newName: "CourseId");
            AlterColumn("dbo.AttendanceSheet", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.AttendanceSheet", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.AttendanceSheet", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.AttendanceSheet", "CourseId");
            AddForeignKey("dbo.AttendanceSheet", "CourseId", "dbo.Course", "CourseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceSheet", "CourseId", "dbo.Course");
            DropIndex("dbo.AttendanceSheet", new[] { "CourseId" });
            AlterColumn("dbo.AttendanceSheet", "CourseId", c => c.Int());
            AlterColumn("dbo.AttendanceSheet", "CourseId", c => c.String(nullable: false));
            AlterColumn("dbo.AttendanceSheet", "StudentId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.AttendanceSheet", name: "CourseId", newName: "Courses_CourseId");
            AddColumn("dbo.AttendanceSheet", "CourseId", c => c.String(nullable: false));
            CreateIndex("dbo.AttendanceSheet", "Courses_CourseId");
            AddForeignKey("dbo.AttendanceSheet", "Courses_CourseId", "dbo.Course", "CourseId");
        }
    }
}
