using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AAMS.Models
{
    public class AttendanceData
    {
        [Key]
        public int AttendanceDataId { get; set; }

        [Required(ErrorMessage = "Please Enter Attendance Sheet ID")]
        public string AttendanceSheetId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please Enter Data for Student")]
        public string Data { get; set; }

        public virtual AttendanceSheet AttendanceSheets { get; set; }
    }
}