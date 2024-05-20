<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quanly.aspx.cs" Inherits="CoursWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="~/CSS/styles.css" />
    <div class="userContainer">
        <div class="userLeft">
             <asp:LinkButton ID="LinkButtonToggleQuizManagement" runat="server" Text="Quản Lý Quiz" CssClass="btn-User link" OnClick="LinkButtonToggleQuizManagement_Click"/>
        </div>
        <div class="userRight">
             <%if (Convert.ToBoolean(Session["quanlyquiz"]))
                 {%>
                <asp:Panel ID="PanelQuizManagement" runat="server" >
                    <asp:Button ID="ButtonAddNew" runat="server" Text="Add New Quiz" OnClick="ButtonAddNew_Click" CssClass="gridview-button" />
                    <asp:Panel ID="PanelAddEdit" runat="server" Visible="False" CssClass="panel-add-edit">
                        <h3><asp:Label ID="LabelFormTitle" runat="server" Text="Add/Edit Quiz" /></h3>
                        <asp:TextBox ID="TextBoxQuizID" runat="server" Placeholder="Quiz ID" Visible="false"></asp:TextBox>
                        <asp:DropDownList ID="DropDownListLessonID" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TextBoxQuestionsNumber" runat="server" Placeholder="Questions Number"></asp:TextBox>
                        <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" CssClass="gridview-button" />
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="gridview-button" />
                    </asp:Panel>
                    <asp:GridView ID="GridViewQuiz" runat="server" AutoGenerateColumns="False" DataKeyNames="QuizID" OnRowEditing="GridViewQuiz_RowEditing" OnRowUpdating="GridViewQuiz_RowUpdating" OnRowCancelingEdit="GridViewQuiz_RowCancelingEdit" OnRowDeleting="GridViewQuiz_RowDeleting" CssClass="gridview-quiz">
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
                <asp:Label ID="ErrorLabel" runat="server" CssClass="error-label"></asp:Label>
            <%}%>
            </div>
        </div>
</asp:Content>