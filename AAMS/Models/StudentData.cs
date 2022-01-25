using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class StudentData
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please enter Student ID")]
        [RegularExpression("^[A-Za-z]{2}\\d{4}$", ErrorMessage = "Invalid Student ID Format")]
        public string StudentCode { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter Father Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please enter Grandfather Name")]
        public string GrandFatherName { get; set; }

        [Required(ErrorMessage = "Please enter Batch ID")]
        [RegularExpression("^DRB\\d{4}$", ErrorMessage = "Invalid Batch ID Format")]
        public string Batch { get; set; }
    }
}