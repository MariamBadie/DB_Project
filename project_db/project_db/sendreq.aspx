<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendreq.aspx.cs" Inherits="project_db.sendreq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            username of stadium<br />
            <asp:TextBox ID="smname" runat="server"></asp:TextBox>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YOUR UPCOMING MATCHES :<br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            Enter Start Time Of The Match Using Above Table :<br />
            <input type="datetime-local" id="starttimeofmatch" name="starttimeofmatch"/>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Send Request" OnClick="Send" />
        </div>
    </form>
</body>
</html>
