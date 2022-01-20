using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class DBentities : DbContext
    {
        public DBentities() : base("DatabaseMVC5") { }
        public DbSet<User> Users { get; set; }

        public DbSet<StudentData> StudentDatas { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<AttendanceSheet> AttendanceSheets {get; set;}
        public DbSet<AttendanceData> AttendanceDatas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<demoEntities>(null);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}