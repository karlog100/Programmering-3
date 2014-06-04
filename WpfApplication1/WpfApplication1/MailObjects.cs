using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    class MailObjects
    {
        public string messageID;
        public string receiver;
        public string sender;
        public string date;
        public string subject;
        public string message;

        public MailObjects(string messageID, string receiver, string sender, string date, string subject, string message)
        {
            this.messageID = messageID;
            this.receiver = receiver;
            this.sender = sender;
            this.date = date;
            this.subject = subject;
            this.message = message;
        }
    }

   
}
