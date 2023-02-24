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
    public partial class crupcoming : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                String cr = (String) Session["user"];
                String sqlquery = "SELECT C.club_name AS host_club_name ,    guest.club_name AS guest_club_name ,     M.start_time , M.end_time ,  S.staduim_name    FROM Club C INNER JOIN Matches M ON (C.id = M.host_club_id)   INNER JOIN Club_Representative cr ON cr.club_id = C.id    INNER JOIN Club guest ON M.guest_club_id = guest.id    LEFT OUTER JOIN Stadium S ON M.staduim_id = S.id    WHERE cr.username = '" + cr + "' AND M.start_time > CURRENT_TIMESTAMP    UNION   SELECT host.club_name as host_club_name , C.club_name as guest_club_name ,  M.start_time , M.end_time , S.staduim_name    FROM Club C INNER JOIN Matches M ON (C.id = M.guest_club_id)     INNER JOIN Club host ON (host.id = M.host_club_id)    INNER JOIN Club_Representative cr ON cr.club_id = C.id    LEFT OUTER JOIN Stadium S ON M.staduim_id = S.id    WHERE cr.username = '" + cr + "'  AND M.start_time > CURRENT_TIMESTAMP";
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
        }
    }
}