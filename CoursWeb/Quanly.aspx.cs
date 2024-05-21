using CoursWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CoursWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonAddNewLesson.Visible = false;
                GridViewLessons.Visible = false;
                BindGridView();
                BindCoursesDropdown();
                LoadLessonsForSelectedCourse();
            }
        }
        private void BindGridView()
        {
            using (var context = new DataContext())
            {
                var categories = context.Categories.ToList();
                GridView1.DataSource = categories;
                GridView1.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
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
            int categoryId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["CategoryID"]);

            using (var context = new DataContext())
            {
                var courseExists= context.Courses.Any(c => c.CategoryID==categoryId);
                if (courseExists)
                {
                    ErrorLabel.Text = "Danh mục hiện đang có khóa học, không thể xóa";
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
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void ButtonAddNew_Click(object sender, EventArgs e)
        {
            PanelAddEdit.Visible = true;
            ClearForm();
        }
        private void ClearForm()
        {
            TextCategoryname.Text = "";
            TextDes.Text = "";

        }
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            try
            {
                String txtname = TextCategoryname.Text;
                String txtdescription = TextDes.Text;
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
                PanelAddEdit.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            PanelAddEdit.Visible = false;
        }

        ////////////////////////////////////////////////

        private void BindCoursesDropdown()
        {
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

            using (var context = new DataContext())
            {
                var lesson = context.Lessons.FirstOrDefault(l => l.LessonID == lessonId);
                if (lesson != null)
                {
                    lesson.LessonName = txtLessonName.Text;
                    lesson.Lesson_URL = txtLessonURL.Text;
                    context.SaveChanges();
                }
            }

            GridViewLessons.EditIndex = -1;
            LoadLessonsForSelectedCourse();
        }

        protected void GridViewLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lessonId = Convert.ToInt32(GridViewLessons.DataKeys[e.RowIndex].Values["LessonID"]);

            using (var context = new DataContext())
            {
                var quizExists = context.Quizs.Any(q => q.LessonID == lessonId);
                if (quizExists)
                {
                    ErrorLabel.Text = "Còn câu hỏi trong bài học không thể xóa được";
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
            if (DropdownCourses.SelectedValue != "0")
            {
                PanelAddEditLesson.Visible = true;
                ClearLessonForm();
            }
            else
            {
                PanelAddEditLesson.Visible = false;
            }
        }

        private void ClearLessonForm()
        {
            TextLessonName.Text = "";
            TextLessonURL.Text = "";
        }

        protected void ButtonSaveLesson_Click(object sender, EventArgs e)
        {
            try
            {
                string lessonName = TextLessonName.Text;
                string lessonURL = TextLessonURL.Text;
                int selectedCourseID = Convert.ToInt32(DropdownCourses.SelectedValue);
                using (var context = new DataContext())
                {
                    var usedLessonID = context.Lessons.Where(l => l.CourseID == selectedCourseID).Select(l =>l.LessonID).ToList();
                    int newLessonID = 1;
                    if (usedLessonID.Any())
                    {
                        for(int i = 1;i<=usedLessonID.Max();i++)
                        {
                            if (!usedLessonID.Contains(i))
                            {
                                newLessonID = 1;
                                break;
                            }
                        }
                    }
                    var course = context.Courses.FirstOrDefault(c => c.CourseID == selectedCourseID);
                    var newLesson = new Lesson()
                    {
                        LessonID = newLessonID,
                        LessonName = lessonName,
                        Lesson_URL = lessonURL,
                        Description = course.Description,
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
                ErrorLabel.Text = "Đã xảy ra lỗi khi lưu bài học. Vui lòng thử lại sau.";
            }
        }

        protected void ButtonCancelLesson_Click(object sender, EventArgs e)
        {
            PanelAddEditLesson.Visible = false;
        }
    }
}