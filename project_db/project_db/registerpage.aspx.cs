using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_db
{
    public partial class registerpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.Equals("Club Representative"))
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
            if (DropDownList1.SelectedValue.Equals("Stadium Manager"))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = false;
            }
            if (DropDownList1.SelectedValue.Equals("Fan"))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = true;
            }
            if (DropDownList1.SelectedValue.Equals("Sports Association Manager"))
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }

        }
        private Boolean checkuser(String username)
        {
            if (username.Equals(""))
            {
                return true;
            }
            Boolean flag = false;
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            String query = "SELECT * FROM System_Users";
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Create an array to store the values for this row
                string[] row = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[i] = reader[i].ToString();

                }
                if (username.Equals(row[0]))
                {
                    flag = true;
                    break;
                }

            }
            conn.Close();

                return flag;
        }

        protected void register(object sender, EventArgs e)
        {
            String connStr = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
            SqlConnection con = new SqlConnection(connStr);
            con.Open();
            if (Panel1.Visible)
            {
                String samu = samusername.Text;
                String sampass = sampassword.Text;
                String samn = samname.Text;
                if (checkuser(samu) || sampass.Equals("") || samn.Equals(""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID CREDENTIALS !! TRY AGAIN');", true);
                    return;
                }
                else
                {
                    SqlCommand addsam = new SqlCommand("addAssociationManager", con);
                    addsam.CommandType = System.Data.CommandType.StoredProcedure;
                    addsam.Parameters.Add(new SqlParameter("@name", samn));
                    addsam.Parameters.Add(new SqlParameter("@username", samu));
                    addsam.Parameters.Add(new SqlParameter("@password", sampass));
                    addsam.ExecuteNonQuery();
                    string message = "REGISTRATION SUCCESSFUL";
                    string redirectUrl = "LoginPage.aspx";
                    string script = "alert('" + message + "'); window.location = '" + redirectUrl + "';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                }
            }
            if (Panel2.Visible)
            {
                String cru = crusername.Text;
                String crpass = crpassword.Text;
                String crn = crname.Text;
                String club = crclubname.Text;
                if (checkuser(cru) || crpass.Equals("") || crn.Equals(""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID CREDENTIALS !! TRY AGAIN');", true);
                }
                else
                {
                    Boolean flag = false;
                    String connStr1 = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
                    SqlConnection conn = new SqlConnection(connStr1);
                    String query = "SELECT * FROM Club c LEFT OUTER JOIN Club_Representative cr ON cr.club_id = c.id";
                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create an array to store the values for this row
                        string[] row = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i].ToString();

                        }
                        if (club.Equals(row[1]) && row[3].Equals(""))
                        {
                            flag = true;
                            break;
                        }

                    }
                    conn.Close();
                    if (!flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('CLUB NOT FOUND OR ALREADY HAVE REPRESENTATIVE !! TRY AGAIN');", true);
                    }
                    else
                    {
                        SqlCommand addcr = new SqlCommand("addRepresentative", con);
                        addcr.CommandType = System.Data.CommandType.StoredProcedure;
                        addcr.Parameters.Add(new SqlParameter("@name", crn));
                        addcr.Parameters.Add(new SqlParameter("@username", cru));
                        addcr.Parameters.Add(new SqlParameter("@password", crpass));
                        addcr.Parameters.Add(new SqlParameter("@clubname", club));
                        addcr.ExecuteNonQuery();
                        string message = "REGISTRATION SUCCESSFUL";
                        string redirectUrl = "LoginPage.aspx";
                        string script = "alert('" + message + "'); window.location = '" + redirectUrl + "';";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    }
                }
            }
            if (Panel3.Visible)
            {
                String manu = manusername.Text;
                String manpass = manpassword.Text;
                String mann = manname.Text;
                String stadium = manstname.Text;
                if (checkuser(manu) || manpass.Equals("") || mann.Equals(""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID CREDENTIALS !! TRY AGAIN');", true);
                }
                else
                {
                    Boolean flag = false;
                    String connStr1 = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
                    SqlConnection conn = new SqlConnection(connStr1);
                    String query = "SELECT * FROM Stadium s LEFT OUTER JOIN Stadium_Manager sm ON s.id = sm.stadium_id";
                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create an array to store the values for this row
                        string[] row = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i].ToString();

                        }
                        if (stadium.Equals(row[1]) && row[5].Equals(""))
                        {
                            flag = true;
                            break;
                        }

                    }
                    conn.Close();
                    if (!flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('STADIUM NOT FOUND OR ALREADY HAVE MANAGER !! TRY AGAIN');", true);
                    }
                    else
                    {
                        SqlCommand addsam = new SqlCommand("addStadiumManager", con);
                        addsam.CommandType = System.Data.CommandType.StoredProcedure;
                        addsam.Parameters.Add(new SqlParameter("@name", mann));
                        addsam.Parameters.Add(new SqlParameter("@username", manu));
                        addsam.Parameters.Add(new SqlParameter("@password", manpass));
                        addsam.Parameters.Add(new SqlParameter("@sname", stadium));
                        addsam.ExecuteNonQuery();
                        string message = "REGISTRATION SUCCESSFUL";
                        string redirectUrl = "LoginPage.aspx";
                        string script = "alert('" + message + "'); window.location = '" + redirectUrl + "';";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    }
                }
            }
            if (Panel4.Visible)
            {
                String fanu = fanusername.Text;
                String fanpass = fanpassword.Text;
                String fann = fanname.Text;
                String nid = fannid.Text;
                String address = fanaddress.Text;
                if (fannumber.Text.Equals("") || address.Equals("") || nid.Equals("") || fann.Equals("") || fanpass.Equals("") )
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE ENTER VALID INFO');", true);
                    return;
                }
                int num = Int16.Parse(fannumber.Text);
                string datetimeString = Request.Form["birth"];
                if (datetimeString.Equals(""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('PLEASE CHOOSE VALID DATE');", true);
                    return;
                }
                DateTime birthdate = DateTime.Parse(datetimeString);
                if (checkuser(fanu) || fanpass.Equals(""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('INVALID CREDENTIALS !! TRY AGAIN');", true);
                }
                else
                {
                    Boolean flag = false;
                    String connStr1 = WebConfigurationManager.ConnectionStrings["project_db"].ToString();
                    SqlConnection conn = new SqlConnection(connStr1);
                    String query = "SELECT * FROM Fan";
                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Create an array to store the values for this row
                        string[] row = new string[reader.FieldCount];
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i].ToString();

                        }
                        if (nid.Equals(row[0]))
                        {
                            flag = true;
                            break;
                        }

                    }
                    conn.Close();
                    if (flag)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "alert", "alert('FAN WITH THIS NATIONAL ID NUMBER ALREADY EXISTS !! TRY AGAIN');", true);
                    }
                    else
                    {
                        SqlCommand addsam = new SqlCommand("addFan", con);
                        addsam.CommandType = System.Data.CommandType.StoredProcedure;
                        addsam.Parameters.Add(new SqlParameter("@name", fann));
                        addsam.Parameters.Add(new SqlParameter("@username", fanu));
                        addsam.Parameters.Add(new SqlParameter("@password", fanpass));
                        addsam.Parameters.Add(new SqlParameter("@address", address));
                        addsam.Parameters.Add(new SqlParameter("@phonenumber", num));
                        addsam.Parameters.Add(new SqlParameter("@birthdate", birthdate));
                        addsam.Parameters.Add(new SqlParameter("@nid", nid));



                        addsam.ExecuteNonQuery();
                        string message = "REGISTRATION SUCCESSFUL";
                        string redirectUrl = "LoginPage.aspx";
                        string script = "alert('" + message + "'); window.location = '" + redirectUrl + "';";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script, true);
                    }
                }
            }

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue.Equals("Club Representative"))
            {
                Panel1.Visible= false;
                Panel2.Visible = true;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
            if (DropDownList1.SelectedValue.Equals("Stadium Manager"))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = true;
                Panel4.Visible = false;
            }
            if (DropDownList1.SelectedValue.Equals("Fan"))
            {
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = true;
            }
            if (DropDownList1.SelectedValue.Equals("Sports Association Manager"))
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
            }
        }
    }
}