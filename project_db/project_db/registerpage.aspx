<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registerpage.aspx.cs" Inherits="project_db.registerpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration</title>
    <style>
        body {
            background-image: url("regimage.jpg");
            color: white
        }
    </style>
</head>
<body>
    <center>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" >
                <asp:ListItem>Sports Association Manager</asp:ListItem>
                <asp:ListItem>Club Representative</asp:ListItem>
                <asp:ListItem>Stadium Manager</asp:ListItem>
                <asp:ListItem>Fan</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Panel ID="Panel1" runat="server">
                name<br />
                <asp:TextBox ID="samname" runat="server"></asp:TextBox>
                <br />

                username<br />
                &nbsp;<asp:TextBox ID="samusername" runat="server"></asp:TextBox>
                &nbsp;<br /> password<br />
                <asp:TextBox ID="sampassword" runat="server" TextMode ="Password"></asp:TextBox>
                <br />

            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server">
                name<br />
                <asp:TextBox ID="crname" runat="server"></asp:TextBox>
                <br />
                username<br />
                <asp:TextBox ID="crusername" runat="server"></asp:TextBox>
                <br />
                password<br />
                <asp:TextBox ID="crpassword" runat="server" TextMode ="Password"></asp:TextBox>
                <br />
                club name
                <br />
                <asp:TextBox ID="crclubname" runat="server"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server">
                name<br />
                <asp:TextBox ID="manname" runat="server"></asp:TextBox>
                <br />
                username<br />
                <asp:TextBox ID="manusername" runat="server"></asp:TextBox>
                <br />
                password<br />
                <asp:TextBox ID="manpassword" runat="server" TextMode ="Password"></asp:TextBox>
                <br />
                stadium name<br />
                <asp:TextBox ID="manstname" runat="server"></asp:TextBox>

            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server">
                name<br />
                <asp:TextBox ID="fanname" runat="server"></asp:TextBox>
                <br />
                username<br />
                <asp:TextBox ID="fanusername" runat="server"></asp:TextBox>
                <br />
                password<br />
                <asp:TextBox ID="fanpassword" runat="server" TextMode ="Password"></asp:TextBox>
                <br />
                national id number<br />
                <asp:TextBox ID="fannid" runat="server"></asp:TextBox>
                <br />
                phone number<br />
                <asp:TextBox ID="fannumber" runat="server"></asp:TextBox>
                <br />
                birth date<br />
                <input id="birth" name="birth" type="datetime-local" />
                <br />
                address<br />
                <asp:TextBox ID="fanaddress" runat="server"></asp:TextBox>
                <br />
            </asp:Panel>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Register New User" OnClick="register" />
        </div>
    </form>
        </center>
</body>
</html>
