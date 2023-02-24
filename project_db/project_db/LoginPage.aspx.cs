using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_db
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            
            String allUsers = "SELECT * FROM System_Users";
            String user = username.Text;
            String pass = password.Text;
            SqlCommand command = new SqlCommand(allUsers, conn);
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
                if (user.Equals(row[0]) && pass.Equals(row[1])){
                    flag = true;
                    break;
                }

                // Add the row to the list
                
            }
            if (flag)
            {
                Session["user"] = user;
            }
            else
            {
                Response.Write("WRONG INFO ENTERED");

            }
            reader.Close();
            Boolean admin = false;
            Boolean sam = false;
            Boolean cr = false;
            Boolean stadiumman = false;
            Boolean fan = false;

            String admins = "SELECT * FROM System_Admin";
            SqlCommand commanda = new SqlCommand(admins, conn);

            String sams = "SELECT * FROM Sports_Association_Manager";
            SqlCommand commandsams = new SqlCommand(sams, conn);


            String crs = "SELECT * FROM Club_Representative";
            SqlCommand commandcrs = new SqlCommand(crs, conn);

            String stadiummans = "SELECT * FROM Stadium_Manager";
            SqlCommand commandstadiummans = new SqlCommand(stadiummans, conn);


            String fans = "SELECT * FROM Fan";
            SqlCommand commandfans = new SqlCommand(fans, conn);

            SqlDataReader readeradmins = commanda.ExecuteReader();

            Boolean flagadmin = false;
            while (readeradmins.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[readeradmins.FieldCount];
                for (int i = 0; i < readeradmins.FieldCount; i++)
                {
                    row[i] = readeradmins[i].ToString();

                }
                if (user.Equals(row[2]))
                {
                    flagadmin = true;
                    break;
                }


            }
            if (flagadmin && flag)
            {
                Response.Redirect("System_Admin.aspx");
            }
            readeradmins.Close();

            SqlDataReader readersams = commandsams.ExecuteReader();

            Boolean flagsam = false;
            while (readersams.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[readersams.FieldCount];
                for (int i = 0; i < readersams.FieldCount; i++)
                {
                    row[i] = readersams[i].ToString();

                }
                if (user.Equals(row[2]))
                {
                    flagsam = true;
                    break;
                }

            }
            if (flagsam && flag)
            {
                Response.Redirect("sam.aspx");
            }
            readersams.Close();

            SqlDataReader readercrs = commandcrs.ExecuteReader();

            Boolean flagcrs = false;
            while (readercrs.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[readercrs.FieldCount];
                for (int i = 0; i < readercrs.FieldCount; i++)
                {
                    row[i] = readercrs[i].ToString();

                }
                if (user.Equals(row[3]))
                {
                    flagcrs = true;
                    break;
                }

            }
            if (flagcrs && flag)
            {
                Response.Redirect("cr.aspx");
            }
            readercrs.Close();

            SqlDataReader readerstadiummans = commandstadiummans.ExecuteReader();

            Boolean flagstadiummans = false;
            while (readerstadiummans.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[readerstadiummans.FieldCount];
                for (int i = 0; i < readerstadiummans.FieldCount; i++)
                {
                    row[i] = readerstadiummans[i].ToString();

                }
                if (user.Equals(row[3]))
                {
                    flagstadiummans = true;
                    break;
                }

            }
            if (flagstadiummans && flag)
            {
                Response.Redirect("stadiummans.aspx");
            }
            readerstadiummans.Close();


            SqlDataReader readerfans = commandfans.ExecuteReader();

            Boolean flagfans = false;
            while (readerfans.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[readerfans.FieldCount];
                for (int i = 0; i < readerfans.FieldCount; i++)
                {
                    row[i] = readerfans[i].ToString();

                }
                if (user.Equals(row[6]) && row[5].Equals("True"))
                {
                    flagfans = true;
                    break;
                }

            }
            if (flagfans && flag)
            {
                Response.Redirect("fan.aspx");
            }
            else
            {
                Response.Write("WRONG INFO OR FAN BLOCKED");
            }
            readerfans.Close();


            conn.Close();
            }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("registerpage.aspx");
        }


    }
}