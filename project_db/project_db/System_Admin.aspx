<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="System_Admin.aspx.cs" Inherits="project_db.System_Admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System Admin</title>
    <style>
        body{
            background-image : url("saback.jpg");
            color : white
        }
    </style>
</head>
<body>
    <center>
    <form id="form1" runat="server">
        <div>
            name of new club</div>
        <p>
            <asp:TextBox ID="addedclubname" runat="server"></asp:TextBox>
            </p>
        <p>
location of new club&nbsp; </p>
        <p>
            <asp:TextBox ID="addedclublocation" runat="server"></asp:TextBox>
            </p>
        <p>
            <asp:Button ID="Button2" runat="server" Text="AddClub" OnClick="add_club" />
        </p>
        <p>
            &nbsp;</p>
        <p>
        </p>
        <p>
            name of club to delete</p>
        <p>
            <asp:TextBox ID="deletedclubname" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button3" runat="server" Text="DeleteClub" OnClick="deleteclub" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            name of stadium</p>
        <p>
            <asp:TextBox ID="addedstadiumname" runat="server"></asp:TextBox>
        </p>
        <p>
            location of stadium</p>
        <p>
            <asp:TextBox ID="addedstadiumlocation" runat="server"></asp:TextBox>
        </p>
        <p>
            capacity</p>
        <p>
            <asp:TextBox ID="addedstadiumcapacity" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button4" runat="server" Text="Add New Stadium" OnClick="addstadium" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            name of stadium to delete</p>
        <p>
            <asp:TextBox ID="deletedstadiumname" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button5" runat="server" Text="Delete Stadium" OnClick="deleteStadium" />
        </p>
        <p>
            &nbsp;</p>
        <p>
            National id number of fan to block</p>
        <p>
            <asp:TextBox ID="blockedfan" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button6" runat="server" Text="Block Fan" OnClick="blockfan" />
        </p>
        <p>
            &nbsp;</p>
    </form>
        </center>
</body>
</html>
