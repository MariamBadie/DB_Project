<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fan.aspx.cs" Inherits="project_db.fan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Time
            <br />
            <input id="starttimeofmatch" name="starttimeofmatch" type="datetime-local" />
            <br />
            <asp:Button ID="Button1" runat="server" Text="View Matches To Attend" OnClick="viewMatches" />
            <br />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            Host name of match<br />
            <asp:TextBox ID="hostname" runat="server"></asp:TextBox>
            <br />
            Guest name of match<br />
            <asp:TextBox ID="guestname" runat="server"></asp:TextBox>
            <br />
            Match Starting time<br />
            <br />
            <input id="starttimeofmatchtoattend" name="starttimeofmatchtoattend" type="datetime-local" />
            <br />
            <asp:Button ID="Button2" runat="server" Text="Purchase Ticket For This Match" OnClick="purchaseticket" />
        </div>
    </form>
</body>
</html>
