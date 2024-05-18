using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoursWeb.Models;


namespace CoursWeb
{
    public partial class SiteMaster : MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Check_Login();            
            
        }
        protected void Home_Click(object sender,EventArgs e)
        {
            Session["queryString"] = "";
            Response.Redirect("/");
        }
        private void Check_Login()
        {
            if(Session["Username"] != null)
            {
                login.Text = Session["Username"].ToString();
                logout.Text = "Quan ly";
               
                if (Convert.ToBoolean(Session["isAdmin"]))
                {
                    logout.Enabled = true;
                    
                }else
                {
                    logout.Enabled = false;
                }
            }
            else
            {
                login.Text = "Login";
                logout.Text = "Signup";
            }
        }
        protected void Login_Click(object sender,EventArgs e)
        {
            if (Session["Username"] == null)
                Response.Redirect("/login");
            else
            {
                Session["Info"] = null;
                Session["cart"] = true;
                Response.Redirect("/userinfo");
            }
              
        }
        
        protected void Signup_Quanly_Click(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
                Response.Redirect("/signup");
            else
            {         
                Response.Redirect("/quanly");              
            }
        }
        protected void Search_Click(object sender,EventArgs e)
        {
            string queyString = searchBox.Text;
            Session["queryString"] = queyString;
            Response.Redirect("/");
        }

       
    }
}