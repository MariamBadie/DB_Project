using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace project_db
{
    public partial class System_Admin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void add_club(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = addedclubname.Text;
            String location = addedclublocation.Text;
            if (name.Equals("") || location.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID INFO !! TRY AGAIN');", true);
                return;
            }

            SqlCommand command = new SqlCommand("addClub", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@location", location));
            String query = "SELECT * FROM Club";
            SqlCommand comm = new SqlCommand(query, conn);

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
                if (name.Equals(row[1]) && location.Equals(row[2]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            if (flag)
            {
                Response.Write("CLUB ALREADY EXISTS");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
        }

        protected void deleteclub(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = deletedclubname.Text;


            SqlCommand command = new SqlCommand("deleteClub", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name", name));
            String query = "SELECT * FROM Club";
            SqlCommand comm = new SqlCommand(query, conn);

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
                if (name.Equals(row[1]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            if (!flag)
            {
                Response.Write("Club Does Not Exist !!");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
        }

        protected void addstadium(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = addedstadiumname.Text;
            String location = addedstadiumlocation.Text;
            if (name.Equals("") || location.Equals("") || addedstadiumcapacity.Text.Equals(""))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID INFO !! TRY AGAIN');", true);
                return;
            }
            int capacity = Int16.Parse(addedstadiumcapacity.Text);
            

            SqlCommand command = new SqlCommand("addStadium", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name", name));
            command.Parameters.Add(new SqlParameter("@location", location));
            command.Parameters.Add(new SqlParameter("@capacity", capacity));

            String query = "SELECT * FROM Stadium";
            SqlCommand comm = new SqlCommand(query, conn);

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
                if (name.Equals(row[1]) && location.Equals(row[2]) 
                    && capacity == Int16.Parse(row[3]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            if (flag)
            {
                Response.Write("Stadium Exists !!!");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
        }

        protected void deleteStadium(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = deletedstadiumname.Text;


            SqlCommand command = new SqlCommand("deleteStadium", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@name", name));
            String query = "SELECT * FROM Stadium";
            SqlCommand comm = new SqlCommand(query, conn);

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
                if (name.Equals(row[1]))
                {
                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            if (!flag)
            {
                Response.Write("Stadium Does Not Exist !!");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
        }

        protected void blockfan(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String name = blockedfan.Text;


            SqlCommand command = new SqlCommand("blockFan", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@nid", name));
            String query = "SELECT * FROM Fan";
            SqlCommand comm = new SqlCommand(query, conn);

            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();


            // Read each row and add it to the list
            Boolean flag = false;
            int st = -1;
            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();
                }
                if (name.Equals(row[0]))
                {
                    if (row[5].Equals("True"))
                    {
                        st = 1;
                    }
                    else
                    {
                        st = 0;
                    }

                    flag = true;
                    break;
                }

                // Add the row to the list

            }
            reader.Close();
            if (!flag)
            {
                Response.Write("Fan Does Not Exist !!!");
            }
            else if (st == 0)
            {
                Response.Write("Fan Already Blocked !!!");
            }
            else
            {
                command.ExecuteNonQuery();
                Response.Write("DONE");
            }
        }
    }
}