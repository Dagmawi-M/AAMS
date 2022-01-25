namespace AAMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttendanceDatas",
                c => new
                    {
                        AttendanceDataID = c.Int(nullable: false, identity: true),
                        AttendanceSheetID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Data = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceDataID)
                .ForeignKey("dbo.AttendanceSheets", t => t.AttendanceSheetID, cascadeDelete: true)
                .Index(t => t.AttendanceSheetID);
            
            CreateTable(
                "dbo.AttendanceSheets",
                c => new
                    {
                        AttendanceSheetId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        Section = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AttendanceSheetId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.StudentDatas", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false),
                        CourseName = c.String(nullable: false),
                        Semester = c.String(nullable: false),
                        Year = c.String(nullable: false),
                        AssignedLecturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.StudentDatas",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentCode = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        FatherName = c.String(nullable: false),
                        GrandFatherName = c.String(nullable: false),
                        Batch = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttendanceDatas", "AttendanceSheetID", "dbo.AttendanceSheets");
            DropForeignKey("dbo.AttendanceSheets", "StudentId", "dbo.StudentDatas");
            DropForeignKey("dbo.AttendanceSheets", "CourseId", "dbo.Courses");
            DropIndex("dbo.AttendanceSheets", new[] { "CourseId" });
            DropIndex("dbo.AttendanceSheets", new[] { "StudentId" });
            DropIndex("dbo.AttendanceDatas", new[] { "AttendanceSheetID" });
            DropTable("dbo.StudentDatas");
            DropTable("dbo.Courses");
            DropTable("dbo.AttendanceSheets");
            DropTable("dbo.AttendanceDatas");
        }
    }
}
