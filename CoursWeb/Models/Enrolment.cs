using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoursWeb.Models
{
    public class Enrolment
    {
        [ScaffoldColumn(false)]
        public int EnrolmentID { get; set; }
        [Required]
        public DateTime Enrolment_date { get; set; }
        public DateTime Completed_date { get; set; }
        public int? AccountID { get; set; }
        public virtual Account Account { get; set; }
        public int? CourseID { get; set; }
        public virtual Course Course { get; set; }
    }
}