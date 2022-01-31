using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class LecturerController : Controller
    {
        // GET: Lecturer
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            //  return RedirectToAction("ViewAttendances");
            return View();
        }
        public ActionResult ViewAttendances()
        {
            int id = (int)Session["idUser"];
            var attendances = _context.AttendanceSheets.Where(a => a.Courses.AssignedLecturerId == id).GroupBy(a=> new { a.CourseId, a.Section }).Select(a => a.FirstOrDefault()).ToList();
            return View(attendances);
        }

        public ActionResult ViewStudents(int id)
        {
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var studentsList = _context.AttendanceSheets.Where(a => a.CourseId == attendance.CourseId && a.Section == attendance.Section).ToList();
            TempData["AttendanceId"] = id;
            return View(studentsList);
        }

        public ActionResult TakeAttendance()
        {
            int id = (int)Session["idUser"];
            System.Diagnostics.Debug.WriteLine(id);
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var studentsList = _context.AttendanceSheets.Where(a => a.CourseId == attendance.CourseId && a.Section == attendance.Section).ToList();
            foreach(var item in studentsList)
            {
                System.Diagnostics.Debug.WriteLine("here");
                AttendanceData ad = new AttendanceData
                {
                    AttendanceSheetId = item.AttendanceSheetId,
                    Date = DateTime.Now
                };
                _context.AttendanceDatas.Add(ad); 
                _context.SaveChanges();
            }
            var datas = _context.AttendanceDatas.Where(a=>a.Date==DateTime.Now).ToList();
            return View(datas);
        }
    }
}