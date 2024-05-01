<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CoursWeb.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="loginContainer">
        <h1 class="loginTitle">Log in to your UCourse account</h1>
        <h2 class="loginSubTitle">You don't have any account?<a href="/signup">Signup</a></h2>
        
        <h4><b>Username</b></h4>
        <asp:TextBox runat="server" ID="Username"></asp:TextBox>
        <h4><b>Password</b></h4>
        <asp:TextBox runat="server" ID="Password" TextMode="Password"></asp:TextBox>
        
        <asp:Label runat="server" ID="statusMessage"></asp:Label>
        <asp:LinkButton class="login-btn link" onclick="btnLogin_Click" runat="server">Login</asp:LinkButton>
    </div>
</asp:Content>
