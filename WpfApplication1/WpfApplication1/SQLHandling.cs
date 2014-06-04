using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    class SQLHandling
    {
        static SQLiteConnection dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
         public static void CreateDatabase()
        {
            #region create non existing database
            if (!System.IO.File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            }
            #endregion

            // open connection
            dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            #region create non existing table
            bool exists = false;
            DataTable dt = dbConnection.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string tablename = (string)row[2];
                if (tablename == "MailList") exists = true;
            }

            if (!exists)
            {
                string sql = "CREATE TABLE MailList(MessageId varchar(50) not null, Receiver varchar(50), Sender varchar(50), Date varchar(20), Subject varchar(60), Message blob, UNIQUE(MessageId))";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            #endregion

            dbConnection.Close();
        }

        //Takes SQLCOMMAND as parameter and run the command aginst the Database.
        public static void insetToDatabase( Message mail)
         {
             dbConnection.Open();
             //SQLiteCommand command = new SQLiteCommand(SQLCOMMAND, dbConnection);
             //command.ExecuteNonQuery();
             //dbConnection.Close();
             
             SQLiteCommand cmd = new SQLiteCommand(dbConnection);
             SQLiteParameter param = cmd.CreateParameter();

             string SQLCOMMAND = @"INSERT INTO MailList (MessageId, Receiver, Sender, Date, Subject, Message) VALUES ('" + mail.Headers.MessageId + "','" + mail.Headers.To + "','" + mail.Headers.From + "','" + mail.Headers.DateSent + "','" + mail.Headers.Subject + "', @param)";


             cmd.CommandText = SQLCOMMAND;
             param.ParameterName = "param";
             MessagePart html = mail.FindFirstHtmlVersion();
             MessagePart plainText = mail.FindFirstPlainTextVersion();
            StringBuilder builder = new StringBuilder();

            if (html != null)
                param.Value = builder.Append(html.GetBodyAsText()).ToString();


            else
                param.Value = plainText.GetBodyAsText();
             
             cmd.Parameters.Add(param);
             try
             {
                 cmd.ExecuteNonQuery();
             }
             catch (Exception)
             {
                 
                 
             }
                 //always close the connection!!
             finally { dbConnection.Close(); }

         }

        public static List<MailObjects> readMails()
        {

            List<MailObjects> mailList = new List<MailObjects>();
            
            SQLiteCommand cmd = new SQLiteCommand(dbConnection);
            
            cmd.CommandText = "SELECT * FROM MailList";

            try 
	{
        dbConnection.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();
                
                while (reader.Read())
	{
        MailObjects nextMail = new MailObjects(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
	         mailList.Add(nextMail);
	}
                reader.Close();
		
	}
	catch (Exception)
	{
		
		throw;
	}
            finally{
                dbConnection.Close();
            }
                


                
            return mailList;
        }
            





   }
}
