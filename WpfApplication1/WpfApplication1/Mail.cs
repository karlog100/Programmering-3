using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop;
using OpenPop.Mime;
using OpenPop.Pop3;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Threading;
using System.Data.SQLite;

namespace MailClient
{
    class Mail
    {
        /// <summary>
        /// Example showing:
        ///  - how to fetch all messages from a POP3 server
        /// </summary>
        /// <param name="hostname">Hostname of the server. For example: pop3.live.com</param>
        /// <param name="port">Host port to connect to. Normally: 110 for plain POP3, 995 for SSL POP3</param>
        /// <param name="useSsl">Whether or not to use SSL to connect to server</param>
        /// <param name="username">Username of the user on the server</param>
        /// <param name="password">Password of the user on the server</param>
        /// <returns>All Messages on the POP3 server</returns>        
        // Downloading all mails...
        public void FetchAllMessages()
        {
            try
            {


            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(Properties.Settings.
                    Default.POPServer, Properties.Settings.Default.POPPort, Properties.Settings.Default.SSL);

                // Authenticate ourselves towards the server
                client.Authenticate(Properties.Settings.Default.UserName, Properties.Settings.Default.Password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // We want to download all messages
                List<Message> allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                    MessageBox.Show(i.ToString());
                    
                    
                }
                // Now return the fetched messages to the database
                foreach (Message MailItem in allMessages)
                {
                    if(MailItem.Headers.MessageId.Length >= 0)
                    { 
                    //MessagePart html = MailItem.FindFirstHtmlVersion();
                    //MessagePart plainText = MailItem.FindFirstPlainTextVersion();
                    //if (html != null)
                    //{

                        string txtSQLQuery = @"INSERT INTO MailList (MessageId, Receiver, Sender, Date, Subject, Message) VALUES ('" + MailItem.Headers.MessageId + "','" + MailItem.Headers.To + "','" + MailItem.Headers.From + "','" + MailItem.Headers.DateSent + "','" + MailItem.Headers.Subject + "', @param)";
                        
                        SQLHandling.insetToDatabase(txtSQLQuery, readMail(MailItem));
                    //}
                    //else if (plainText != null)
                    //{
                      //  string txtSQLQuery = "INSERT INTO MailList (MessageId, Receiver, Sender, Date, Subject, Message) VALUES ('" + MailItem.Headers.MessageId + "','" + MailItem.Headers.To + "','" + MailItem.Headers.Sender + "','" + MailItem.Headers.DateSent + "','" + MailItem.Headers.Subject + "','" + plainText.GetBodyAsText() + "')";
                        //SQLHandling.insetToDatabase(txtSQLQuery);
                    //}
                    }
                    
                }

                
                
                
            }

            }
            
            catch(Exception e)
            { MessageBox.Show(e.Message); }
        }


        public static string readMail (Message mail)
        {
            StringBuilder bodyBuilder = new StringBuilder();

            MessagePart plainText = mail.FindFirstPlainTextVersion();
            MessagePart htmlText = mail.FindFirstHtmlVersion();

            if (plainText != null)
                bodyBuilder.Append(plainText.GetBodyAsText());
            
            if (htmlText != null)
                bodyBuilder.Append(htmlText.GetBodyAsText());
            

            if (plainText == null && bodyBuilder == null)
                 return "Empty mail";
            
            else
                return bodyBuilder.ToString();
        }


        //For sending mails..
        public static void sendEmail(string reciverMail, string senderMail, string subject, string textBody, string mailPassword, string attachment)
        {
            ThreadStart processTaskThread = delegate
            {
            
            
            SmtpClient c = new SmtpClient(@"smtp.live.com", 25);
            MailAddress add = new MailAddress(reciverMail);
            MailMessage mail = new MailMessage();
            mail.To.Add(add);
            mail.From = new MailAddress(senderMail);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = textBody;
            mail.Attachments.Add(new Attachment(attachment));
            c.Credentials = new System.Net.NetworkCredential(senderMail, mailPassword);
            c.EnableSsl = true;
            
            try
            {
                c.Send(mail);
            }
            catch(Exception e) {
                MessageBox.Show(e.ToString());
            }

            finally
            {
                c.Dispose();
            }
            };
            Thread thread = new Thread(processTaskThread);
            thread.Start();
        }

        public static void sendEmail(string reciverMail, string senderMail, string subject, string textBody, string mailPassword)
        {
            ThreadStart processTaskThread = delegate
            {
            
            SmtpClient c = new SmtpClient(@"smtp.live.com", 25);
            MailAddress add = new MailAddress(reciverMail);
            MailMessage mail = new MailMessage();
            mail.To.Add(add);
            mail.From = new MailAddress(senderMail);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = textBody;
            c.Credentials = new System.Net.NetworkCredential(senderMail, mailPassword);
            c.EnableSsl = true;

            try
            {
                c.Send(mail);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                c.Dispose();
            }
                        };
            Thread thread = new Thread(processTaskThread);
            thread.Start();
        }

        //public static void sendEmail(string reciverMail, string senderMail, string subject, string textBody, string mailPassword, string CC)
        //{
        //    SmtpClient c = new SmtpClient(@"smtp.live.com", 25);
        //    MailAddress add = new MailAddress(reciverMail);
        //    MailMessage msg = new MailMessage();
        //    msg.To.Add(add);
        //    msg.From = new MailAddress(senderMail);
        //    msg.IsBodyHtml = true;
        //    msg.Subject = subject;
        //    msg.Body = textBody;
        //    c.Credentials = new System.Net.NetworkCredential(senderMail, mailPassword);
        //    c.EnableSsl = true;
        //    c.Send(msg);
        //}

        /// <summary>
        /// Example showing:
        ///  - how to save a message to a file
        ///  - how to load a message from a file at a later point
        /// </summary>
        /// <param name="mail">The message to save and load at a later point</param>
        /// <returns>The Message, but loaded from the file system</returns>
        public static Message SaveAndLoadFullMessage (Message mail)
        {
            // FileInfo about the location to save/load message
            FileInfo file = new FileInfo("someFile.eml");

            // Save the full message to some file
            mail.Save(file);

            // Now load the message again. This could be done at a later point
            Message loadedMessage = Message.Load(file);

            // use the message again
            return loadedMessage;
        }




        //public static string FindPlainTextInMessage(Message mail)
        //{
        //    MessagePart plainText = mail.FindFirstPlainTextVersion();
        //    if (plainText != null)
        //    {
        //        return plainText.ToString();
        //    }
        //    else
        //        return "not plain text";
        //}

        //public static string FindHtmlInMessage(Message mail)
        //{
        //    MessagePart html = mail.FindFirstHtmlVersion();
        //    if (html != null)
        //    {
        //        return html.ToString();
        //    }

        //    else
        //        return "not html...";
        //}


        //public static string test42(Message mail)
        //{
        //    string text = mail.FindAllTextVersions().ToString();
        //    if (text != null)
        //    {
        //        return text.ToString();
        //    }

        //    else
        //        return "not html...";
        //}



        
        
    }
}
