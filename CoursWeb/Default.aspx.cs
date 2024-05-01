﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoursWeb.Models;
using System.Web.ModelBinding;

namespace CoursWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        
        
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }
        public IQueryable<Course> GetCourses()
        {
            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Course> query = _db.Courses;
            return query;
        }
    }
}