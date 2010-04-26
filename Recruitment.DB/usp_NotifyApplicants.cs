using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Net.Mail;
using System.Collections.Generic;

public partial class StoredProcedures
{
    static readonly double NumPriorNotificationDays = 7;

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_NotifyApplicants()
    {
        SqlConnection con = new SqlConnection("Context Connection=true");

        string query = "EXEC usp_GetNotificationList" + "'" + DateTime.Now.AddDays(NumPriorNotificationDays).ToShortDateString() + "'";
        
        SqlCommand cmd = new SqlCommand(query, con);

        con.Open();

        List<UserInfo> users = new List<UserInfo>();

        using (SqlDataReader rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                users.Add(new UserInfo(rdr));

                SqlContext.Pipe.Send(rdr["Email"].ToString());
            }

            //SqlContext.Pipe.Send(rdr);
        }

        
        SqlCommand sendEmail = new SqlCommand("", con);

        foreach (UserInfo user in users)
        {
            sendEmail.CommandText = string.Format("EXEC msdb.dbo.sp_send_dbmail @recipients='{0}', @subject='{1}', @body = '{2}', @body_format = '{3}'",
                                            "srkirkland@ucdavis.edu", //user.Email, 
                                            "Subject", 
                                            "Hello: " + user.FullName, 
                                            "HTML");
            sendEmail.ExecuteNonQuery();
        }
        
        con.Close();

        // Put your code here
    }

    public class UserInfo
    {
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        private string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public UserInfo(SqlDataReader rdr)
        {
            Email = rdr["Email"].ToString();
            FirstName = rdr["FirstName"].ToString();
            LastName = rdr["LastName"].ToString();
        }
    }
};
