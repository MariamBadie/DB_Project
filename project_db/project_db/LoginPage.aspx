<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="project_db.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Log In</title>
    <style>
        body{
            background-image : url("loginback.jpg")
        }
    </style>
</head>
<body>
    <center>
    <form id="form1" runat="server">
        <div>
            Username:&nbsp; &nbsp;
            <asp:TextBox ID="username" runat="server"  Placeholder="enter your username"></asp:TextBox>
        <p>
            Password:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="password" runat="server" TextMode ="Password" Placeholder="enter your password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Login" />
        </p>
        <asp:Button ID="Button2" runat="server" Text="No Account ? No Problem. Register !!" OnClick="Button2_Click" />
            </div>

            </form>
        </center>
</body>
</html>
