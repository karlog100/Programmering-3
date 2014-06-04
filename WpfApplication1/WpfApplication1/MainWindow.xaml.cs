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
        public MainWindow()
        {
            InitializeComponent();
            SQLHandling.CreateDatabase();
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
            Mail mailObjct = new Mail();
            mailObjct.FetchAllMessages();
            
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            browserMailRead.NavigateToString("test");
        }     
    }
}
