using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void usp_NotifyApplicants()
    {
        SqlConnection con = new SqlConnection("Context Connection=true");

        string query = "EXEC usp_GetNotificationList";
        
        SqlCommand cmd = new SqlCommand(query, con);

        con.Open();

        using (SqlDataReader rdr = cmd.ExecuteReader())
        {
            while (rdr.Read())
            {
                SqlContext.Pipe.Send(rdr["Email"].ToString());
            }

            //SqlContext.Pipe.Send(rdr);
        }

        con.Close();

        // Put your code here
    }
};
