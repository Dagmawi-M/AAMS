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
        int attendanceId = 0;
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
            //attendanceId = id;
            return View(studentsList);
        }

        public ActionResult CreateAttendance()
        {
            int id = (int)TempData["AttendanceId"];
           // int id = attendanceId;
            System.Diagnostics.Debug.WriteLine(id);
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var studentsList = _context.AttendanceSheets.Where(a => a.CourseId == attendance.CourseId && a.Section == attendance.Section).ToList();
            string today = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt");
            foreach (var item in studentsList)
            {
                System.Diagnostics.Debug.WriteLine("here");
                AttendanceData ad = new AttendanceData
                {
                    AttendanceSheetId = item.AttendanceSheetId,
                    Date = today
                };
                System.Diagnostics.Debug.WriteLine(ad.AttendanceSheetId);
                System.Diagnostics.Debug.WriteLine(ad.Date);
                _context.AttendanceDatas.Add(ad); 
                _context.SaveChanges();
            }
            var data = _context.AttendanceDatas.Where(a => a.Date == today).FirstOrDefault();
            return RedirectToAction("TakeAttendance");

        }
        public ActionResult TakeAttendance()
        {
            int id = (int)TempData["AttendanceId"];
            var data = _context.AttendanceDatas.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var datas = _context.AttendanceDatas.Where(a => a.Date == data.Date).ToList();
            return View(datas);
        }
        //[HttpPost]
        //public void TakeAttendance(string value)
        //{
        //    var space = value.IndexOf(" ");
        //    var id = value.Substring(0, space);
        //    var data = value.Substring(space + 1);
        //    var attendance = _context.AttendanceDatas.Where(a=>a.AttendanceDataID.ToString()==id).FirstOrDefault();
        //    attendance.Data = data;
        //    _context.SaveChanges();
        //}

        public ActionResult Present(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Present";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            return RedirectToAction("TakeAttendance");
        }
        public ActionResult Absent(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Absent";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            return RedirectToAction("TakeAttendance");
        }
        public ActionResult Permission(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Permission";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            return RedirectToAction("TakeAttendance");
        }

        public ActionResult EditAttendance(int id)
        {
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var datas = _context.AttendanceDatas.Where(a => a.AttendanceSheetId == id).ToList();
            return View(datas);
        }
        public ActionResult DeleteAttendance(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            var courseId = attendance.AttendanceSheets.CourseId;
            _context.AttendanceDatas.Remove(attendance);
            _context.SaveChanges();
            var sheet = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == courseId).FirstOrDefault();
            return RedirectToAction("EditAttendance",attendance.AttendanceSheetId);
        }
    }
}