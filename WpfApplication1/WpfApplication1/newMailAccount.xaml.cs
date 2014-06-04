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

namespace MailClient
{
    /// <summary>
    /// Interaction logic for newMailAccount.xaml
    /// </summary>
    public partial class newMailAccount : Window
    {
        public newMailAccount()
        {
            InitializeComponent();
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.UserName = txtMail.Text;
            Properties.Settings.Default.Password = txtPassword.Password;
            Properties.Settings.Default.POPPort = Convert.ToInt32(txtPOPPort.Text);
            Properties.Settings.Default.POPServer = txtPOPServer.Text;
            Properties.Settings.Default.SSL = checkSSL.IsChecked.Value;
            Properties.Settings.Default.SMTPPort = Convert.ToInt32(txtSMTPPort.Text);
            Properties.Settings.Default.SMTPServer = txtSMTPServer.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("you saved the settings!");

            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
