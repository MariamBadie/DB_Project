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
using System.Xml.Linq;

namespace project_db
{
    public partial class sendreq : System.Web.UI.Page
    {
        private void viewall()
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String user = (String)Session["user"];
            String sqlquery = "SELECT host.club_name as hostname , guest.club_name as guestname, M.start_time , M.end_time FROM Matches M Inner Join Club host On M.host_club_id = host.id INNER JOIN Club guest ON M.guest_club_id = guest.id INNER JOIN Club_Representative cr ON host.id = cr.club_id LEFT OUTER JOIN Host_Request hr ON M.id = hr.match_id WHERE cr.username = '" + user + "' AND M.start_time > CURRENT_TIMESTAMP AND hr.match_id IS NULL AND M.staduim_id IS NULL";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, conn);
            //conn.Open();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                viewall();
            }
        }

        protected void Send(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            string datetimeString = Request.Form["starttimeofmatch"];
            if (datetimeString.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                viewall();  
                return;
            }
            DateTime starttime = DateTime.Parse(datetimeString);

            String smusername = smname.Text;
            Boolean flag = false;
            String query = "SELECT * FROM Stadium";
            SqlCommand comm = new SqlCommand(query, conn);





            String cr = (String)Session["user"];
            int cid = -1;

            String cname = "";

            conn.Open();

            String query0 = "SELECT * FROM Club_Representative cr INNER JOIN Club c On cr.club_id = c.id";
            SqlCommand comm0 = new SqlCommand(query0, conn);

            SqlDataReader reader0 = comm0.ExecuteReader();


            // Read each row and add it to the list
            while (reader0.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader0.FieldCount];
                for (int i = 0; i < reader0.FieldCount; i++)
                {
                    row[i] = reader0[i].ToString();
                }
                if (cr.Equals(row[3]))
                {
                    cname = row[5];
                    cid = Int16.Parse(row[2]);
                }

                // Add the row to the list

            }
            reader0.Close();

            SqlDataReader reader = comm.ExecuteReader();


            // Read each row and add it to the list
            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();
                }
                if (smusername.Equals(row[1]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();

            String query2 = "SELECT * FROM Matches";
            SqlCommand comm2 = new SqlCommand(query2, conn);

            SqlDataReader reader2 = comm2.ExecuteReader();
            Boolean flag2 = false;
            Boolean flag3 = false;
            // Read each row and add it to the list
            while (reader2.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader2.FieldCount];
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row[i] = reader2[i].ToString();
                }
                if (starttime.Equals(DateTime.Parse(row[1])))
                {
                    if (cid == Int16.Parse(row[3]) && (row[5].Equals("")))
                    {
                        flag2 = true;
                        break;
                    }
                }

                // Add the row to the list

            }
            reader2.Close();

            String query3 = "SELECT * FROM Matches m INNER JOIN Host_Request hr ON m.id = hr.match_id ";
            SqlCommand comm3 = new SqlCommand(query3, conn);

            SqlDataReader reader3 = comm3.ExecuteReader();

            while (reader3.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader3.FieldCount];
                for (int i = 0; i < reader3.FieldCount; i++)
                {
                    row[i] = reader3[i].ToString();
                }
                if (starttime.Equals(DateTime.Parse(row[1])))
                {
                    if (cid == Int16.Parse(row[3]))
                    {
                        flag3 = true;
                        break;
                    }
                }

                // Add the row to the list

            }
            reader3.Close();

            if (flag && flag2 && !flag3)
            {
                SqlCommand command = new SqlCommand("addHostRequest", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@clubname", cname));
                command.Parameters.Add(new SqlParameter("@sname", smusername));
                command.Parameters.Add(new SqlParameter("@start_time", starttime));
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
            else
            {
                Response.Write("WRONG CREDITS OR REQUEST ALREADY SENT");
            }
            viewall();

        }
    }
}