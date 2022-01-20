namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceData",
                c => new
                    {
                        AttendanceSheetId = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Data = c.String(nullable: false),
                        AttendanceSheets_AttendanceSheetId = c.Int(),
                    })
                .PrimaryKey(t => new { t.AttendanceSheetId, t.Date })
                .ForeignKey("dbo.AttendanceSheet", t => t.AttendanceSheets_AttendanceSheetId)
                .Index(t => t.AttendanceSheets_AttendanceSheetId);
            
            CreateTable(
                "dbo.AttendanceSheet",
                c => new
                    {
                        AttendanceSheetId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(nullable: false),
                        CourseId = c.String(nullable: false),
                        Section = c.String(nullable: false),
                        AssignedLecturerId = c.Int(nullable: false),
                        Courses_CourseId = c.Int(),
                        Students_StdId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AttendanceSheetId)
                .ForeignKey("dbo.Course", t => t.Courses_CourseId)
                .ForeignKey("dbo.StudentData", t => t.Students_StdId)
                .Index(t => t.Courses_CourseId)
                .Index(t => t.Students_StdId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false),
                        CourseName = c.String(nullable: false),
                        Semester = c.String(nullable: false),
                        Year = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentData",
                c => new
                    {
                        StdId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false),
                        FatherName = c.String(nullable: false),
                        GrandFatherName = c.String(nullable: false),
                        Batch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StdId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceData", "AttendanceSheets_AttendanceSheetId", "dbo.AttendanceSheet");
            DropForeignKey("dbo.AttendanceSheet", "Students_StdId", "dbo.StudentData");
            DropForeignKey("dbo.AttendanceSheet", "Courses_CourseId", "dbo.Course");
            DropIndex("dbo.AttendanceSheet", new[] { "Students_StdId" });
            DropIndex("dbo.AttendanceSheet", new[] { "Courses_CourseId" });
            DropIndex("dbo.AttendanceData", new[] { "AttendanceSheets_AttendanceSheetId" });
            DropTable("dbo.StudentData");
            DropTable("dbo.Course");
            DropTable("dbo.AttendanceSheet");
            DropTable("dbo.AttendanceData");
        }
    }
}
