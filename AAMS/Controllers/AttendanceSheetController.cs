using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class AttendanceSheetController : Controller
    {
        // GET: AttendanceSheet
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("ViewAttendanceSheets");
        }
        public ActionResult ViewAttendanceSheets()
        {
            var uniqueSheets = _context.AttendanceSheets.Select(a => new { a.CourseId, a.Courses, a.Section }).Distinct().ToList();
            ViewBag.unique = uniqueSheets;
            return View(_context.AttendanceSheets.GroupBy(a => new { a.CourseId, a.Section }).Select(a => a.FirstOrDefault()).ToList()); 
        }

        public ActionResult ListAttendanceSheet(int id)
        {
            var attendanceSheet = _context.AttendanceSheets.Where(ash => ash.AttendanceSheetId == id).FirstOrDefault();
            var studentsList = _context.AttendanceSheets.Where(ash => ash.CourseId == attendanceSheet.CourseId
                && ash.Section == attendanceSheet.Section).OrderBy(a=>a.Students.FirstName).ToList();
            ViewBag.courseCode = attendanceSheet.Courses.CourseCode;
            ViewBag.courseName = attendanceSheet.Courses.CourseName;
            ViewBag.assignedLecturer = attendanceSheet.Courses.AssignedLecturerId;
            ViewBag.section = attendanceSheet.Section;
            return View(studentsList);
        }

        public ActionResult AddAttendanceSheet()
        {
            ViewBag.courses = _context.Courses.ToList();
            ViewBag.students = _context.StudentDatas.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddAttendanceSheet(AttendanceSheet attendanceSheet)
        {
            _context.AttendanceSheets.Add(attendanceSheet);   
            _context.SaveChanges();
            return RedirectToAction("ViewAttendanceSheets");
        }

        public ActionResult AddStudentToAttendanceSheet(string code)
        {
            var sheet = _context.AttendanceSheets.Where(a => a.Courses.CourseCode == code).FirstOrDefault();
            ViewBag.CourseId = sheet.CourseId;
            ViewBag.students = _context.StudentDatas.ToList();
            return View();

        }
        [HttpPost]
        public ActionResult AddStudentToAttendanceSheet(AttendanceSheet sheet)
        {
            sheet.CourseId = (int)TempData["CourseId"];
            _context.AttendanceSheets.Add(sheet);
            _context.SaveChanges();
            return RedirectToAction("ListAttendanceSheet", new { id = sheet.AttendanceSheetId});

        }

        public ActionResult DeleteStudentAttendance(int id)
        {
            var sheet = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            _context.AttendanceSheets.Remove(sheet);
            _context.SaveChanges();
            return RedirectToAction("ListAttendanceSheet", new { id = sheet.AttendanceSheetId });
        }

        public ActionResult DeleteAttendanceSheet(int id)
        {
            var sheet = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var sheets = _context.AttendanceSheets.Where(a => a.CourseId == sheet.CourseId).ToList();
            foreach(var item in sheets)
            {
                _context.AttendanceSheets.Remove(item);
            }
            _context.SaveChanges();
            return RedirectToAction("ViewAttendanceSheets");
        }
    }
}