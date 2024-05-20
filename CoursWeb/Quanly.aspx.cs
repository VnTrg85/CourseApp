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
                BindQuizData();
                BindLessonDropdown();
            }
        }

        private void BindQuizData()
        {
            using (var _db = new DataContext())
            {
                var quizData = from quiz in _db.Quizs
                               join lesson in _db.Lessons on quiz.LessonID equals lesson.LessonID
                               select new
                               {
                                   QuizID = quiz.QuizID,
                                   LessonName = lesson.LessonName,
                                   Questions_number = quiz.Questions_number
                               };

                GridViewQuiz.DataSource = quizData.ToList();
                GridViewQuiz.DataBind();
            }
        }

        private void BindLessonDropdown()
        {
            using (var _db = new DataContext())
            {
                var lessons = _db.Lessons.Select(l => new { l.LessonID, l.LessonName }).ToList();
                DropDownListLessonID.DataSource = lessons;
                DropDownListLessonID.DataTextField = "LessonName";
                DropDownListLessonID.DataValueField = "LessonID";
                DropDownListLessonID.DataBind();
                DropDownListLessonID.Items.Insert(0, new ListItem("Select Lesson", "0"));
            }
        }

        protected void ButtonAddNew_Click(object sender, EventArgs e)
        {
            PanelAddEdit.Visible = true;
            LabelFormTitle.Text = "Add Quiz";
            ClearForm();
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListLessonID.SelectedValue == "0" || string.IsNullOrEmpty(TextBoxQuestionsNumber.Text))
                {
                    ErrorLabel.Text = "Please select a Lesson and enter Questions Number.";
                    return;
                }

                int lessonID = Convert.ToInt32(DropDownListLessonID.SelectedValue);
                int questionsNumber = Convert.ToInt32(TextBoxQuestionsNumber.Text);

                try
                {
                    myCon = DbClass.OpenConn();
                    using (SqlCommand cmd = new SqlCommand("dbo.createQuiz", myCon))
                    {
                        cmd.Parameters.Add("@lessonID", SqlDbType.Int).Value = lessonID;
                        cmd.Parameters.Add("@number", SqlDbType.Int).Value = questionsNumber;
                        cmd.Connection = myCon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex) { string err = ex.Message; }
                finally { myCon.Close(); }

                BindQuizData();
                PanelAddEdit.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred while saving the quiz: " + ex.Message;
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

        protected void GridViewQuiz_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewQuiz.EditIndex = e.NewEditIndex;
            BindQuizData();
        }

        protected void GridViewQuiz_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int quizID = Convert.ToInt32(GridViewQuiz.DataKeys[e.RowIndex].Value);
                GridViewRow row = GridViewQuiz.Rows[e.RowIndex];
                TextBox questionsNumberTextBox = row.FindControl("TextBoxEditQuestionsNumber") as TextBox;
                if (questionsNumberTextBox != null)
                {
                    int questionsNumber = Convert.ToInt32(questionsNumberTextBox.Text);
                    using (var _db = new DataContext())
                    {
                        var quiz = _db.Quizs.Find(quizID);
                        if (quiz != null)
                        {
                            quiz.Questions_number = questionsNumber;
                            quiz.Questions_number = questionsNumber;

                            _db.SaveChanges();
                        }
                        else
                        {
                            ErrorLabel.Text = "Quiz not found.";
                        }
                    }

                    GridViewQuiz.EditIndex = -1;
                    BindQuizData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewQuiz_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewQuiz.EditIndex = -1;
            BindQuizData();
        }

        protected void GridViewQuiz_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int quizID = Convert.ToInt32(GridViewQuiz.DataKeys[e.RowIndex].Value);

            using (var _db = new DataContext())
            {
                var quiz = _db.Quizs.Find(quizID);
                _db.Quizs.Remove(quiz);
                _db.SaveChanges();
            }

            BindQuizData();
        }

        private void ClearForm()
        {
            TextBoxQuizID.Text = "";
            DropDownListLessonID.SelectedIndex = 0;
            TextBoxQuestionsNumber.Text = "";
        }

        protected void LinkButtonToggleQuizManagement_Click(object sender, EventArgs e)
        {
            Session["quanlyquiz"] = true;
        }
    }
}