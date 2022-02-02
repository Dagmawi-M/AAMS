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
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Registrar
        public ActionResult Index()
        {
           // var uniqueSheets = _context.AttendanceSheets.Select(a => new { a.CourseId, a.Courses, a.Section }).Distinct().ToList();
            //ViewBag.unique = uniqueSheets;
           // return View(_context.AttendanceSheets.GroupBy(a => a.CourseId).Select(a => a.FirstOrDefault()).ToList());
            var course = _context.Courses.ToList();
            return View(course);
        }
    }
}