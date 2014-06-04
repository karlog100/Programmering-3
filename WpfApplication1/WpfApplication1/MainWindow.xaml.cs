using OpenPop.Mime;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MailClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Mail mailObjct = new Mail();
        List<MailObjects> Inbox;
        public MainWindow()
        {
            InitializeComponent();
            SQLHandling.CreateDatabase();
            //this need to be enabled when no more debugging
            //mailObjct.FetchAllMessages();
            Inbox = SQLHandling.readMails();
        }

        private void menuSend_Click(object sender, RoutedEventArgs e)
        {
            SendMail sendMailWindow = new SendMail();
            sendMailWindow.Show();
        }

        private void NewMailAccount_Click(object sender, RoutedEventArgs e)
        {
            newMailAccount newMailWindow = new newMailAccount();
            newMailWindow.Show();
        }

        private void menuRecive_Click(object sender, RoutedEventArgs e)
        {            
            mailObjct.FetchAllMessages();

            foreach (MailObjects mail in Inbox)
            {
                MailoviewObject NewItem = new MailoviewObject(mail);
                
                //ListBoxItem newitem = new ListBoxItem();
                //newitem.Content = mail.subject;
                listMailOverView.Children.Add(NewItem);    
            } 
        }

        

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void MailOverView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //browserMailRead.NavigateToString(Inbox[listMailOverView.SelectedIndex].message);
        }     
    }
}
