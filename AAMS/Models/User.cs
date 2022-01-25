using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AAMS.Models
{
	public class User
	{
		[Key, Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }
		[Required(ErrorMessage = "Please enter your first name")]
		[StringLength(50, MinimumLength = 3)]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please enter your father's name")]
		[StringLength(50, MinimumLength = 3)]
		public string FatherName { get; set; }
		[Required(ErrorMessage = "Please enter your grandfather's name")]
		[StringLength(50, MinimumLength = 3)]
		public string GrandFatherName { get; set; }
		[Required]
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
		public string Email { get; set; }
		[Required]
		//[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
		public string Password { get; set; }
		[NotMapped]
		[Required]
		[Compare("Password")]
		public string ConfirmPassword { get; set; }

		public string FullName
		{
			get
			{
				return $"{this.FirstName} {this.FatherName} {this.GrandFatherName}";
			}
		}

        public string Role { get; set; }
	}
}

