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

                //Courses
                BindCoursesData();
                BindCategoryDropdown();
                //Account
                BindAccountsData();
                //Quiz
                BindQuizData();
                BindLessonDropdown();
                //QuizDetail
                BindQuizDetailData();
                BindQuizDropdown();
                //Enrol
                BindEnrolData();
                BindCourseDropdown();
                //Cate and lesson               
                BindGridView();
                BindCoursesDropdown();
                ButtonAddNewLesson.Visible = false;
                GridViewLessons.Visible = false;
                LoadLessonsForSelectedCourse();
            }
        }
        //CATEGORY MANAGEMENT
        private void BindGridView()
        {
            ErrorLabelCate.Text = "";
            using (var context = new DataContext())
            {
                var categories = context.Categories.ToList();
                GridView1.DataSource = categories;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelCate.Text = "";
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ErrorLabelCate.Text = "";
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ErrorLabelCate.Text = "";
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int categoryId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["CategoryID"]);
            TextBox txtCategoryName = (TextBox)row.FindControl("txtCategoryName");
            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");

            using (var context = new DataContext())
            {
                var category = context.Categories.FirstOrDefault(c => c.CategoryID == categoryId);
                if (category != null)
                {
                    category.CategoryName = txtCategoryName.Text;
                    category.Description = txtDescription.Text;
                    context.SaveChanges();
                }
            }

            GridView1.EditIndex = -1;
            BindGridView();
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelCate.Text = "";
            int categoryId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["CategoryID"]);

            using (var context = new DataContext())
            {
                var courseExists = context.Courses.Any(c => c.CategoryID == categoryId);
                if (courseExists)
                {
                    ErrorLabelCate.Text = "Danh mục hiện đang có khóa học, không thể xóa";
                }
                else
                {

                    var category = context.Categories.Find(categoryId);
                    if (category != null)
                    {
                        context.Categories.Remove(category);
                        context.SaveChanges();
                    }
                    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('Categories', RESEED, 0)");
                }
            }
            BindGridView();
        }
        protected void ButtonAddNewCate_Click(object sender, EventArgs e)
        {
            ErrorLabelCate.Text = "";
            PanelAddEditCate.Visible = true;
            ClearFormCate();
        }
        private void ClearFormCate()
        {
            TextCategoryname.Text = "";
            TextDes.Text = "";

        }
        protected void ButtonSaveCate_Click(object sender, EventArgs e)
        {
            try
            {
                String txtname = TextCategoryname.Text;
                String txtdescription = TextDes.Text;
                if(txtname == "" || txtdescription == "")
                {
                    ErrorLabelCate.Text = "Please fill all required fileds";
                }
                int lastCategoryID;
                using (var _db = new DataContext())
                {
                    lastCategoryID = _db.Categories.OrderByDescending(c => c.CategoryID).FirstOrDefault()?.CategoryID ?? 0;
                    var newCategory = new Category()
                    {
                        CategoryID = lastCategoryID + 1,
                        CategoryName = txtname,
                        Description = txtdescription,
                    };
                    _db.Categories.Add(newCategory);
                    _db.SaveChanges();
                }

                BindGridView();
                PanelAddEditCate.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabelCate.Text = "An error occurred while saving the category: " + ex.Message;
            }


        }
        protected void ButtonCancelCate_Click(object sender, EventArgs e)
        {
            PanelAddEditCate.Visible = false;
        }
        protected void LinkButtonToggleCateManagement_Click(object sender, EventArgs e)
        {
            Session["quanlydanhmuc"] = true;
            Session["quanlyquiz"] = false;
            Session["quanlytaikhoan"] = false;
            Session["quanlykhoahoc"] = false;
            Session["quanlyquizdetail"] = false;
            Session["quanlydangky"] = false;
            Session["quanlybaihoc"] = false;
        }

        //COURSE MANAGEMENT
        private void BindCoursesData()
        {
            ErrorLabelCourse.Text = "";
            using (var _db = new DataContext())
            {
                var coursesData = from course in _db.Courses
                                  select course;

                GridViewCourses.DataSource = coursesData.ToList();
                GridViewCourses.DataBind();
            }
        }

        private void BindCategoryDropdown()
        {

            using (var _db = new DataContext())
            {
                var categories = _db.Categories.Select(l => new { l.CategoryID, l.CategoryName }).ToList();
                DropDownListCateID.DataSource = categories;
                DropDownListCateID.DataTextField = "CategoryName";
                DropDownListCateID.DataValueField = "CategoryID";
                DropDownListCateID.DataBind();
                DropDownListCateID.Items.Insert(0, new ListItem("Select Category", "0"));
            }
        }

        protected void ButtonAddNewCourse_Click(object sender, EventArgs e)
        {
            ErrorLabelCourse.Text = "";
            PanelAddEditCourse.Visible = true;
            LabelFormTitleCourse.Text = "Add Courses";
            ClearFormCourse();
        }

        protected void ButtonSaveCourse_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListCateID.SelectedValue == "0" || string.IsNullOrEmpty(TextBoxName.Text) || string.IsNullOrEmpty(TextBoxDesc.Text) || string.IsNullOrEmpty(TextBoxImage.Text) || string.IsNullOrEmpty(TextBoxPrice.Text))
                {
                    ErrorLabelCourse.Text = "Please select category or enter required information.";
                    return;
                }

                int categoryID = Convert.ToInt32(DropDownListCateID.SelectedValue);
                string name = TextBoxName.Text;
                string desc = TextBoxDesc.Text;
                string image = TextBoxImage.Text;
                string price = TextBoxPrice.Text;
                try
                {
                    myCon = DbClass.OpenConn();
                    using (SqlCommand cmd = new SqlCommand("dbo.createCourse", myCon))
                    {
                        cmd.Parameters.Add("@CateID", SqlDbType.Int).Value = categoryID;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                        cmd.Parameters.Add("@Desc", SqlDbType.NVarChar).Value = desc;
                        cmd.Parameters.Add("@Image", SqlDbType.NVarChar).Value = image;
                        cmd.Parameters.Add("@Price", SqlDbType.NVarChar).Value = price;
                        cmd.Connection = myCon;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex) { string err = ex.Message; }
                finally { myCon.Close(); }

                BindCoursesData();
                PanelAddEditCourse.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabelCourse.Text = "An error occurred while saving the course: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ErrorLabelCourse.Text += " Inner exception: " + ex.InnerException.Message;
                }
            }
        }

        protected void ButtonCancelCourse_Click(object sender, EventArgs e)
        {
            ErrorLabelCourse.Text = "";
            PanelAddEditCourse.Visible = false;
        }

        protected void GridViewCourses_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelCourse.Text = "";
            GridViewCourses.EditIndex = e.NewEditIndex;
            BindCoursesData();
        }

        protected void GridViewCourses_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ErrorLabelCourse.Text = "";

            try
            {
                int courseID = Convert.ToInt32(GridViewCourses.DataKeys[e.RowIndex].Value);
                GridViewRow row = GridViewCourses.Rows[e.RowIndex];
                TextBox nameTextBox = row.FindControl("TextBoxEditName") as TextBox;
                TextBox descTextBox = row.FindControl("TextBoxEditDesc") as TextBox;
                TextBox priceTextBox = row.FindControl("TextBoxEditPrice") as TextBox;
                if (nameTextBox != null && descTextBox != null && priceTextBox != null)
                {
                    string name = nameTextBox.Text;
                    string desc = descTextBox.Text;
                    double price = Convert.ToDouble(priceTextBox.Text);

                    using (var _db = new DataContext())
                    {
                        var course = _db.Courses.Find(courseID);
                        if (course != null)
                        {
                            course.CourseName = name;
                            course.Description = desc;
                            course.Price = price;
                            _db.SaveChanges();
                        }
                        else
                        {
                            ErrorLabelCourse.Text = "Course not found.";
                        }
                    }

                    GridViewCourses.EditIndex = -1;
                    BindCoursesData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabelCourse.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewCourses_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewCourses.EditIndex = -1;
            BindCoursesData();
        }

        protected void GridViewCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelCourse.Text = "";
            int courseID = Convert.ToInt32(GridViewCourses.DataKeys[e.RowIndex].Value);
            using (var _db = new DataContext())
            {
                var enrols = _db.Enrolments.Where(p => p.CourseID == courseID);
                if (enrols.Count() > 0)
                {
                    ErrorLabelCourse.Text = "That Course can not be deleted because there are some Enrolments relevant to Course";
                    return;
                }
                var lessons = _db.Lessons.Where(p => p.CourseID == courseID);
                if (lessons.Count() > 0)
                {
                    ErrorLabelCourse.Text = "That Course can not be deleted because there are some Lessons relevant to Course";
                    return;
                }
            }

            using (var _db = new DataContext())
            {
                var course = _db.Courses.Find(courseID);
                _db.Courses.Remove(course);
                _db.SaveChanges();
            }

            BindCoursesData();
        }

        private void ClearFormCourse()
        {           
            DropDownListCateID.SelectedIndex = 0;
            TextBoxName.Text = "";
            TextBoxDesc.Text = "";
            TextBoxImage.Text = "";
            TextBoxPrice.Text = "";
        }

        protected void LinkButtonToggleCoursesManagement_Click(object sender, EventArgs e)
        {
            BindCategoryDropdown();
            Session["quanlykhoahoc"] = true;
            Session["quanlytaikhoan"] = false;
            Session["quanlyquiz"] = false;
            Session["quanlyquizdetail"] = false;
            Session["quanlydangky"] = false;
            Session["quanlydanhmuc"] = false;
            Session["quanlybaihoc"] = false;           

        }
        //LESSON MANAGEMENT

        private void BindCoursesDropdown()
        {
            ErrorLabelLesson.Text = "";

            using (var context = new DataContext())
            {
                var courses = context.Courses.ToList();
                courses.Insert(0, new Course { CourseID = 0, CourseName = "Chọn Khóa Học" });
                DropdownCourses.DataSource = courses;
                DropdownCourses.DataTextField = "CourseName";
                DropdownCourses.DataValueField = "CourseID";
                DropdownCourses.DataBind();
            }
        }

        protected void DropdownCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropdownCourses.SelectedValue != "0")
            {
                ButtonAddNewLesson.Visible = true;
                GridViewLessons.Visible = true;
                LoadLessonsForSelectedCourse();
            }
            else
            {
                ButtonAddNewLesson.Visible = false;
                GridViewLessons.Visible = false;
            }
        }

        private void LoadLessonsForSelectedCourse()
        {
            ErrorLabelLesson.Text = "";

            if (DropdownCourses.SelectedItem != null && DropdownCourses.SelectedValue != "0")
            {
                int selectedCourseID = Convert.ToInt32(DropdownCourses.SelectedValue);

                using (var context = new DataContext())
                {
                    var lessons = context.Lessons.Where(l => l.CourseID == selectedCourseID).ToList();
                    GridViewLessons.DataSource = lessons;
                    GridViewLessons.DataBind();
                }
            }
            else
            {
                GridViewLessons.DataSource = null;
                GridViewLessons.DataBind();
            }
        }
        protected void GridViewLessons_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelLesson.Text = "";
            GridViewLessons.EditIndex = e.NewEditIndex;
            LoadLessonsForSelectedCourse();
        }

        protected void GridViewLessons_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewLessons.EditIndex = -1;
            LoadLessonsForSelectedCourse();
        }

        protected void GridViewLessons_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewLessons.Rows[e.RowIndex];
            int lessonId = Convert.ToInt32(GridViewLessons.DataKeys[e.RowIndex].Values["LessonID"]);
            TextBox txtLessonName = (TextBox)row.FindControl("txtLessonName");
            TextBox txtLessonURL = (TextBox)row.FindControl("txtLessonURL");
            TextBox txtLessonDesc = (TextBox)row.FindControl("txtLessonDesc");


            using (var context = new DataContext())
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonID == lessonId);
                if (lesson != null)
                {
                    lesson.LessonName = txtLessonName.Text;
                    lesson.Lesson_URL = txtLessonURL.Text;
                    lesson.Description = txtLessonDesc.Text;
                    context.SaveChanges();
                }
            }

            GridViewLessons.EditIndex = -1;
            LoadLessonsForSelectedCourse();
        }

        protected void GridViewLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelLesson.Text = "";

            int lessonId = Convert.ToInt32(GridViewLessons.DataKeys[e.RowIndex].Values["LessonID"]);

            using (var context = new DataContext())
            {
                var quizExists = context.Quizs.Any(q => q.LessonID == lessonId);
                if (quizExists)
                {
                    ErrorLabelLesson.Text = "Còn câu hỏi trong bài học không thể xóa được";
                    return;
                }
                else
                {
                    var lesson = context.Lessons.Find(lessonId);
                    if (lesson != null)
                    {
                        context.Lessons.Remove(lesson);
                        context.SaveChanges();
                    }
                }
            }
            LoadLessonsForSelectedCourse();
        }

        protected void ButtonAddNewLesson_Click(object sender, EventArgs e)
        {
            ErrorLabelLesson.Text = "";

            if (DropdownCourses.SelectedValue != "0")
            {
                PanelAddEditLesson.Visible = true;
                ClearLessonForm();
            }
            else
            {
            }
        }

        private void ClearLessonForm()
        {
            TextLessonName.Text = "";
            TextLessonURL.Text = "";
            TextLessonDesc.Text = "";
        }

        protected void ButtonSaveLesson_Click(object sender, EventArgs e)
        {
            try
            {
                string lessonName = TextLessonName.Text;
                string lessonURL = TextLessonURL.Text;
                string lessonDesc = TextLessonDesc.Text;

                if (string.IsNullOrEmpty(lessonName) || string.IsNullOrEmpty(lessonURL))
                {
                    ErrorLabelLesson.Text = "Vui lòng điền đầy đủ thông tin bài học.";
                    return;
                }
                int lastLessonID;
                int selectedCourseID = Convert.ToInt32(DropdownCourses.SelectedValue);
                using (var context = new DataContext())
                {
                    lastLessonID = context.Lessons.OrderByDescending(c => c.LessonID).FirstOrDefault()?.LessonID ?? 0;
                    var existingLesson = context.Lessons.FirstOrDefault(l => l.LessonID == lastLessonID + 1);
                    if (existingLesson != null)
                    {
                        ErrorLabelLesson.Text = "LessonID đã tồn tại. Vui lòng chọn một LessonID khác.";
                        return;
                    }
                    var course = context.Courses.FirstOrDefault(c => c.CourseID == selectedCourseID);
                    var newLesson = new Lesson()
                    {
                        LessonID = lastLessonID + 1,
                        LessonName = lessonName,
                        Lesson_URL = lessonURL,
                        Description = lessonDesc,
                        CourseID = selectedCourseID,
                    };
                    context.Lessons.Add(newLesson);
                    context.SaveChanges();
                }

                LoadLessonsForSelectedCourse();
                PanelAddEditLesson.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabelLesson.Text = "Đã xảy ra lỗi khi lưu bài học. Vui lòng thử lại sau.";
            }
        }

        protected void ButtonCancelLesson_Click(object sender, EventArgs e)
        {
            PanelAddEditLesson.Visible = false;
        }
        protected void LinkButtonToggleLessonManagement_Click(object sender, EventArgs e)
        {
            BindCoursesDropdown();
            Session["quanlybaihoc"] = true;
            Session["quanlykhoahoc"] = false;
            Session["quanlytaikhoan"] = false;
            Session["quanlyquiz"] = false;
            Session["quanlyquizdetail"] = false;
            Session["quanlydangky"] = false;
            Session["quanlydanhmuc"] = false;

        }
        //ACCOUNT MANAGEMENT
        private void BindAccountsData()
        {
            ErrorLabelAcc.Text = "";

            using (var _db = new DataContext())
            {
                var accountData = from account in _db.Accounts
                                  select account;

                GridViewAccounts.DataSource = accountData.ToList();
                GridViewAccounts.DataBind();
            }
        }


        protected void GridViewAccounts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelAcc.Text = "";
            GridViewAccounts.EditIndex = e.NewEditIndex;
            BindAccountsData();
        }

        protected void GridViewAccounts_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ErrorLabelAcc.Text = "";

            try
            {
                int accountID = Convert.ToInt32(GridViewAccounts.DataKeys[e.RowIndex].Value);
                GridViewRow row = GridViewAccounts.Rows[e.RowIndex];
                TextBox isAdminTextBox = row.FindControl("TextBoxEditIsAdmin") as TextBox;
                
                if (isAdminTextBox != null)
                {
                    bool isAdmin = isAdminTextBox.Text == "true" ?true:false ;
                  
                    using (var _db = new DataContext())
                    {
                        var acc = _db.Accounts.Find(accountID);
                        if (acc != null)
                        {
                            acc.isAdmin = isAdmin;
                            _db.SaveChanges();
                        }
                        else
                        {
                            ErrorLabelAcc.Text = "Account not found.";
                        }
                    }

                    GridViewAccounts.EditIndex = -1;
                    BindAccountsData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabelAcc.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewAccounts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewAccounts.EditIndex = -1;
            BindAccountsData();
        }

        protected void GridViewAccounts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelAcc.Text = "";
            int accountID = Convert.ToInt32(GridViewAccounts.DataKeys[e.RowIndex].Value);
            using (var _db = new DataContext())
            {
                var enrols = _db.Enrolments.Where(p => p.AccountID == accountID);
                if (enrols.Count() > 0)
                {
                    ErrorLabelAcc.Text = "That Account can not be deleted because there are some Enrolments relevant to Account";
                    return;
                }              
            }

            using (var _db = new DataContext())
            {
                var account = _db.Accounts.Find(accountID);
                _db.Accounts.Remove(account);
                _db.SaveChanges();
            }

            BindAccountsData();
        }


        protected void LinkButtonToggleAccManagement_Click(object sender, EventArgs e)
        {
            Session["quanlytaikhoan"] = true;
            Session["quanlykhoahoc"] = false;           
            Session["quanlyquiz"] = false;
            Session["quanlyquizdetail"] = false;
            Session["quanlydangky"] = false;
            Session["quanlydanhmuc"] = false;
            Session["quanlybaihoc"] = false;
        }

        //QUIZ MANAGEMENT
        private void BindQuizData()
        {
            ErrorLabelQuiz.Text = "";

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

        protected void ButtonAddNewQuiz_Click(object sender, EventArgs e)
        {
            ErrorLabelQuiz.Text = "";
            PanelAddEditQuiz.Visible = true;
            LabelFormTitleQuiz.Text = "Add Quiz";
            ClearFormQuiz();
        }

        protected void ButtonSaveQuiz_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListLessonID.SelectedValue == "0" || string.IsNullOrEmpty(TextBoxQuestionsNumber.Text))
                {
                    ErrorLabelQuiz.Text = "Please select a Lesson and enter Questions Number.";
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
                PanelAddEditQuiz.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabelQuiz.Text = "An error occurred while saving the quiz: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ErrorLabelQuiz.Text += " Inner exception: " + ex.InnerException.Message;
                }
            }
        }

        protected void ButtonCancelQuiz_Click(object sender, EventArgs e)
        {
            PanelAddEditQuiz.Visible = false;
        }

        protected void GridViewQuiz_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelQuiz.Text = "";
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
                            ErrorLabelQuiz.Text = "Quiz not found.";
                        }
                    }

                    GridViewQuiz.EditIndex = -1;
                    BindQuizData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabelQuiz.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewQuiz_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewQuiz.EditIndex = -1;
            BindQuizData();
        }

        protected void GridViewQuiz_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelQuiz.Text = "";

            int quizID = Convert.ToInt32(GridViewQuiz.DataKeys[e.RowIndex].Value);
            using (var _db = new DataContext())
            {
                var quizDetails = _db.QuizDetails.Where(p => p.QuizDetailID == quizID);
                if (quizDetails.Count() > 0)
                {
                    ErrorLabelQuiz.Text = "That Quiz can not be deleted because there are some Quiz Detail relevant to Quiz";
                    return;
                }
               
            }

            using (var _db = new DataContext())
            {
                var quizDetails = _db.QuizDetails.Where(p => p.QuizDetailID == quizID);
                if (quizDetails.Count() > 0)
                {
                    ErrorLabelAcc.Text = "That Quiz can not be deleted because there are some Quiz details relevant to Quiz";
                    return;
                }
            }

            using (var _db = new DataContext())
            {
                var quiz = _db.Quizs.Find(quizID);
                _db.Quizs.Remove(quiz);
                _db.SaveChanges();
            }

            BindQuizData();
        }

        private void ClearFormQuiz()
        {
            TextBoxQuizID.Text = "";
            DropDownListLessonID.SelectedIndex = 0;
            TextBoxQuestionsNumber.Text = "";
        }

        protected void LinkButtonToggleQuizManagement_Click(object sender, EventArgs e)
        {
            BindLessonDropdown();
            Session["quanlyquiz"] = true;
            Session["quanlytaikhoan"] = false;
            Session["quanlykhoahoc"] = false;
            Session["quanlyquizdetail"] = false;
            Session["quanlydangky"] = false;
            Session["quanlydanhmuc"] = false;
            Session["quanlybaihoc"] = false;
        }

        // QUIZ DETAIL MANAGEMENT
        private void BindQuizDetailData()
        {
            ErrorLabelQuizDetail.Text = "";

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

        protected void ButtonAddNewQuizDetail_Click(object sender, EventArgs e)
        {
            ErrorLabelQuizDetail.Text = "";
            PanelAddEditQuizDetail.Visible = true;
            LabelFormTitleQuizDetail.Text = "Add Quiz Detail";
            ClearFormQuizDetail();
        }

        protected void ButtonSaveQuizDetail_Click(object sender, EventArgs e)
        {
            try
            {
                if (DropDownListQuizID.SelectedValue == "0" || string.IsNullOrEmpty(TextBoxQuestion.Text) || string.IsNullOrEmpty(TextBoxAnswer.Text))
                {
                    ErrorLabelQuizDetail.Text = "Please select Quiz and enter required information";
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
                ErrorLabelQuizDetail.Text = "";
                PanelAddEditQuizDetail.Visible = false;
            }
            catch (Exception ex)
            {
                ErrorLabelQuizDetail.Text = "An error occurred while saving the quiz detail: " + ex.Message;
                if (ex.InnerException != null)
                {
                    ErrorLabelQuizDetail.Text += " Inner exception: " + ex.InnerException.Message;
                }
            }
        }

        protected void ButtonCancelQuizDetail_Click(object sender, EventArgs e)
        {
            PanelAddEditQuizDetail.Visible = false;
        }

        protected void GridViewQuizDetail_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ErrorLabelQuizDetail.Text = "";

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
                            ErrorLabelQuizDetail.Text = "Quiz Detail not found.";
                        }
                    }

                    GridViewQuizDetail.EditIndex = -1;
                    BindQuizDetailData();
                }
            }
            catch (Exception ex)
            {
                ErrorLabelQuizDetail.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridViewQuizDetail_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewQuizDetail.EditIndex = -1;
            BindQuizDetailData();
        }

        protected void GridViewQuizDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelQuizDetail.Text = "";

            int quizDetailID = Convert.ToInt32(GridViewQuizDetail.DataKeys[e.RowIndex].Value);

            using (var _db = new DataContext())
            {
                var quizDetail = _db.QuizDetails.Find(quizDetailID);
                _db.QuizDetails.Remove(quizDetail);
                _db.SaveChanges();
            }

            BindQuizDetailData();
        }

        private void ClearFormQuizDetail()
        {

            DropDownListQuizID.SelectedIndex = 0;
            TextBoxQuestion.Text = "";
            TextBoxAnswer.Text = "";

        }

        protected void LinkButtonToggleQuizDetailManagement_Click(object sender, EventArgs e)
        {
            BindQuizDropdown();
            Session["quanlyquizdetail"] = true;
            Session["quanlytaikhoan"] = false;
            Session["quanlykhoahoc"] = false;
            Session["quanlyquiz"] = false;
            Session["quanlydangky"] = false;
            Session["quanlydanhmuc"] = false;
            Session["quanlybaihoc"] = false;
        }

        //ENROL MANAGEMENT
        private void BindEnrolData()
        {
            ErrorLabelEnrol.Text = "";

            using (var _db = new DataContext())
            {
                var enrolData = from enrol in _db.Enrolments
                                join account in _db.Accounts on enrol.AccountID equals account.AccountID
                                select new
                                {
                                    EnrolmentID = enrol.EnrolmentID,
                                    Enrolment_date = enrol.Enrolment_date,
                                    Completed_date = enrol.Completed_date,
                                    Username = account.Username,
                                };

                GridViewEnrol.DataSource = enrolData.ToList();
                GridViewEnrol.DataBind();
            }
        }

        private void BindCourseDropdown()
        {
            DropDownListCourseID.Items.Clear();
            using (var _db = new DataContext())
            {
                var courses = _db.Courses.Select(course => new { course.CourseID, course.CourseName }).ToList();
                DropDownListCourseID.Items.Add(new ListItem("All courses", "0"));
                foreach (var course in courses)
                {
                    DropDownListCourseID.Items.Add(new ListItem(course.CourseName, course.CourseID.ToString()));
                }

            }
        }       
        protected void Courses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorLabelEnrol.Text = "";

            int selectedCourseID = Convert.ToInt32(DropDownListCourseID.SelectedValue);
            if (selectedCourseID != 0)
            {
                using (var _db = new DataContext())
                {
                    var enrolData = from enrol in _db.Enrolments
                                    where enrol.CourseID == selectedCourseID
                                    join account in _db.Accounts on enrol.AccountID equals account.AccountID
                                    select new
                                    {
                                        EnrolmentID = enrol.EnrolmentID,
                                        Enrolment_date = enrol.Enrolment_date,
                                        Completed_date = enrol.Completed_date,
                                        Username = account.Username,
                                    };

                    GridViewEnrol.DataSource = enrolData.ToList();
                    GridViewEnrol.DataBind();
                }
            }
            else
            {
                BindEnrolData();
            }

        }

        protected void GridViewEnrol_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ErrorLabelEnrol.Text = "";

            int enrolmentID = Convert.ToInt32(GridViewEnrol.DataKeys[e.RowIndex].Value);

            using (var _db = new DataContext())
            {
                var enrol = _db.Enrolments.Find(enrolmentID);
                _db.Enrolments.Remove(enrol);
                _db.SaveChanges();
            }

            BindEnrolData();
            DropDownListCourseID.SelectedIndex = 0;
        }



        protected void LinkButtonToggleEnrolManagement_Click(object sender, EventArgs e)
        {
            BindCourseDropdown();
            Session["quanlydangky"] = true;
            Session["quanlyquizdetail"] = false;
            Session["quanlytaikhoan"] = false;
            Session["quanlykhoahoc"] = false;
            Session["quanlyquiz"] = false;
            Session["quanlydanhmuc"] = false;
            Session["quanlybaihoc"] = false;
        }
    }
}