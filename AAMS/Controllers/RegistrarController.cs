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

        

    }
}