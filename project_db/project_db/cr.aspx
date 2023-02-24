<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cr.aspx.cs" Inherits="project_db.cr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="View Club Info" OnClick="clubinfo" />
            <br />
            <br />
            <asp:Button ID="Button2" runat="server" Text="View Upcoming Club Matches" OnClick="crupcoming" />
            <br />
            <br />
            Enter Date<br />
            <input type="datetime-local" id="time" name="time"/>
            <br />
            <asp:Button ID="Button3" runat="server" Text="Find Available Stadiums On This Date" OnClick="availableSOn" />

            <br />
            <asp:Panel ID="Panel1" runat="server" Height="16px">
            </asp:Panel>

            <asp:Button ID="Button4" runat="server" Text="Send Request" OnClick="sendreq" />


        </div>
    </form>
</body>
</html>
