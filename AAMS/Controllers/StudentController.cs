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
    }
}