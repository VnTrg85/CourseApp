using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursWeb
{
    public static class DbClass
    {
        // thu phuong an voi localdb
        public static DataTable tbGioHang = new DataTable();
        public static SqlConnection OpenConn()
        {
            SqlConnection myCon = new SqlConnection(ConfigurationManager.ConnectionStrings["CoursWeb"].ConnectionString);
            myCon.Open();
            return myCon;
        }
    }
}