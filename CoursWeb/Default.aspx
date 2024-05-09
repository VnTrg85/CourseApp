<%@ Page Title="Courses" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CoursWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
       <img class ="homeImg" src="https://img-c.udemycdn.com/notices/web_carousel_slide/image/7b068e4a-1ecf-481e-aaa4-5018a2fffa27.jpg"/>
        <div runat="server">
             <hgroup>
                <h2 class="homeTitle">A broad selection of courses</h2>
            </hgroup>
            <div class="courseContainer">
            <asp:ListView ID="courseList" runat="server" DataKeyNames="CourseID" ItemType="CoursWeb.Models.Course" SelectMethod="GetCourses">
                <EmptyDataTemplate>
                    <table >
                        <tr>
                            <td>No result was found</td>
                        </tr>
                    </table>
                    
                </EmptyDataTemplate>
                 <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>   
                        <a href="/coursedetail?courseId=<%#:Item.CourseID %>"class="link" >
                            <div class="courseItem">
                            <img src="<%#: Item.Image %>" class="courseImg"/>
                            <h2 class="courseName"><%#: Item.CourseName %> </h2>                            
                            <span class="coursePrice"><%#: Item.Price %> VND</span>
                            </div>
                          </a>
                </ItemTemplate>
            </asp:ListView>
          </div>
        </div>
</asp:Content>
