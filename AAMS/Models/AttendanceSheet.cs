using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class AttendanceSheet
    {
        [Key]
        [ScaffoldColumn(false)]
        public int AttendanceSheetId { get; set; }

        [Required(ErrorMessage = "Please Enter Student ID")]
        public int StdId { get; set; }

        [Required(ErrorMessage = "Please Enter Course ID")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Enter Section")]
        [RegularExpression("^[A-Za-z]$")]
        public string Section { get; set; }

        public int AssignedLecturerId { get; set; }

        public virtual StudentData Students { get; set; }

        public virtual Course Courses { get; set; }

    }
}