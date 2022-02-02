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
       // int attendanceId = 0;

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
            TempData["AttendanceId"] = data.AttendanceSheetId;
            TempData["Date"] = today;
            return RedirectToAction("TakeAttendance");

        }
        public ActionResult TakeAttendance()
        {
            int id = (int)TempData["AttendanceId"];
            string date = TempData["Date"].ToString();
            var datas = _context.AttendanceDatas.Where(a => a.AttendanceSheetId == id && a.Date== date).ToList();
            return View(datas);
        }

        public ActionResult Present(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Present";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance");
        }
        public ActionResult Absent(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Absent";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance");
        }
        public ActionResult Permission(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Permission";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance");
        }

        public ActionResult EditAttendance(int id)
        {
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var datas = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == attendance.CourseId && a.AttendanceSheets.Section==attendance.Section).ToList();
            return View(datas);
        }
        public ActionResult DeleteAttendance(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            var courseId = attendance.AttendanceSheets.CourseId;
            _context.AttendanceDatas.Remove(attendance);
            _context.SaveChanges();
            var sheet = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == courseId).FirstOrDefault();
            return RedirectToAction("EditAttendance",new { id = sheet.AttendanceSheetId });
        }

        public ActionResult MarkAttendance(int id)
        {
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var datas = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == attendance.CourseId && a.AttendanceSheets.Section == attendance.Section && a.Data=="Present").GroupBy(a=> a.AttendanceSheets.StudentId).Select(a => a.FirstOrDefault()).ToList();
            
            return View(datas);
        }
        public ActionResult EditAttendanceData (int id)
        {
            var data = _context.AttendanceDatas.Where(d => d.AttendanceDataID == id).FirstOrDefault();
            ViewBag.StudentCode = data.AttendanceSheets.Students.StudentCode;
            ViewBag.FirstName = data.AttendanceSheets.Students.FirstName;
            ViewBag.FatherName = data.AttendanceSheets.Students.FatherName;
            ViewBag.GrandFatherName = data.AttendanceSheets.Students.GrandFatherName;
            ViewBag.Batch = data.AttendanceSheets.Students.Batch;
            ViewBag.Date = data.Date;
            return View();
        }
        [HttpPost]
        public ActionResult EditAttendanceData(AttendanceData ad,string data , string state)
        {
            var existing = _context.AttendanceDatas.Where(d => d.AttendanceDataID == ad.AttendanceDataID).FirstOrDefault();
                System.Diagnostics.Debug.WriteLine(ad.Date); 
            existing.Data = state;
            _context.SaveChanges();
            return RedirectToAction("EditAttendanceData", new { id = ad.AttendanceDataID });
        }

        public ActionResult ViewStatus(int id)
        {
            var sheet = _context.AttendanceDatas.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var student = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId==sheet.AttendanceSheets.CourseId).ToList();
            ViewBag.Absent = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data=="Absent").ToList().Count();
            ViewBag.Present = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data == "Present").ToList().Count();
            ViewBag.Permission = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data == "Permission").ToList().Count();
            return View(student);
        }
    }
}