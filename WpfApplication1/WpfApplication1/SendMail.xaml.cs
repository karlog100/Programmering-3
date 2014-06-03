using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MailClient;
using System.Threading;

namespace MailClient
{
    /// <summary>
    /// Interaction logic for SendMail.xaml
    /// </summary>
    public partial class SendMail : Window
    {
        public SendMail()
        {
            InitializeComponent();
        }

        private void btrMailSen_Click(object sender, RoutedEventArgs e)
        {

            Thread sendMailThread = new Thread(Mail.sendEmail(txtReciver.Text, @"f.isse2009@live.dk", txtsubject.Text, txtSendMailBody.Text, "Testtest"));
            
        }
    }
}
