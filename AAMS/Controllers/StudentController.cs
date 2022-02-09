using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
 
        private ApplicationDbContext _db = new ApplicationDbContext();

        public StudentController()
        {

        }

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
  
        public ActionResult Index()
        {
            var course = _db.Courses.ToList();
            return View(course);
   
        }

        public ActionResult ViewStudentAttendances()
        {
            int studentId = (int)Session["StudentId"];
            @System.Diagnostics.Debug.WriteLine(studentId);

            var datas = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == studentId).OrderBy(a => a.AttendanceSheets.Courses.CourseName).GroupBy(a=>a.AttendanceSheets.CourseId).Select(a=>a.FirstOrDefault()).ToList();
           // @System.Diagnostics.Debug.WriteLine("test : ", _db.AttendanceDatas.Count());
            return View(datas);
        }

        public ActionResult DetailsStudentAttendance(int id)
        {
            var attendance = _db.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            var datas = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == attendance.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == attendance.AttendanceSheets.CourseId).ToList();
            ViewBag.Present = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == attendance.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == attendance.AttendanceSheets.CourseId && a.Data=="Present").ToList().Count();
            ViewBag.Absent = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == attendance.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == attendance.AttendanceSheets.CourseId && a.Data == "Absent").ToList().Count();
            ViewBag.Permission = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == attendance.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == attendance.AttendanceSheets.CourseId && a.Data == "Permission").ToList().Count();
            double present = (double)ViewBag.Present, absent = (double)ViewBag.Absent, permission = (double)ViewBag.Permission;
            double active = present + permission, total = present + permission + absent;
            double percentage;
            try
            {
                percentage = (active / total) * 100;
            }
            catch (DivideByZeroException)
            {
                percentage = 0;
            }
            ViewBag.Percentage = percentage;
            return View(datas);
        }

      
    }
}