using System;
using System.ComponentModel.DataAnnotations;
using Application;

namespace AAMS.Models
{
	public class Student: User
	{

		[Required(ErrorMessage = "Please enter student ID")]
		[RegularExpression("^[A-Za-z]{2}\\d{4}$", ErrorMessage = "Invalid Student ID Format")]
		public string StudentCode { get; set; }

		[Required(ErrorMessage = "Please enter Batch ID")]
        [RegularExpression("^DRB\\d{4}$", ErrorMessage = "Invalid Batch ID Format")]
		public string Batch { get; set; }

		public Student()
		{
			Role = Constants.UserTypes.STUDENT;
		}
	}
}

