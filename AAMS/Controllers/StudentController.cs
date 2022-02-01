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
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewStudentAttendances()
        {
            int id = (int)Session["idUser"];
            var datas = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == id).GroupBy(a => a.AttendanceSheets.CourseId).Select(a => a.FirstOrDefault()).ToList();
            return View(datas);
        }
    }
}