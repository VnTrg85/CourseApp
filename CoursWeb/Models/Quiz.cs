using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CoursWeb.Models
{
    public class Quiz
    {
        [ScaffoldColumn(false)]
        public int QuizID { get; set; }

        public int? LessonID { get; set; }

        public virtual Lesson Lesson { get; set; }
        public int Questions_number { get; set; }
    }
}