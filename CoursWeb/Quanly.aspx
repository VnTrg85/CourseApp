<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quanly.aspx.cs" Inherits="CoursWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="~/Content/styles.css" />
    <div class="userContainer">
        <div class="userLeft">
             <asp:LinkButton ID="LinkButtonToggleCateManagement" runat="server" Text="Quản Lý Categories" CssClass="btn-User link" OnClick="LinkButtonToggleCateManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleCoursesManagement" runat="server" Text="Quản Lý Courses" CssClass="btn-User link" OnClick="LinkButtonToggleCoursesManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleLessonsManagement" runat="server" Text="Quản Lý Lessons" CssClass="btn-User link" OnClick="LinkButtonToggleLessonManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleAccManagement" runat="server" Text="Quản Lý Accounts" CssClass="btn-User link" OnClick="LinkButtonToggleAccManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleQuizManagement" runat="server" Text="Quản Lý Quizs" CssClass="btn-User link" OnClick="LinkButtonToggleQuizManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleQuizDetailManagement" runat="server" Text="Quản Lý Quiz Details" CssClass="btn-User link" OnClick="LinkButtonToggleQuizDetailManagement_Click"/>
             <asp:LinkButton ID="LinkButtonToggleEnrolManagement" runat="server" Text="Quản Lý En<a href="Quanly.aspx">Quanly.aspx</a>rolments" CssClass="btn-User link" OnClick="LinkButtonToggleEnrolManagement_Click"/>
        </div>
        <div class="userRight">        

            <%if (Convert.ToBoolean(Session["quanlydanhmuc"]))
                 {%>
                <asp:Button ID="ButtonAddNewCate" runat="server" Text="Add New Category" OnClick="ButtonAddNewCate_Click" CssClass="gridview-button" />
                <asp:Panel ID="PanelAddEditCate" runat="server" Visible="False" CssClass="panel-add-edit">
                    <h2>Add new category</h2>
                    <asp:TextBox ID="TextCategoryname" runat="server" placeholder="Tên danh mục"></asp:TextBox>
                    <asp:TextBox ID="TextDes" runat="server" placeholder="Mô tả danh mục"></asp:TextBox>
                    <asp:Button ID="ButtonSaveCate" runat="server" Text="Save" OnClick="ButtonSaveCate_Click" CssClass="gridview-button"/>
                    <asp:Button ID="ButtonCancelCate" runat="server"  Text="Cancel" OnClick="ButtonCancelCate_Click" CssClass="gridview-button" />
                </asp:Panel>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID"
                        OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="CategoryID" HeaderText="Category ID" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Category Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="ErrorLabelCate" runat="server" CssClass="error-label"></asp:Label>
            <%}%>

             <%if (Convert.ToBoolean(Session["quanlykhoahoc"]))
                 {%>
                <asp:Panel ID="PanelCoursesManagement" runat="server" >
                    <asp:Button ID="ButtonAddNewCourse" runat="server" Text="Add New Course" OnClick="ButtonAddNewCourse_Click" CssClass="gridview-button" />
                    <asp:Panel ID="PanelAddEditCourse" runat="server" Visible="False" CssClass="panel-add-edit">
                        <h3><asp:Label ID="LabelFormTitleCourse" runat="server" /></h3>
                        <asp:DropDownList ID="DropDownListCateID" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TextBoxName" runat="server" Placeholder="Coure Name"></asp:TextBox>
                        <asp:TextBox ID="TextBoxDesc" runat="server" Placeholder="Coure Description"></asp:TextBox>
                        <asp:TextBox ID="TextBoxImage" runat="server" Placeholder="Course Image"></asp:TextBox>
                        <asp:TextBox ID="TextBoxPrice" runat="server" Placeholder="Course Price"></asp:TextBox>
                        <asp:Button ID="ButtonSaveCourse" runat="server" Text="Save" OnClick="ButtonSaveCourse_Click" CssClass="gridview-button" />
                        <asp:Button ID="ButtonCancelCourse" runat="server" Text="Cancel" OnClick="ButtonCancelCourse_Click" CssClass="gridview-button" />
                    </asp:Panel>
                    <asp:GridView ID="GridViewCourses" runat="server" AutoGenerateColumns="False" EmptyDataText="Empty Course" DataKeyNames="CourseID" OnRowEditing="GridViewCourses_RowEditing" OnRowUpdating="GridViewCourses_RowUpdating" OnRowCancelingEdit="GridViewCourses_RowCancelingEdit" OnRowDeleting="GridViewCourses_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="CourseID" HeaderText="Course ID" ReadOnly="True" SortExpression="CourseID" />
                            <asp:TemplateField HeaderText="Course Name" SortExpression="CourseName">
                                <ItemTemplate>
                                    <%# Eval("CourseName") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditName" runat="server" Text='<%# Bind("CourseName") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>    
                            <asp:TemplateField HeaderText="Course Description" SortExpression="Description">
                                <ItemTemplate >
                                    <%# Eval("Description") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditDesc" runat="server" Text='<%# Bind("Description") %>' />
                             
                                </EditItemTemplate>
                            </asp:TemplateField>                           
                            <asp:TemplateField HeaderText="Price" SortExpression="Price">
                                <ItemTemplate>
                                    <%# Eval("Price") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditPrice" runat="server" Text='<%# Bind("Price") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>                     
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="ErrorLabelCourse" runat="server" CssClass="error-label"></asp:Label>
            <%}%>

            <%if (Convert.ToBoolean(Session["quanlybaihoc"]))
                 {%>
               <asp:DropDownList ID="DropdownCourses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropdownCourses_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="ButtonAddNewLesson" runat="server" Text="Add New Lesson" OnClick="ButtonAddNewLesson_Click" CssClass="gridview-button" Visible="false" />
            <asp:Panel ID="PanelAddEditLesson" runat="server" Visible="False" CssClass="panel-add-edit">
                <h2>Add New Lesson</h2>
                <asp:TextBox ID="TextLessonName" runat="server" placeholder="Lesson Name"></asp:TextBox>
                <asp:TextBox ID="TextLessonURL" runat="server" placeholder="Lesson URL"></asp:TextBox>
                <asp:TextBox ID="TextLessonDesc" runat="server" placeholder="Lesson Description"></asp:TextBox>
                <asp:Button ID="ButtonSaveLesson" runat="server" Text="Save" OnClick="ButtonSaveLesson_Click" CssClass="gridview-button" />
                <asp:Button ID="ButtonCancelLesson" runat="server" Text="Cancel" OnClick="ButtonCancelLesson_Click" CssClass="gridview-button" />
            </asp:Panel>
            <asp:GridView ID="GridViewLessons" runat="server" AutoGenerateColumns="False" DataKeyNames="LessonID"
                    OnRowEditing="GridViewLessons_RowEditing" OnRowCancelingEdit="GridViewLessons_RowCancelingEdit"
                    OnRowUpdating="GridViewLessons_RowUpdating" OnRowDeleting="GridViewLessons_RowDeleting" Visible="false" CssClass="gridview-quiz">
                    <Columns>
                        <asp:BoundField DataField="LessonID" HeaderText="Lesson ID" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Lesson Name">
                            <ItemTemplate>
                                <asp:Label ID="lblLessonName" runat="server" Text='<%# Eval("LessonName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLessonName" runat="server" Text='<%# Bind("LessonName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Lesson Description">
                            <ItemTemplate>
                                <asp:Label ID="lblLessonDesc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLessonDesc" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Lesson Video URL">
                            <ItemTemplate>
                                <asp:Label ID="lblLessonURL" runat="server" Text='<%# Eval("Lesson_URL") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLessonURL" runat="server" Text='<%# Bind("Lesson_URL") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
               <asp:Label ID="ErrorLabelLesson" runat="server" CssClass="error-label"></asp:Label>
            <%}%>


            <%if (Convert.ToBoolean(Session["quanlytaikhoan"]))
                 {%>
                <asp:Panel ID="PanelAccountManagement" runat="server" >                   
                    <asp:GridView ID="GridViewAccounts" runat="server" AutoGenerateColumns="False" EmptyDataText="Empty Acocunt" DataKeyNames="AccountID" OnRowEditing="GridViewAccounts_RowEditing" OnRowUpdating="GridViewAccounts_RowUpdating" OnRowCancelingEdit="GridViewAccounts_RowCancelingEdit" OnRowDeleting="GridViewAccounts_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="AccountID" HeaderText="Account ID" ReadOnly="True" SortExpression="AccountID" />
                            <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="True" SortExpression="Username" />
                            <asp:BoundField DataField="Password" HeaderText="Password" ReadOnly="True" SortExpression="Password" />
                            <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True" SortExpression="Email" />          
                                                     
                            <asp:TemplateField HeaderText="Is admin" SortExpression="isAdmin">
                                <ItemTemplate>
                                    <%# Eval("isAdmin") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditIsAdmin" runat="server" Text='<%# Bind("isAdmin") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>                     
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="ErrorLabelAcc" runat="server" CssClass="error-label"></asp:Label>
            <%}%>
            <%if (Convert.ToBoolean(Session["quanlyquiz"]))
                 {%>
                <asp:Panel ID="PanelQuizManagement" runat="server" >
                    <asp:Button ID="ButtonAddNewQuiz" runat="server" Text="Add New Quiz" OnClick="ButtonAddNewQuiz_Click" CssClass="gridview-button" />
                    <asp:Panel ID="PanelAddEditQuiz" runat="server" Visible="False" CssClass="panel-add-edit">
                        <h3><asp:Label ID="LabelFormTitleQuiz" runat="server" Text="Add/Edit Quiz" /></h3>
                        <asp:TextBox ID="TextBoxQuizID" runat="server" Placeholder="Quiz ID" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="DropDownListLessonID" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TextBoxQuestionsNumber" runat="server" Placeholder="Questions Number"></asp:TextBox>
                        <asp:Button ID="ButtonSaveQuiz" runat="server" Text="Save" OnClick="ButtonSaveQuiz_Click" CssClass="gridview-button" />
                        <asp:Button ID="ButtonCancelQuiz" runat="server" Text="Cancel" OnClick="ButtonCancelQuiz_Click" CssClass="gridview-button" />
                    </asp:Panel>
                    <asp:GridView ID="GridViewQuiz" runat="server" AutoGenerateColumns="False" EmptyDataText="Empty Quiz"  DataKeyNames="QuizID" OnRowEditing="GridViewQuiz_RowEditing" OnRowUpdating="GridViewQuiz_RowUpdating" OnRowCancelingEdit="GridViewQuiz_RowCancelingEdit" OnRowDeleting="GridViewQuiz_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="QuizID" HeaderText="Quiz ID" ReadOnly="True" SortExpression="QuizID" />

                            <asp:BoundField DataField="LessonName" HeaderText="Lesson Name" ReadOnly="True" SortExpression="LessonName" />
                            
                            <asp:TemplateField HeaderText="Questions Number" SortExpression="Questions_number">
                                <ItemTemplate>
                                    <%# Eval("Questions_number") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditQuestionsNumber" runat="server" Text='<%# Bind("Questions_number") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="ErrorLabelQuiz" runat="server" CssClass="error-label"></asp:Label>
            <%}%>

             <%if (Convert.ToBoolean(Session["quanlyquizdetail"]))
                 {%>
                <asp:Panel ID="PanelQuizDetailManagement" runat="server" >
                    <asp:Button ID="ButtonAddNewQuizDetail" runat="server" Text="Add New Quiz Detail" OnClick="ButtonAddNewQuizDetail_Click" CssClass="gridview-button" />
                    <asp:Panel ID="PanelAddEditQuizDetail" runat="server" Visible="False" CssClass="panel-add-edit">
                        <h3><asp:Label ID="LabelFormTitleQuizDetail" runat="server" Text="Add/Edit Quiz QuizDetail" /></h3>                        
                        <asp:DropDownList ID="DropDownListQuizID" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TextBoxQuestion" runat="server" Placeholder="Question"></asp:TextBox>
                        <asp:TextBox ID="TextBoxAnswer" runat="server" Placeholder="Answer"></asp:TextBox>
                        <asp:Button ID="ButtonSaveQuizDetail" runat="server" Text="Save" OnClick="ButtonSaveQuizDetail_Click" CssClass="gridview-button" />
                        <asp:Button ID="ButtonCancelQuizDetail" runat="server" Text="Cancel" OnClick="ButtonCancelQuizDetail_Click" CssClass="gridview-button" />
                    </asp:Panel>
                    <asp:GridView ID="GridViewQuizDetail" runat="server" AutoGenerateColumns="False" EmptyDataText="Empty Quiz Detail"  DataKeyNames="QuizDetailID" OnRowEditing="GridViewQuizDetail_RowEditing" OnRowUpdating="GridViewQuizDetail_RowUpdating" OnRowCancelingEdit="GridViewQuizDetail_RowCancelingEdit" OnRowDeleting="GridViewQuizDetail_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="QuizDetailID" HeaderText="Quiz Detail ID" ReadOnly="True" SortExpression="QuizDetailID" />

                            <asp:BoundField DataField="QuizID" HeaderText="Quiz ID" ReadOnly="True" SortExpression="QuizID" />
                            
                            <asp:TemplateField HeaderText="Question" SortExpression="Question">
                                <ItemTemplate>
                                    <%# Eval("Question") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBoxEditQuestion" runat="server" Text='<%# Bind("Question") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Answer" SortExpression="Answer">
                                 <ItemTemplate>
                                     <%# Eval("Answer") %>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:TextBox ID="TextBoxEditAnswer" runat="server" Text='<%# Bind("Answer") %>' />
                                 </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="Edit" Text="Edit" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButtonUpdate" runat="server" CommandName="Update" Text="Update" CssClass="gridview-button edit-button" />
                                    <asp:LinkButton ID="LinkButtonCancel" runat="server" CommandName="Cancel" Text="Cancel" CssClass="gridview-button delete-button" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="ErrorLabelQuizDetail" runat="server" CssClass="error-label"></asp:Label>
            <%}%>
                <%if (Convert.ToBoolean(Session["quanlydangky"]))
                 {%>
                <asp:Panel ID="PanelEnrolManagement" runat="server" >                       
                    <asp:DropDownList ID="DropDownListCourseID" runat="server" OnSelectedIndexChanged="Courses_SelectedIndexChanged" CssClass="dropdown" AutoPostBack=True></asp:DropDownList>                 
  
                    <asp:GridView ID="GridViewEnrol" runat="server" AutoGenerateColumns="False" EmptyDataText="Empty enrolmments" DataKeyNames="EnrolmentID" OnRowDeleting="GridViewEnrol_RowDeleting" CssClass="gridview-quiz">
                        <Columns>
                            <asp:BoundField DataField="EnrolmentID" HeaderText="Enrolment ID" ReadOnly="True" SortExpression="EnrolmentID" />
                            <asp:BoundField DataField="Enrolment_date" HeaderText="Enrolment Date" ReadOnly="True" SortExpression="Enrolment_date" />
                            <asp:BoundField DataField="Completed_date" HeaderText="Completed Date" ReadOnly="True" SortExpression="Completed_date" />
                            <asp:BoundField DataField="Username" HeaderText="Customer Name" ReadOnly="True" SortExpression="Username" />                   
                            <asp:TemplateField HeaderText="Chức Năng">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonDelete" runat="server" CommandName="Delete" Text="Delete" CssClass="gridview-button delete-button" />
                                </ItemTemplate>                               
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Label ID="ErrorLabelEnrol" runat="server" CssClass="error-label"></asp:Label>
            <%}%>




                </div>
            </div>
</asp:Content>
