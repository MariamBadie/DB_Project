using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace project_db
{
    public partial class sam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void addmatch(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String hostname = hostadded.Text;
            String guestname = guestadded.Text;
            string datetimeString = Request.Form["starttimeofadded"];
            if (datetimeString.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime starttime = DateTime.Parse(datetimeString);

            string endtimeofdate = Request.Form["endtimeofadded"];
            if (endtimeofdate.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime endtime = DateTime.Parse(endtimeofdate);

            SqlCommand command = new SqlCommand("addNewMatch", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name_host", hostname));
            command.Parameters.Add(new SqlParameter("@name_guest", guestname));
            command.Parameters.Add(new SqlParameter("@start_time", starttime));
            command.Parameters.Add(new SqlParameter("@end_time", endtime));
            String query = "SELECT M.start_time , M.end_time , host.club_name as hostname , guest.club_name as guestname FROM Matches M INNER JOIN Club host On M.host_club_id = host.id INNER JOIN Club guest On guest.id = M.guest_club_id";
            SqlCommand comm = new SqlCommand(query, conn);

            String query2 = "SELECT * FROM Club";
            SqlCommand comm2 = new SqlCommand(query2, conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();

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
                if (starttime.Equals(DateTime.Parse(row[0])) && endtime.Equals(DateTime.Parse(row[1])) 
                    && hostname.Equals(row[2]) && guestname.Equals(row[3]) )
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            SqlDataReader reader2 = comm2.ExecuteReader();

            Boolean hostfound = false;
            Boolean guestfound = false;
            while (reader2.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader2.FieldCount];
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row[i] = reader2[i].ToString();
                }
                if (row[1].Equals(hostname))
                {
                    hostfound = true;
                }
                if (row[1].Equals(guestname))
                {
                    guestfound = true;
                }
                if (hostfound && guestfound)
                {
                    break;
                }

                // Add the row to the list

            }
            reader2.Close();
            if (!hostfound || !guestfound)
            {
                Response.Write("You Entered Wrong Club Names");
            }
            else if (flag)
            {
                Response.Write("Match With Same Info ALREADY EXISTS");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }



        }

        // ask
        protected void deleteMatch(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String hostname = deletehostname.Text;
            String guestname = deleteguestname.Text;
            string datetimeString = Request.Form["starttimeofdelete"];
            if (datetimeString.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime starttime = DateTime.Parse(datetimeString);

            string endtimeofdate = Request.Form["endtimeofdelete"];
            if (endtimeofdate.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime endtime = DateTime.Parse(endtimeofdate);

            SqlCommand command = new SqlCommand("deleteMatch", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@host_name", hostname));
            command.Parameters.Add(new SqlParameter("@guest_name", guestname));
           // command.Parameters.Add(new SqlParameter("@start_time", starttime));
           // command.Parameters.Add(new SqlParameter("@end_time", endtime));
            String query = "SELECT M.start_time , M.end_time , host.club_name as hostname , guest.club_name as guestname FROM Matches M INNER JOIN Club host On M.host_club_id = host.id INNER JOIN Club guest On guest.id = M.guest_club_id";
            SqlCommand comm = new SqlCommand(query, conn);

            String query2 = "SELECT * FROM Club";
            SqlCommand comm2 = new SqlCommand(query2, conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();

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
                if (starttime.Equals(DateTime.Parse(row[0])) && endtime.Equals(DateTime.Parse(row[1]))
                    && hostname.Equals(row[2]) && guestname.Equals(row[3]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            SqlDataReader reader2 = comm2.ExecuteReader();

            Boolean hostfound = false;
            Boolean guestfound = false;
            while (reader2.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader2.FieldCount];
                for (int i = 0; i < reader2.FieldCount; i++)
                {
                    row[i] = reader2[i].ToString();
                }
                if (row[1].Equals(hostname))
                {
                    hostfound = true;
                }
                if (row[1].Equals(guestname))
                {
                    guestfound = true;
                }
                if (hostfound && guestfound)
                {
                    break;
                }

                // Add the row to the list

            }
            reader2.Close();
            if (!hostfound || !guestfound)
            {
                Response.Write("You Entered Wrong Club Names");
            }
            else if (!flag)
            {
                Response.Write("Match Not Found");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }

        }

        protected void nevermatched(object sender, EventArgs e)
        {
            Response.Redirect("clubsnevermatched.aspx");
        }

        protected void upcoming(object sender, EventArgs e)
        {
            Response.Redirect("upcomingmatches.aspx");

        }

        protected void prev(object sender, EventArgs e)
        {
            Response.Redirect("prev.aspx");

        }
    }
}