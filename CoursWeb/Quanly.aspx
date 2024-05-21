<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quanly.aspx.cs" Inherits="CoursWeb.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="~/Content/styles.css" />
    <div class="userContainer">
        <div class="userLeft">
             <asp:LinkButton ID="LinkButtonToggleEnrolManagement" runat="server" Text="Quản Lý Enrolment" CssClass="btn-User link" OnClick="LinkButtonToggleEnrolManagement_Click"/>
        </div>
        <div class="userRight">
             <%if (Convert.ToBoolean(Session["quanlydangkykh"]))
                 {%>
                <asp:Panel ID="PanelEnrolManagement" runat="server" >                       
                    <asp:DropDownList ID="DropDownListCourseID" runat="server" OnSelectedIndexChanged="Courses_SelectedIndexChanged" CssClass="dropdown" AutoPostBack=True></asp:DropDownList>                 
                    </asp:Panel>
                    <asp:GridView ID="GridViewEnrol" runat="server" AutoGenerateColumns="False" DataKeyNames="EnrolmentID" OnRowDeleting="GridViewEnrol_RowDeleting" CssClass="gridview-quiz">
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
                <asp:Label ID="ErrorLabel" runat="server" CssClass="error-label"></asp:Label>
            <%}%>
            </div>
        </div>
</asp:Content>