using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CoursWeb.Models;


namespace CoursWeb
{
    public partial class SiteMaster : MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Check_Login();
            SubMenu();


        }
        private void SubMenu()
        {
            List<Category> categories = GetData();

            if (categories != null && categories.Count > 0)
            {
                foreach (var category in categories)
                {
                    var subMenuItem = new HtmlGenericControl("li");
                    var link = new HyperLink();
                    link.Text = category.CategoryName;
                    link.NavigateUrl = $"ManagerCatagories.aspx?CategoryID={category.CategoryID}";
                    subMenuItem.Controls.Add(link);
                    subMenu.Controls.Add(subMenuItem);
                }
            }
        }
        private List<Category> GetData()
        {

            using (var context = new DataContext())
            {
                return context.Categories.ToList();
            }
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