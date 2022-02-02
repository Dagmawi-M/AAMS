using AAMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace AAMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["idUser"] != null)
            {
                TempData["UserId"] = Session["idUser"];
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register
        public ActionResult Register()
        {

            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var isRegisteredStudent = _db.StudentDatas.Where( s => s.FirstName.Equals( _user.FirstName)  && s.FatherName.Equals(_user.FatherName) && s.GrandFatherName.Equals(_user.GrandFatherName));
                var IsRolesStudent = _user.Role.Equals("Student") ;
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email);

 
                    if (isRegisteredStudent.Count() == 0 && IsRolesStudent)
                    {
                    ViewBag.stdError = "Student has not been registered";
                    return View();
                }
                else if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";

                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                string FirstName=""; string FatherName=""; string GrandFatherName="";


                //  var data = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password))?.ToList();

                var loggedInUser = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password))?.ToList().FirstOrDefault();

                if (loggedInUser != null)
                {
                    FirstName = loggedInUser.FirstName;
                    FatherName = loggedInUser.FatherName;
                    GrandFatherName = loggedInUser.GrandFatherName;
                
                    var loggedInStudent = _db.StudentDatas.Where(s => s.FirstName.Equals(FirstName) && s.FatherName.Equals(FatherName) && s.GrandFatherName.Equals(GrandFatherName)).FirstOrDefault();
                
                    var Student = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Role.Equals("Student"));
                    var Lecturer = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Role.Equals("Lecturer"));
                    var Registrar = _db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.Role.Equals("Registrar"));

                    //add session
                    Session["FirstName"] = FirstName; ;
                    Session["FatherName"] = FatherName;
                    Session["GrandFatherName"] = GrandFatherName; 
                    Session["FullName"] = loggedInUser.FullName;
                    Session["Email"] = loggedInUser.Email;
                    Session["idUser"] = loggedInUser.ID;

                    //Security Logic is non-existent here , will fix it later
                    if (Student.Count() > 0) {
                        Session["StudentId"] = loggedInStudent.StudentId;
                        return RedirectToAction("Index", "Student");
                    }
                       
                    else if (Lecturer.Count() > 0)
                        return RedirectToAction("Index", "Lecturer");
                    else if (Registrar.Count() > 0)
                        return RedirectToAction("Index", "Registrar");
                }
                else
                {
                    ViewBag.errorL = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

   
    }
}