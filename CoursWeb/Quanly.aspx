<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quanly.aspx.cs" Inherits="CoursWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="~/Content/styles.css" />
    <div class="userContainer">
        <div class="userLeft">
             <asp:LinkButton ID="LinkButtonToggleQuizDetailManagement" runat="server" Text="Quản Lý Quiz Detail" CssClass="btn-User link" OnClick="LinkButtonToggleQuizDetailManagement_Click"/>
        </div>
        <div class="userRight">
             <%if (Convert.ToBoolean(Session["quanlyquizdetail"]))
                 {%>
                <asp:Panel ID="PanelQuizManagement" runat="server" >
                    <asp:Button ID="ButtonAddNew" runat="server" Text="Add New Quiz Detail" OnClick="ButtonAddNew_Click" CssClass="gridview-button" />
                    <asp:Panel ID="PanelAddEdit" runat="server" Visible="False" CssClass="panel-add-edit">
                        <h3><asp:Label ID="LabelFormTitle" runat="server" Text="Add/Edit Quiz" /></h3>                        
                        <asp:DropDownList ID="DropDownListQuizID" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TextBoxQuestion" runat="server" Placeholder="Question"></asp:TextBox>
                        <asp:TextBox ID="TextBoxAnswer" runat="server" Placeholder="Answer"></asp:TextBox>
                        <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="ButtonSave_Click" CssClass="gridview-button" />
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" CssClass="gridview-button" />
                    </asp:Panel>
                    <asp:GridView ID="GridViewQuizDetail" runat="server" AutoGenerateColumns="False" DataKeyNames="QuizDetailID" OnRowEditing="GridViewQuizDetail_RowEditing" OnRowUpdating="GridViewQuizDetail_RowUpdating" OnRowCancelingEdit="GridViewQuizDetail_RowCancelingEdit" OnRowDeleting="GridViewQuizDetail_RowDeleting" CssClass="gridview-quiz">
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
                <asp:Label ID="ErrorLabel" runat="server" CssClass="error-label"></asp:Label>
            <%}%>
            </div>
        </div>
</asp:Content>