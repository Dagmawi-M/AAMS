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
            var sheets = _context.AttendanceSheets.ToList();
            return View(sheets);
         
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

        public ActionResult CreateAttendance(int id)
        {
           // int id = (int)TempData["AttendanceId"];
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
            return RedirectToAction("TakeAttendance", new { id=data.AttendanceSheetId, date=today });

        }
        public ActionResult TakeAttendance(string date)
        {
            //int id = (int)TempData["AttendanceId"];
            //string date = TempData["Date"].ToString();
            var datas = _context.AttendanceDatas.Where(a=>a.Date== date).ToList();
            return View(datas);
        }

        public ActionResult Present(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Present";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance", new { date = attendance.Date });
        }
        public ActionResult Absent(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Absent";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance", new { date = attendance.Date });
        }
        public ActionResult Permission(int id)
        {
            var attendance = _context.AttendanceDatas.Where(a => a.AttendanceDataID == id).FirstOrDefault();
            attendance.Data = "Permission";
            _context.SaveChanges();
            TempData["AttendanceId"] = attendance.AttendanceSheetId;
            TempData["Date"] = attendance.Date;
            return RedirectToAction("TakeAttendance", new { date = attendance.Date });
        }

        public ActionResult EditAttendance(int id, string date="")
        {
            var attendance = _context.AttendanceSheets.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var datas = new List<AttendanceData>();
            if (date == "")
                datas = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == attendance.CourseId && a.AttendanceSheets.Section == attendance.Section).ToList();
            else
               datas = _context.AttendanceDatas.Where(a => a.AttendanceSheets.CourseId == attendance.CourseId && a.AttendanceSheets.Section == attendance.Section && a.Date==date).ToList();
            var dates = _context.AttendanceDatas.GroupBy(a => a.Date).Select(a => a.FirstOrDefault()).ToList();
            TempData["AttendanceId"] = datas.First().AttendanceSheetId;
            ViewBag.Dates = dates;
            return View(datas);
        }
        [HttpPost]
        public ActionResult EditAttendance()
        {
            string filter = Request.Form["filter"].ToString();
            int id = (int)TempData["AttendanceId"];
            return RedirectToAction("EditAttendance", new { id = id, date = filter });
        }

        public ActionResult FilterAttendance(int id)
        {
            string filter = Request.Form["filter"].ToString();
            return RedirectToAction("EditAttendance", new { id = id, date = filter });
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
            var percent = new List<double>();
            var outOfFive = new List<double>();
            var outOfTen = new List<double>();
            foreach(var item in datas)
            {
                double present = (double)_context.AttendanceDatas.Where(d => d.AttendanceSheets.StudentId == item.AttendanceSheets.StudentId && d.Data == "Present").ToList().Count();
                double absent = (double)_context.AttendanceDatas.Where(d => d.AttendanceSheets.StudentId == item.AttendanceSheets.StudentId && d.Data == "Absent").ToList().Count();
                double permission = (double)_context.AttendanceDatas.Where(d => d.AttendanceSheets.StudentId == item.AttendanceSheets.StudentId && d.Data == "Permission").ToList().Count();
                double active = present + permission, total = present + permission + absent;
                double percentage, resultFive, resultTen;
                try
                {
                    percentage = (active / total) * 100;
                }
                catch (DivideByZeroException)
                {
                    percentage = 0;
                }
                percent.Add(percentage);
                resultFive = (percentage / 100) * 5;
                resultTen = (percentage / 100) * 10;
                outOfFive.Add(resultFive);
                outOfTen.Add(resultTen);
            }
            ViewBag.Percent = percent;
            ViewBag.OutOfFive = outOfFive;
            ViewBag.OutOfTen = outOfTen;

            return View(datas);
        }
        public ActionResult EditAttendanceData (int id)
        {
            var data = _context.AttendanceDatas.Where(d => d.AttendanceDataID == id).FirstOrDefault();
            ViewBag.AttendanceDataID = id;
            ViewBag.StudentCode = data.AttendanceSheets.Students.StudentCode;
            ViewBag.FirstName = data.AttendanceSheets.Students.FirstName;
            ViewBag.FatherName = data.AttendanceSheets.Students.FatherName;
            ViewBag.GrandFatherName = data.AttendanceSheets.Students.GrandFatherName;
            ViewBag.Batch = data.AttendanceSheets.Students.Batch;
            ViewBag.Date = data.Date;
            return View();
        }

        [HttpPost]
        public ActionResult EditAttendanceData(AttendanceData ad)
        {
            int id = (int)TempData["AttendanceDataID"];
            var existing = _context.AttendanceDatas.Where(d => d.AttendanceDataID == id).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(ad.Data);
            System.Diagnostics.Debug.WriteLine(ad.AttendanceDataID);
            System.Diagnostics.Debug.WriteLine(ad.AttendanceSheetId);
            existing.Data = ad.Data;
            _context.SaveChanges();
            return RedirectToAction("EditAttendance", new { id = existing.AttendanceSheetId });
        }

        public ActionResult ViewStatus(int id)
        {
            var sheet = _context.AttendanceDatas.Where(a => a.AttendanceSheetId == id).FirstOrDefault();
            var student = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId==sheet.AttendanceSheets.CourseId).ToList();
            ViewBag.Absent = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data=="Absent").ToList().Count();
            ViewBag.Present = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data == "Present").ToList().Count();
            ViewBag.Permission = _context.AttendanceDatas.Where(a => a.AttendanceSheets.StudentId == sheet.AttendanceSheets.StudentId && a.AttendanceSheets.CourseId == sheet.AttendanceSheets.CourseId && a.Data == "Permission").ToList().Count();
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
            ViewBag.Percentage = percentage.ToString();
            return View(student);
        }
    }
}