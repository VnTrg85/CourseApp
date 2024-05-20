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
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection myCon = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQuizDetailData();
                BindQuizDropdown();
            }
        }

        private void BindQuizDetailData()
        {
            using (var _db = new DataContext())
            {
                var quizDetailData = from quizdetail in _db.QuizDetails
                                     select quizdetail;
                GridViewQuizDetail.DataSource = quizDetailData.ToList();
                GridViewQuizDetail.DataBind();
            }
        }

        private void BindQuizDropdown()
        {
            using (var _db = new DataContext())
            {
                var quizs = _db.Quizs.Select(q => new { q.QuizID }).ToList();
                DropDownListQuizID.DataSource = quizs;
                DropDownListQuizID.DataTextField = "QuizID";
                DropDownListQuizID.DataValueField = "QuizID";
                DropDownListQuizID.DataBind();
                DropDownListQuizID.Items.Insert(0, new ListItem("Select Quiz", "0"));
            }
        }

        protected void ButtonAddNew_Click(object sender, EventArgs e)
        {
            PanelAddEdit.Visible = true;
            LabelFormTitle.Text = "Add Quiz Detail";
            ClearForm();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListQuizID.SelectedValue == "0" || string.IsNullOrEmpty(TextBoxQuestion.Text) || string.IsNullOrEmpty(TextBoxAnswer.Text))
                {
                    ErrorLabel.Text = "Please select Quiz and enter required information";
                    return;
                }

                int QuizID = Convert.ToInt32(DropDownListQuizID.SelectedValue);
                string question = TextBoxQuestion.Text;
                string answer = TextBoxAnswer.Text;

                try
                {
                    myCon = DbClass.OpenConn();
                    using (SqlCommand cmd = new SqlCommand("dbo.createQuizDetail", myCon))
                    {
                        cmd.Parameters.Add("@quizID", SqlDbType.Int).Value = QuizID;
                        cmd.Parameters.Add("@question", SqlDbType.NVarChar).Value = question;
                        cmd.Parameters.Add("@answer", SqlDbType.NVarChar).Value = answer;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex) { string err = ex.Message; }
                finally { myCon.Close(); }

                BindQuizDetailData();
                ErrorLabel.Text = "";
                PanelAddEdit.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred while saving the quiz detail: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ErrorLabel.Text += " Inner exception: " + ex.InnerException.Message;
                }
            }
        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            PanelAddEdit.Visible = false;
        }

        protected void GridViewQuizDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewQuizDetail.EditIndex = e.NewEditIndex;
            BindQuizDetailData();
        }

        protected void GridViewQuizDetail_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int quizDetailID = Convert.ToInt32(GridViewQuizDetail.DataKeys[e.RowIndex].Value);
                GridViewRow row = GridViewQuizDetail.Rows[e.RowIndex];
                TextBox questionTextBox = row.FindControl("TextBoxEditQuestion") as TextBox;
                TextBox answerTextBox = row.FindControl("TextBoxEditAnswer") as TextBox;

                if (questionTextBox != null && answerTextBox != null)
                {
                    string question = questionTextBox.Text;
                    string answer = answerTextBox.Text;
                    using (var _db = new DataContext())
                    {
                        var quiz = _db.QuizDetails.Find(quizDetailID);
                        if (quiz != null)
                        {
                            quiz.Question = question;
                            quiz.Answer = answer;

                            _db.SaveChanges();
                        }
                        else
                        {
                            ErrorLabel.Text = "Quiz Detail not found.";
                        }
                    }

                    GridViewQuizDetail.EditIndex = -1;
                    BindQuizDetailData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewQuizDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewQuizDetail.EditIndex = -1;
            BindQuizDetailData();
        }

        protected void GridViewQuizDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int quizDetailID = Convert.ToInt32(GridViewQuizDetail.DataKeys[e.RowIndex].Value);

            using (var _db = new DataContext())
            {
                var quizDetail = _db.QuizDetails.Find(quizDetailID);
                _db.QuizDetails.Remove(quizDetail);
                _db.SaveChanges();
            }

            BindQuizDetailData();
        }

        private void ClearForm()
        {

            DropDownListQuizID.SelectedIndex = 0;
            TextBoxQuestion.Text = "";
            TextBoxAnswer.Text = "";

        }

        protected void LinkButtonToggleQuizDetailManagement_Click(object sender, EventArgs e)
        {
            Session["quanlyquizdetail"] = true;
        }
    }
}