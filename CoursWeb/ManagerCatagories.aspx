<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerCatagories.aspx.cs" Inherits="CoursWeb.ManagerCatagories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="largesi">
           Khóa học <%= categoryName %>
    </h1>
    <h2 class="smallsi">Các Khóa học để bắt đầu</h2>
    <div>
        <p class="lite">Khám phá các khóa học do các chuyên gia giàu kinh nghiệm trong ngành giảng dạy</p>
        <asp:ListView ID="courseList" runat="server">
        <EmptyDataTemplate>
            <table >
                <tr>
                    <td>Khong co khoa hoc nao</td>
                </tr>
            </table>
    
        </EmptyDataTemplate>
         <GroupTemplate>
            <tr id="itemPlaceholderContainer" runat="server">
                <td id="itemPlaceholder" runat="server"></td>
            </tr>
        </GroupTemplate>
        <ItemTemplate>   
                <a href="/coursedetail?courseId=<%# Eval("CourseID") %>"class="link" >
                    <div class="courseItem">
                    <img src="<%# Eval("Image") %>" class="courseImg"/>
                    <h2 class="courseName"><%#Eval("CourseName") %> </h2>                            
                    <span class="coursePrice"><%# Eval("Price") %> VND</span>
                    </div>
                  </a>
        </ItemTemplate>
        </asp:ListView>

    </div>
</asp:Content>
