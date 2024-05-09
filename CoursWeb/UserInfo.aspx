<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="CoursWeb.UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="userContainer">
        <div class="userLeft">
            <asp:LinkButton runat="server" onclick="Cart_Click" CssClass="btn-User link">Cart</asp:LinkButton>
            <asp:LinkButton runat="server" onclick="Info_Click" CssClass="btn-User link">Info</asp:LinkButton>
            <asp:LinkButton runat="server" onclick="Logout_Click" CssClass="btn-User link">Log out</asp:LinkButton>
        </div>
        <div class="userRight">
            <%if (Convert.ToBoolean(Session["cart"]))
            {%>
                <asp:Gridview ID="gvEnrols" runat="server" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="<div style=\width:700px;height:50px;background:#f1f3f4;text-align:center;font-weight:600;align-content:center;color:black;font-size:18px;\>Empty Cart</div>" >
                    <Columns>
                        
                        <asp:BoundField DataField="CourseID" HeaderText="Course ID">
                            <HeaderStyle CssClass="header-table"/>
                            <ItemStyle CssClass="item-table" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CourseName" HeaderText="Course Name">
                            <HeaderStyle CssClass="header-table"/>
                            <ItemStyle CssClass="item-table" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SL" HeaderText="Quantity">
                            <HeaderStyle CssClass="header-table" Width="100px" />
                            <ItemStyle CssClass="item-table" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Price">
                            <HeaderStyle CssClass="header-table"  Width="100px"  />
                            <ItemStyle CssClass="item-table" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tongtien" HeaderText="Total">
                            <HeaderStyle CssClass="header-table"  Width="120px"/>
                            <ItemStyle CssClass="item-table" />
                        </asp:BoundField>      
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="delCourse" Text="Del" CommandArgument=<%#Container.DataItemIndex%> runat="server" OnClick ="Del_Click"/>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    
                </asp:Gridview>
            <%}%>
            <%if (Convert.ToBoolean(Session["Info"]))
            {%>                                                           
                            <div class="change-pass">
                                <asp:Label runat="server" CssClass="pass-change-label">Old password</asp:Label>
                                <asp:TextBox ID="OldPass" runat="server"  CssClass="pass-change-input"></asp:TextBox>
                                <asp:Label runat="server" CssClass="pass-change-label">New password</asp:Label>
                                <asp:TextBox ID="NewPass"  runat="server" CssClass="pass-change-input"></asp:TextBox>
                                <asp:Label runat="server" CssClass="pass-change-label">Cofirm password</asp:Label>
                                <asp:TextBox ID="ConfPass"  runat="server" CssClass="pass-change-input"></asp:TextBox> 
                                <asp:Label runat="server" ID="errMsg"></asp:Label>
                                <asp:LinkButton runat="server" ID="btnChangePass" CssClass="btnChange link" OnClick="Change_Password_Click" >Change password</asp:LinkButton>
                            </div>                                     
            <%}%>
        </div>
    </div>
</asp:Content>
