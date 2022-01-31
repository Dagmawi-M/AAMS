using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class AttendanceData
    {
		[Key]
		public int AttendanceDataID { get; set; }

		[Required(ErrorMessage = "Please Enter Attendance Sheet ID")]
		public int AttendanceSheetId { get; set; }

		[Required]
		public DateTime Date { get; set; }

		//[Required(ErrorMessage = "Please enter Data for Student")]
		public string Data { get; set; }

		public virtual AttendanceSheet AttendanceSheets { get; set; }
	}
}