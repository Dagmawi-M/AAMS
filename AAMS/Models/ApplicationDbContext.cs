using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=ApplicationDbContext")
        {
        }
        public DbSet<StudentData> StudentDatas { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<AttendanceSheet> AttendanceSheets { get; set; }
        public DbSet<AttendanceData> AttendanceDatas { get; set; }
    }
}