using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Web.UI.WebControls;
using CoursWeb.Models;
using System.Web.UI.WebControls.WebParts;

namespace CoursWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEnrolData();
                BindCourseDropdown();
            }
        }

        private void BindEnrolData()
        {
            using (var _db = new DataContext())
            {
                var enrolData = from enrol in _db.Enrolments
                                join account in _db.Accounts on enrol.AccountID equals account.AccountID
                                select new
                                {
                                    EnrolmentID = enrol.EnrolmentID,
                                    Enrolment_date = enrol.Enrolment_date,
                                    Completed_date = enrol.Completed_date,
                                    Username = account.Username,
                                };

                GridViewEnrol.DataSource = enrolData.ToList();
                GridViewEnrol.DataBind();
            }
        }

        private void BindCourseDropdown()
        {
            using (var _db = new DataContext())
            {
                var courses = _db.Courses.Select(course => new { course.CourseID, course.CourseName }).ToList();
                DropDownListCourseID.Items.Add(new ListItem("All courses", "0"));
                foreach (var course in courses)
                {
                    DropDownListCourseID.Items.Add(new ListItem(course.CourseName, course.CourseID.ToString()));
                }

            }
        }
        public void BindEnrolDataSecond()
        {

        }
        protected void Courses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedCourseID = Convert.ToInt32(DropDownListCourseID.SelectedValue);

            if (selectedCourseID != 0)
            {
                using (var _db = new DataContext())
                {
                    var enrolData = from enrol in _db.Enrolments
                                    where enrol.CourseID == selectedCourseID
                                    join account in _db.Accounts on enrol.AccountID equals account.AccountID
                                    select new
                                    {
                                        EnrolmentID = enrol.EnrolmentID,
                                        Enrolment_date = enrol.Enrolment_date,
                                        Completed_date = enrol.Completed_date,
                                        Username = account.Username,
                                    };

                    GridViewEnrol.DataSource = enrolData.ToList();
                    GridViewEnrol.DataBind();
                }
            }
            else
            {
                BindEnrolData();
            }

        }

        protected void GridViewEnrol_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int enrolmentID = Convert.ToInt32(GridViewEnrol.DataKeys[e.RowIndex].Value);

            using (var _db = new DataContext())
            {
                var enrol = _db.Enrolments.Find(enrolmentID);
                _db.Enrolments.Remove(enrol);
                _db.SaveChanges();
            }

            BindEnrolData();
            DropDownListCourseID.SelectedIndex = 0;
        }



        protected void LinkButtonToggleEnrolManagement_Click(object sender, EventArgs e)
        {
            Session["quanlydangkykh"] = true;
        }
    }
}