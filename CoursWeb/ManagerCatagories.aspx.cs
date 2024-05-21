using CoursWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoursWeb
{
    public partial class ManagerCatagories : System.Web.UI.Page
    {
        protected string categoryName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["CategoryID"]))
                {
                    int categoryIDValue;
                    if (int.TryParse(Request.QueryString["CategoryID"], out categoryIDValue))
                    {
                        using (var context = new DataContext())
                        {
                            var category = context.Categories.FirstOrDefault(c => c.CategoryID == categoryIDValue);
                            if (category != null)
                            {
                                categoryName=category.CategoryName;
                                var courses = context.Courses.Where(c => c.CategoryID == categoryIDValue).ToList();
                                courseList.DataSource = courses;
                                courseList.DataBind();
                            }
                        }
                    }
                }
            }
        }
    }
}