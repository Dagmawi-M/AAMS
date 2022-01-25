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
		public int AttendanceSheetId { get; set; }

		[Required(ErrorMessage = "Please enter student ID")]
		public int StudentId { get; set; }

		[Required(ErrorMessage = "Please enter course ID")]
		public int CourseId { get; set; }

		[Required(ErrorMessage = "Please enter Section")]
		[RegularExpression("^[A-Za-z]$")]
		public string Section { get; set; }

		public virtual StudentData Students { get; set; }
		public virtual Course Courses { get; set; }
	}
}