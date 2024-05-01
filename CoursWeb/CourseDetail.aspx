<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseDetail.aspx.cs" Inherits="CoursWeb.CourseDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FormView ID="courseDetail" runat="server" ItemType="CoursWeb.Models.Course" SelectMethod ="GetCourse" RenderOuterTable="false">
        <ItemTemplate>
            <div class="courseDetail">
                <div class="detailTop">
                    <div class="nameanddesc">
                        <h1 class="courseDetailName"><%#:Item.CourseName %></h1>
                        <p class="courseDesc"><%#:Item.Description %></p>
                        <span class="coursePrice">Price: <%#:Item.Price%> VND</span>
                    </div>
                    <img src="<%#: Item.Image %>"/>
                </div>
                             
            </div>
        </ItemTemplate>

    </asp:FormView>
    <div class="lessonHeader">Course content</div>
    <div class="detailBottom">
        <div class="lessonContainer">
            <asp:ListView ID="lessonList" runat="server" DataKeyNames="LessonID" ItemType="CoursWeb.Models.Lesson" SelectMethod="GetLessons">
                   <ItemTemplate>
                        <div class="lessonItem">
                            <div class="lessonName"><%#:Item.LessonName%></div>
                            <div class="lessonDesc"><%#:Item.Description%></div>
                        </div>
                   </ItemTemplate>
            </asp:ListView> 
        </div>
        <div class="buttons">
            <button class="addCartBtn">Add to cart</button>
            <button class="buyBtn">Buy now</button>

        </div>
    </div>
     
</asp:Content>
