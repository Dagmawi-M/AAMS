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
        public ActionResult AddStudents()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStudents(StudentData stu)
        {
            _context.StudentDatas.Add(stu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}