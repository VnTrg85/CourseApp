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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string categoryID = Request.QueryString["CategoryID"];
                if (!string.IsNullOrEmpty(categoryID))
                {
                    int categoryIDValue;
                    if (int.TryParse(categoryID, out categoryIDValue))
                    {
                        using (var context = new DataContext())
                        {
                            var courses = context.Courses
                                .Where(c => c.CategoryID == categoryIDValue)
                                .ToList();
                            courseList.DataSource = courses;
                            courseList.DataBind();
                        }
                    }
                }
            }
        }
    }
}