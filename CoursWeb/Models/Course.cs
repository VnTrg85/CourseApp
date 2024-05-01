using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CoursWeb.Models
{
    public class Course
    {
        [ScaffoldColumn(false)]
        public int CourseID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string CourseName { get; set; }

        [Required, StringLength(10000), Display(Name = "Course Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Image { get; set; }

        [Display(Name = "Price")]
        public double? Price { get; set; }

        public int? CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}