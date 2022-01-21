using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class RegistrarController : Controller
    {
        // GET: Registrar
        DBentities _context = new DBentities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewStudents()
        {
            var Students = _context.StudentDatas.ToList();
            return View(Students);
        }
        public ActionResult DetailsStudent(int id)
        {
            var Student = _context.StudentDatas.Where(t => t.StdId == id).FirstOrDefault();
            return View(Student);
        }
        public ActionResult AddStudents()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudents(StudentData stu)
        {
            _context.StudentDatas.Add(stu);
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }
        public ActionResult EditStudents(int id)
        {
            StudentData stu = _context.StudentDatas.Where(t => t.StdId == id).FirstOrDefault();
            return View(stu);
        }
        [HttpPost]
        public ActionResult EditStudents(StudentData stu)
        {
            StudentData eStu = _context.StudentDatas.Where(t => t.StdId == stu.StdId).FirstOrDefault();
            eStu.FirstName = stu.FirstName;
            eStu.FatherName = stu.FatherName;
            eStu.GrandFatherName = stu.GrandFatherName;
            eStu.Batch = stu.Batch;
            eStu.StudentCode = stu.StudentCode;
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }

        public ActionResult DeleteStudents(int id)
        {
            StudentData eStu = _context.StudentDatas.Where(t => t.StdId == id).FirstOrDefault();
            _context.StudentDatas.Remove(eStu);
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }

        public ActionResult ViewCourses()
        {
            var Courses = _context.Courses.ToList();
            return View(Courses);
        }
        public ActionResult DetailsCourse(int id)
        {
            var Course = _context.Courses.Where(t => t.CourseId == id).FirstOrDefault();
            return View(Course);
        }

        public ActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCourse(Course crc)
        {
            _context.Courses.Add(crc);
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        public ActionResult EditCourse(int id)
        {
            Course crc = _context.Courses.Where(t => t.CourseId == id).FirstOrDefault();
            return View(crc);
        }
        [HttpPost]
        public ActionResult EditCourse(Course crc)
        {
            Course eCrc = _context.Courses.Where(t => t.CourseId ==crc.CourseId).FirstOrDefault();
            eCrc.CourseCode = crc.CourseCode;
            eCrc.CourseName = crc.CourseName;
            eCrc.Semester = crc.Semester;
            eCrc.Year = crc.Year;
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }
        public ActionResult DeleteCourse(int id)
        {
            Course crc = _context.Courses.Where(t => t.CourseId == id).FirstOrDefault();
            _context.Courses.Remove(crc);
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        public ActionResult ViewAttendanceSheets()
        {
            //var AttendanceSheets = _context.AttendanceSheets.Select(p => new { p.CourseId, p.AssignedLecturerId}).ToList();
            var AttendanceSheets = _context.AttendanceSheets.ToList();
            return View(AttendanceSheets);
        }

        public ActionResult ListAttendanceSheet(int id)
        {
            var ASheet = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var studentsList = _context.AttendanceSheets.Where(a => a.CourseId == ASheet.CourseId && a.Section == ASheet.Section && a.AssignedLecturerId == ASheet.AssignedLecturerId).ToList();
            return View(studentsList);
        }

        public ActionResult AddAttendanceSheet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAttendanceSheet(AttendanceSheet As)
        {
            _context.AttendanceSheets.Add(As);
            _context.SaveChanges();
            return RedirectToAction("ViewAttendanceSheets");
        }
    }
}