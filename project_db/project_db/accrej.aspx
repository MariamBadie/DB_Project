<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="accrej.aspx.cs" Inherits="project_db.accrej" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
            <center>
                Host club name
                <asp:TextBox ID="hostname" runat="server"></asp:TextBox>
                <br />
                Guest club name
                <asp:TextBox ID="guestname" runat="server"></asp:TextBox>
                <br />
                Match start time
                <input id="starttimeofmatch" name="starttimeofmatch" type="datetime-local" />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Accept Request" OnClick="accept" />
                <asp:Button ID="Button2" runat="server" Text="Reject Request" OnClick="reject"/>
            </center>
        </div>
    </form>
</body>
</html>
