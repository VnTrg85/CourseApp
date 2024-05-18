using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoursWeb.Models;
using System.Web.ModelBinding;

namespace CoursWeb
{
    public partial class CourseDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public IQueryable<Course> GetCourse([QueryString("courseID")] int? courseId)
        {
            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Course> query = _db.Courses;
            if (courseId.HasValue && courseId > 0)
            {
                query = query.Where(p => p.CourseID == courseId);
            }
            else
            {
                query = null;
            }
            return query;
        }
        public IQueryable<Lesson> GetLessons([QueryString("courseID")] int? courseId)
        {
            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Lesson> query = _db.Lessons;      
            query = query.Where(p => p.CourseID == courseId);         
            return query;
        }
        public void AddToCart_Click(object sender,EventArgs e)
        {
            if(Session["Username"] != null)
            {
                int courseId = Convert.ToInt32(Request.QueryString["courseId"]);
                var enrol = new Enrolment
                {
                    Enrolment_date = DateTime.Now,
                    Completed_date = DateTime.Now,
                    AccountID = Convert.ToInt32(Session["UserID"].ToString()),
                    CourseID = courseId
                };
                var _db = new CoursWeb.Models.DataContext();
                _db.Enrolments.Add(enrol);
                _db.SaveChanges();
                Session["Info"] = false;
                Session["Cart"] = true;
                Response.Redirect("/userinfo");
            }else
            {
                Response.Redirect("/login");
            }
            
        }
        
    }
}