using System;
using System.Collections.Generic;
using System.Data;
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
        public static void insetToDatabase(string SQLCOMMAND, string mail)
         {
             dbConnection.Open();
             //SQLiteCommand command = new SQLiteCommand(SQLCOMMAND, dbConnection);
             //command.ExecuteNonQuery();
             //dbConnection.Close();
             
             SQLiteCommand cmd = new SQLiteCommand(dbConnection);
             SQLiteParameter param = cmd.CreateParameter();

             cmd.CommandText = SQLCOMMAND;
             param.ParameterName = "param";
             param.Value = mail;
             cmd.Parameters.Add(param);
             try
             {
                 cmd.ExecuteNonQuery();
             }
             catch (Exception)
             {
                 
                 
             }
             finally { dbConnection.Close(); }
                   



         }





                }
}
