using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace CoursWeb.Models
{
    public class Lesson
    {
        [ScaffoldColumn(false)]
        public int LessonID { get; set; }

        [Required, StringLength(100), Display(Name = "Name")]
        public string LessonName { get; set; }

        [Required, StringLength(10000), Display(Name = "Lesson Video URL"), DataType(DataType.MultilineText)]
        public string Lesson_URL { get; set; }

        [Required, StringLength(10000), Display(Name = "Lesson Description"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int? CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
}