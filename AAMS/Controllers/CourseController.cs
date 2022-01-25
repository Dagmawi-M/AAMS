﻿using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAMS.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            return RedirectToAction("ViewCourses");
        }
        public ActionResult ViewCourses()
        {
            return View(_context.Courses.ToList());
        }

        public ActionResult DetailsCourse(int id)
        {
            return View(_context.Courses.Where(c => c.CourseId == id).FirstOrDefault());
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        public ActionResult EditCourse(int id)
        {
            return View(_context.Courses.Where(c => c.CourseId == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditCourse(Course _course)
        {
            var course = _context.Courses.Where(c => c.CourseId == _course.CourseId).FirstOrDefault();
            course.CourseCode = _course.CourseCode;
            course.CourseName = _course.CourseName;
            course.Semester = _course.Semester;
            course.Year = _course.Year;
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }

        public ActionResult DeleteCourse(int id)
        {
            var course = _context.Courses.Where(c => c.CourseId == id).FirstOrDefault();
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("ViewCourses");
        }
    }
}