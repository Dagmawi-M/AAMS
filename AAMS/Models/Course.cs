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
		public int CourseId { get; set; }

		[Required(ErrorMessage = "Please enter Course Code")]
		public string CourseCode { get; set; }

		[Required(ErrorMessage = "Please enter course name")]
		public string CourseName { get; set; }

		[Required(ErrorMessage = "Please enter semester")]
		public string Semester { get; set; }

		[Required(ErrorMessage = "Please enter year")]
		public string Year { get; set; }

		public int AssignedLecturerId { get; set; }
	}
}