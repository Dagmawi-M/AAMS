using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class Course
    {
        [Key]
        [ScaffoldColumn(false)]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Enter Course Code")]
        //[RegularExpression("^[CS]{2}//d{3}$", ErrorMessage = "Invalid Course Code Format")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Please enter Course Name")]
        public string CourseName { get; set; }

        //[StringRange(AllowableValues = new[] {"Autumn", "Winter", "Spring"})]
        [Required(ErrorMessage = "Please Enter Semester")]
        public string Semester { get; set; }

        [Required(ErrorMessage = "Please Enter Year")]
       // [RegularExpression("^[0-9]{4}$", ErrorMessage = "Please Enter Appropriate Format")]
        public string Year { get; set; }
    }
}