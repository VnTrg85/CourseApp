<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quanly.aspx.cs" Inherits="CoursWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="css/style.css" />
        <div class="myrow">
            <div class="coltrai">
                <ul id="menu1">
                    <li>
                        <a href="">Quan ly danh muc</a>
                    </li>
                    <li>
                        <a href="">Quan ly bai hoc</a>
                    </li>
                </ul>
            </div>
            <div class="colphai">
                <asp:Button ID="ButtonAddNew" runat="server" Text="Add New Category" OnClick="ButtonAddNew_Click" CSSClass="gridview-button" />
                <asp:Panel ID="PanelAddEdit" runat="server" Visible="False" CSSClass="panel-add-edit">
                    <h2>Thêm mới danh mục</h2>
                    <asp:TextBox ID="TextCategoryname" runat="server" placeholder="Tên danh mục"></asp:TextBox>
                    <asp:TextBox ID="TextDes" runat="server" placeholder="Mô tả danh mục"></asp:TextBox>
                    <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" CSSClass="gridview-button"/>
                    <asp:Button ID="ButtonCancel" runat="server"  Text="Cancel" OnClick="ButtonCancel_Click" CSSClass="gridview-button" />
                </asp:Panel>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID"
                        OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
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
                            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                
            </div>
        </div>
        <div>
            <asp:DropDownList ID="DropdownCourses" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropdownCourses_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="ButtonAddNewLesson" runat="server" Text="Add New Lesson" OnClick="ButtonAddNewLesson_Click" CssClass="gridview-button" Visible="false" />
            <asp:Panel ID="PanelAddEditLesson" runat="server" Visible="False" CssClass="panel-add-edit">
                <h2>Add New Lesson</h2>
                <asp:TextBox ID="TextLessonName" runat="server" placeholder="Lesson Name"></asp:TextBox>
                <asp:TextBox ID="TextLessonURL" runat="server" placeholder="Lesson URL"></asp:TextBox>
                <asp:Button ID="ButtonSaveLesson" runat="server" Text="Save" OnClick="ButtonSaveLesson_Click" CssClass="gridview-button" />
                <asp:Button ID="ButtonCancelLesson" runat="server" Text="Cancel" OnClick="ButtonCancelLesson_Click" CssClass="gridview-button" />
            </asp:Panel>
            <asp:GridView ID="GridViewLessons" runat="server" AutoGenerateColumns="False" DataKeyNames="LessonID"
                    OnRowEditing="GridViewLessons_RowEditing" OnRowCancelingEdit="GridViewLessons_RowCancelingEdit"
                    OnRowUpdating="GridViewLessons_RowUpdating" OnRowDeleting="GridViewLessons_RowDeleting" Visible="false">
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
                        <asp:TemplateField HeaderText="Lesson Video URL">
                            <ItemTemplate>
                                <asp:Label ID="lblLessonURL" runat="server" Text='<%# Eval("Lesson_URL") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLessonURL" runat="server" Text='<%# Bind("Lesson_URL") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            
        </div>
        <asp:Label ID="ErrorLabel" runat="server" CssClass="error-label"></asp:Label>
</asp:Content>
