 using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

        public ActionResult ViewStudents(string batch = "")
        {
            var batches = _context.StudentDatas.GroupBy(a => a.Batch).Select(a => a.FirstOrDefault()).ToList();
            ViewBag.Batches = batches;
            if(batch=="")
                return View(_context.StudentDatas.OrderBy(a => a.FirstName).ToList());
            else
                return View(_context.StudentDatas.Where(a=>a.Batch==batch).OrderBy(a => a.FirstName).ToList());

        }
        [HttpPost]
        public ActionResult ViewStudents()
        {
            string filter = Request.Form["filter"].ToString();
            return RedirectToAction("ViewStudents", new { batch = filter });
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

        public ActionResult UploadStudentData()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadStudentData(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { 
                                new DataColumn("StudentCode", typeof(string)),
                                new DataColumn("FirstName", typeof(string)),
                                new DataColumn("FatherName", typeof(string)),
                                new DataColumn("GrandFatherName", typeof(string)),
                                new DataColumn("Batch",typeof(string)) });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                string conString = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.StudentDatas";

                        //[OPTIONAL]: Map the DataTable columns with that of the database table
                        //sqlBulkCopy.ColumnMappings.Add("StudentId", "StudentId");
                        sqlBulkCopy.ColumnMappings.Add("StudentCode", "StudentCode");
                        sqlBulkCopy.ColumnMappings.Add("FirstName", "FirstName");
                        sqlBulkCopy.ColumnMappings.Add("FatherName", "FatherName");
                        sqlBulkCopy.ColumnMappings.Add("GrandFatherName", "GrandFatherName");
                        sqlBulkCopy.ColumnMappings.Add("Batch", "Batch");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }
            return RedirectToAction("ViewStudents");
        }


    }
}