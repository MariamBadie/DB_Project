using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_db
{
    public partial class fan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void viewMatches(object sender, EventArgs e)
        {
            string datetimeString = Request.Form["starttimeofmatch"];
            if (datetimeString.Equals("")){
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime starttime = DateTime.Parse(datetimeString);
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String sqlquery = "SELECT DISTINCT host.club_name AS host_club_name , guest.club_name AS guest_club_name , S.staduim_name , S.staduim_location FROM Matches M INNER JOIN Ticket T ON M.id = T.match_id INNER JOIN Club host ON host.id = M.host_club_id INNER JOIN Club guest ON guest.id = M.guest_club_id INNER JOIN Stadium s ON s.id = M.staduim_id WHERE T.ticket_status = 1 AND M.start_time >='" + starttime + "'";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, conn);
            conn.Open();

            SqlDataAdapter sda = new SqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            StringBuilder sb = new StringBuilder();
            sb.Append("<center>");
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

        protected void purchaseticket(object sender, EventArgs e)
        {
            string datetimeString = Request.Form["starttimeofmatchtoattend"];
            if (datetimeString.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime starttime = DateTime.Parse(datetimeString);
            String fanname = (String) Session["user"];

            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String host = hostname.Text;
            String guest = guestname.Text;
            Boolean matchfound = false;

            conn.Open();
            String query = "SELECT * FROM Matches M INNER JOIN Club host ON M.host_club_id = host.id INNER JOIN Club guest ON M.guest_club_id = guest.id INNER JOIN Ticket t ON t.match_id = M.id";
            SqlCommand command = new SqlCommand(query, conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();

                }
                if (host.Equals(row[7]) && guest.Equals(row[10]))
                {
                    if (row[13].Equals("True") && starttime.Equals(DateTime.Parse(row[1])))
                    {
                        matchfound = true;
                        break;
                    }
                }

            }

            reader.Close();

            String nid = "";
            String fan = "SELECT * FROM Fan";
            SqlCommand command2 = new SqlCommand(fan, conn);
            SqlDataReader reader2 = command2.ExecuteReader();
           // Response.Write(fanname);
            while (reader2.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader2.FieldCount];
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row[i] = reader2[i].ToString();

                }
                //Response.Write(fanname);
                if (row[6].Equals(fanname)) 
                {
                    nid = row[0];
                }

            }

            reader2.Close();

            if (matchfound ) {
                //Response.Write(nid);
                SqlCommand com = new SqlCommand("purchaseTicket", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add(new SqlParameter("@nid", nid));
                com.Parameters.Add(new SqlParameter("@hostname", host));
                com.Parameters.Add(new SqlParameter("@guestname", guest));
                com.Parameters.Add(new SqlParameter("@start_time", starttime));
                com.ExecuteNonQuery();
                Response.Write("DONE");

            }
            else
            {
                Response.Write("Match Not Found OR NO AVAILABLE TICKETS");
            }


        }
    }
}