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
namespace CoursWeb
{
    public partial class UserInfo : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            getEnrolment();
        }
        public void getEnrolment()
        {

            try
            {
                myCon = DbClass.OpenConn();
                using (SqlCommand cmd = new SqlCommand("dbo.getCartByUserID", myCon))
                {
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Session["UserID"].ToString();
                    cmd.Connection = myCon;
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    
                    SqlDataReader myDr = cmd.ExecuteReader();

                    gvEnrols.DataSource = myDr;
                    gvEnrols.DataBind();
                    myDr.Close();
                }
            }
            catch (Exception ex) { string err = ex.Message; }
            finally { myCon.Close(); }
        }
        protected void Logout_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Session["UserID"] = null;
            Session["Cart"] = null;
            Session["Info"] = null;
            Response.Redirect("/");
        }
        protected void Cart_Click(object sender, EventArgs e)
        {
            Session["Cart"] = true;
            Session["Info"] = null;
        }
        protected void Info_Click(object sender, EventArgs e)
        {
            Session["Info"] = true;
            Session["Cart"] = null;
        }
        protected void Del_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            int rowSelected = Convert.ToInt32(btn.CommandArgument);
            int courseId = Convert.ToInt32(gvEnrols.Rows[rowSelected].Cells[0].Text);
            try
            {
                myCon = DbClass.OpenConn();
                using (SqlCommand cmd = new SqlCommand("dbo.deleteCart", myCon))
                {
                    cmd.Parameters.Add("@CourseID", SqlDbType.Int).Value = courseId;
                    cmd.Parameters.Add("@AccountID", SqlDbType.Int).Value = Session["UserID"].ToString();
                    cmd.Connection = myCon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteScalar();
                    Response.Redirect("/userinfo");
                }
            }
            catch (Exception ex) { string err = ex.Message; }
            finally { myCon.Close(); }
        }
        protected void Change_Password_Click(object sender,EventArgs e)
        {
            string oldPass = OldPass.Text;
            string newPass = NewPass.Text;
            string confPass = ConfPass.Text;
            int userID = Convert.ToInt32(Session["UserID"].ToString());
            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Account> query = _db.Accounts;
            query = query.Where(p => p.AccountID == userID && p.Password == oldPass);
            if(query.Count() == 0)
            {
                errMsg.Text = "Wrong password.Please try again";
            }else
            {
                if(newPass == confPass)
                {
                    try
                    {
                        myCon = DbClass.OpenConn();
                        using (SqlCommand cmd = new SqlCommand("dbo.changePass", myCon))
                        {
                            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                            cmd.Parameters.Add("@NewPassword", SqlDbType.Int).Value = newPass;
                            cmd.Connection = myCon;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteScalar();
                            OldPass.Text = "";
                            NewPass.Text = "";
                            ConfPass.Text = "";
                            errMsg.Text = "Change password successfully";
                            Thread.Sleep(3000);
                            errMsg.Text = "";
                            
                        }
                    }
                    catch (Exception ex) { string err = ex.Message; }
                    finally { myCon.Close(); }
                }
                else
                {
                    errMsg.Text = "The new password and confirm password is not same.";
                }
            }
        }
    }
}