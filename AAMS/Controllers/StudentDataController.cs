 using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class StudentDataController : Controller
    {
        // GET: StudentData
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("ViewStudents");
        }

        public ActionResult ViewStudents()
        {
            return View(_context.StudentDatas.ToList());
        }

        public ActionResult DetailsStudent(int id)
        {
            return View(_context.StudentDatas.Where(t => t.StudentId == id).FirstOrDefault());
        }

        public ActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(StudentData student)
        {
            _context.StudentDatas.Add(student);
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }

        public ActionResult EditStudent(int id)
        {
            return View(_context.StudentDatas.Where(s => s.StudentId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditStudent(StudentData _student)
        {
            var student = _context.StudentDatas.Where(s => s.StudentId == _student.StudentId).FirstOrDefault();
            student.FirstName = _student.FirstName;
            student.FatherName = _student.FatherName;
            student.GrandFatherName = _student.GrandFatherName;
            student.Batch = _student.Batch;
            student.StudentCode = _student.StudentCode;
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }

        public ActionResult DeleteStudent(int id)
        {
            var student = _context.StudentDatas.Where(s => s.StudentId == id).FirstOrDefault();
            _context.StudentDatas.Remove(student);
            _context.SaveChanges();
            return RedirectToAction("ViewStudents");
        }
    }
}