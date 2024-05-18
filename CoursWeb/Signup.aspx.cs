using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoursWeb.Models;
namespace CoursWeb
{
    public partial class Signup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Signup_Click(object sender,EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            string email = Email.Text;

            var _db = new CoursWeb.Models.DataContext();
            IQueryable<Account> query = _db.Accounts;
            query = query.Where(p => p.Username == username || p.Email == email);
            if(query.Count() > 0)
            {
                Error.Text = "The account has already exist. Change username and email";
            }else
            {
                Account AccItem = new Account
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    isAdmin = false
                };
                _db.Accounts.Add(AccItem);
                _db.SaveChanges();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account has been created successfully')", true);
                Response.Redirect("/login");
            }
        }
    }
}