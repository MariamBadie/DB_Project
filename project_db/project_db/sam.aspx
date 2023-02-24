<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sam.aspx.cs" Inherits="project_db.sam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            name of host<br />
            <asp:TextBox ID="hostadded" runat="server"></asp:TextBox>
            <br />
            name of guest<br />
            <asp:TextBox ID="guestadded" runat="server"></asp:TextBox>
        <p>
            start_time</p>
        <input type="datetime-local" id="starttimeofadded" name="starttimeofadded"/><br />
        end time<br />
        <input type="datetime-local" id="endtimeofadded" name="endtimeofadded"/>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add Match" OnClick ="addmatch" />
        <br />
            <br />
            <br />
            host name to delete<br />
            <asp:TextBox ID="deletehostname" runat="server"></asp:TextBox>
            <br />
            guest name to delete<br />
            <asp:TextBox ID="deleteguestname" runat="server"></asp:TextBox>
            <br />
            start time to delete<br />
            <br /><input type="datetime-local" id="starttimeofdelete" name="starttimeofdelete"/>
            <br />
            end time to delete<br />
            <input type="datetime-local" id="endtimeofdelete" name="endtimeofdelete"/>
            <br />
            <asp:Button ID="Button2" runat="server" Text="Delete Match" OnClick="deleteMatch" />
            <br />
            <br />
            <asp:Button ID="Button4" runat="server" Text="Show Upcoming Matches" OnClick="upcoming" />
            <br />
            <br />
            <asp:Button ID="Button5" runat="server" Text="Already Played Match" OnClick="prev" />
            <br />
            <br />
            <asp:Button ID="Button3" runat="server" Text="Show clubs never matched" OnClick="nevermatched" />
        <br>
       </div>

    </form>
</body>
</html>
