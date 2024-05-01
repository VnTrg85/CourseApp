<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="CoursWeb.Signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer">
        <h1 class="loginTitle">Register your account to UCourse</h1>
        <h2 class="loginSubTitle">You already have account?<a href="/login">Login</a></h2>
        <h4 class="mb2"><b>Username</b></h4>
        <asp:TextBox runat="server" ID="Username"></asp:TextBox>
        <h4 class="mb2"><b>Password</b></h4>
        <asp:TextBox runat="server" TextMode="Password"  ID="Password"></asp:TextBox>
        <h4 class="mb2"><b>Email</b></h4>
        <asp:TextBox runat="server"  ID="Email"></asp:TextBox>
        <asp:Label runat="server" ID="Error"></asp:Label>
        <asp:LinkButton runat="server" CssClass="login-btn link" OnClick="Signup_Click">Sign up</asp:LinkButton>
     
    </div>
</asp:Content>
