using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoursWeb.Models;
namespace CoursWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Account> query = _db.Accounts;
            query = query.Where(p => p.Username == username && p.Password == password);
            if(query.Count<Account>() > 0)
            {
                Session["Username"] = query.First().Username;

                Session["isAdmin"] = query.First().isAdmin;
                Response.Redirect("/");
                statusMessage.Text = "";
            }
            else
            {
                statusMessage.Text = "Login failed. Please check username and password again";
            }
        }
        
    }
}