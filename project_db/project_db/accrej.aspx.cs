using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_db
{
    public partial class accrej : System.Web.UI.Page
    {
        private void showallreqs()
        {
            String sman = (String)Session["user"];

            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String query = "SELECT cr.club_representative_name , host.club_name as hostname , guest.club_name as guestname , M.start_time , M.end_time , hr.request_status FROM Host_Request hr INNER JOIN Club_Representative cr ON hr.representative_id = cr.id INNER JOIN Matches M ON hr.match_id = M.id INNER JOIN Club host ON host.id = M.host_club_id INNER JOIN Club guest ON M.guest_club_id = guest.id INNER JOIN Stadium_Manager sm ON hr.manager_id = sm.id WHERE hr.request_status = 'unhandled' AND sm.username = '" + sman + "'";
            SqlCommand sqlcomm = new SqlCommand(query, conn);
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
            sb.Append("All Unhandled Requests");
            sb.Append("<table border=1>");
            sb.Append("<thead>");
            sb.Append("<tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                sb.Append("<th>");
                sb.Append(dc.ColumnName.ToUpper());
                sb.Append("</th>");
            }
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<br>");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append("<td>");
                    sb.Append(dr[dc.ColumnName].ToString());
                    sb.Append("</td>");
                }
                sb.Append("</tr>");
                sb.Append("<br>");
            }
            sb.Append("</table>");
            sb.Append("</center>");
            Panel1.Controls.Add(new Label { Text = sb.ToString() });
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                showallreqs();
            }
        }

        protected void accept(object sender, EventArgs e)
        {
            String sman = (String)Session["user"];
            String dtstring = Request.Form["starttimeofmatch"];
            if (dtstring.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                showallreqs();
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            DateTime starttime = DateTime.Parse(dtstring);
            String host = hostname.Text;
            String guest = guestname.Text;
            String query = "SELECT cr.club_representative_name , host.club_name as hostname , guest.club_name as guestname , M.start_time , M.end_time , hr.request_status FROM Host_Request hr INNER JOIN Club_Representative cr ON hr.representative_id = cr.id INNER JOIN Matches M ON hr.match_id = M.id INNER JOIN Club host ON host.id = M.host_club_id INNER JOIN Club guest ON M.guest_club_id = guest.id INNER JOIN Stadium_Manager sm ON hr.manager_id = sm.id WHERE hr.request_status = 'unhandled' AND sm.username = '" + sman + "'";
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();


            // Read each row and add it to the list
            Boolean flag = false;
            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();
                }
                if (host.Equals(row[1]) && guest.Equals(row[2]) && starttime.Equals(DateTime.Parse(row[3])))
                {
                    flag = true;
                    break;
                }
            }
            reader.Close();
            if (flag)
            {
                SqlCommand accept = new SqlCommand("acceptRequest" , conn);
                accept.CommandType = CommandType.StoredProcedure;
                accept.Parameters.Add(new SqlParameter("@username", sman));
                accept.Parameters.Add(new SqlParameter("@hostname", host));
                accept.Parameters.Add(new SqlParameter("@guestname", guest));
                accept.Parameters.Add(new SqlParameter("@start_time", starttime));
                accept.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('REQUEST ACCEPTED');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Request Doesnt exist or already handled');", true);
            }
            showallreqs();

        }

        protected void reject(object sender, EventArgs e)
        {
            String sman = (String)Session["user"];
            String dtstring = Request.Form["starttimeofmatch"];
            if (dtstring.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                showallreqs();
                return;
            }
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            DateTime starttime = DateTime.Parse(dtstring);
            String host = hostname.Text;
            String guest = guestname.Text;
            String query = "SELECT cr.club_representative_name , host.club_name as hostname , guest.club_name as guestname , M.start_time , M.end_time , hr.request_status FROM Host_Request hr INNER JOIN Club_Representative cr ON hr.representative_id = cr.id INNER JOIN Matches M ON hr.match_id = M.id INNER JOIN Club host ON host.id = M.host_club_id INNER JOIN Club guest ON M.guest_club_id = guest.id INNER JOIN Stadium_Manager sm ON hr.manager_id = sm.id WHERE hr.request_status = 'unhandled' AND sm.username = '" + sman + "'";
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();


            // Read each row and add it to the list
            Boolean flag = false;
            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();
                }
                if (host.Equals(row[1]) && guest.Equals(row[2]) && starttime.Equals(DateTime.Parse(row[3])))
                {
                    flag = true;
                    break;
                }
            }
            reader.Close();
            if (flag)
            {
                SqlCommand accept = new SqlCommand("rejectRequest", conn);
                accept.CommandType = CommandType.StoredProcedure;
                accept.Parameters.Add(new SqlParameter("@username", sman));
                accept.Parameters.Add(new SqlParameter("@hostname", host));
                accept.Parameters.Add(new SqlParameter("@guestname", guest));
                accept.Parameters.Add(new SqlParameter("@start_time", starttime));
                accept.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('REQUEST REJECTED');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Request Doesnt exist or already handled');", true);
            }
            showallreqs();
        }
    }
}