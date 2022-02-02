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
            return View();
        }

        public ActionResult ViewStudentAttendances()
        {
            int studentId = (int)Session["StudentId"];
            @System.Diagnostics.Debug.WriteLine(studentId);

            var datas = _db.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == studentId).GroupBy(a=>a.AttendanceSheets.CourseId).Select(a=>a.FirstOrDefault()).ToList();
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
            int present = (int)ViewBag.Present, absent = (int)ViewBag.Absent, permission = (int)ViewBag.Permission;
            int active = present + permission;
            System.Diagnostics.Debug.WriteLine(active);
            double percentage = ((present+permission) / (present+absent+permission)) * 100;
            ViewBag.Percentage = percentage.ToString();
            return View(datas);
        }
    }
}