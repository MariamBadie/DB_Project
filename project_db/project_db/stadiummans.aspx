<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stadiummans.aspx.cs" Inherits="project_db.stadiummans" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Show Stadium Info" OnClick="stadinfo" />
            <br />
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <br />
            <asp:Button ID="Button2" runat="server" Text="View All Host Requests" OnClick="allhrs" />
            <br />
            <asp:Panel ID="Panel2" runat="server">
            </asp:Panel>
            <asp:Button ID="Button3" runat="server" Text="Accept Or Reject Requests" OnClick="accrej" />
            <br />
        </div>
    </form>
</body>
</html>
