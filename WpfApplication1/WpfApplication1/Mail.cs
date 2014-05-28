using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop;
using OpenPop.Mime;
using OpenPop.Pop3;

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
        
        string hostname = "pop3.live.com";
        int port = 995; 
        bool useSsl = true;
        string username = @"f.isse2009@live.dk";
        string password = "Testtest";
        
        
        
        public static List<Message> FetchAllMessages(string hostname, int port, bool useSsl, string username, string password)
        {
            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

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
                }

                // Now return the fetched messages
                return allMessages;
            }
        }

        public static string FindPlainTextInMessage(Message mail)
        {
            MessagePart plainText = mail.FindFirstPlainTextVersion();
            if (plainText != null)
            {
                return plainText.ToString();
            }
            else
                return "not plain text";
        }

        public static string FindHtmlInMessage(Message mail)
        {
            MessagePart html = mail.FindFirstHtmlVersion();
            if (html != null)
            {
                return html.ToString();
            }

            else
                return "not html...";
        }


        public static string test42(Message mail)
        {
            string text = mail.FindAllTextVersions().ToString();
            if (text != null)
            {
                return text.ToString();
            }

            else
                return "not html...";
        }
        
    }
}
