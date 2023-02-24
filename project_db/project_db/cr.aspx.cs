using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_db
{
    public partial class cr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void clubinfo(object sender, EventArgs e)
        {
            Response.Redirect("clubinfo.aspx");
        }

        protected void crupcoming(object sender, EventArgs e)
        {
            Response.Redirect("crupcoming.aspx");
        }

        protected void availableSOn(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            string datetimeString = Request.Form["time"];
            if (datetimeString.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                return;
            }
            DateTime time = DateTime.Parse(datetimeString);
            String sqlquery = "    SELECT DISTINCT S.staduim_name , S.staduim_location , S.capacity FROM Stadium S LEFT OUTER JOIN Matches M ON s.id = M.staduim_id WHERE S.staduim_status = '1' AND NOT EXISTS( SELECT * FROM Matches M2 WHERE(M2.start_time <= '" +time + "' AND M2.end_time >= '" + time + "') AND M2.staduim_id = S.id)";
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

        protected void sendreq(object sender, EventArgs e)
        {
            Response.Redirect("sendreq.aspx");
        }
    }
}