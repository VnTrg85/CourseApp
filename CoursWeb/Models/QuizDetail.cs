using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace CoursWeb.Models
{
    public class QuizDetail
    {

        [ScaffoldColumn(false)]
        public int QuizDetailID { get; set; }

        public int? QuizID { get; set; }

        public virtual Quiz Quiz { get; set; }

        [Required, StringLength(100), Display(Name = "Question")]
        public string Question { get; set; }

        [Required, StringLength(100), Display(Name = "Answer")]
        public string Answer { get; set; }
       
    }
}