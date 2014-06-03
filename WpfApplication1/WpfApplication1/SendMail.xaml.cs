﻿using System;
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
        private string filename;
        public SendMail()
        {
            InitializeComponent();
        }

        private void btrMailSen_Click(object sender, RoutedEventArgs e)
        {
            if (lblAttachments.Content.ToString() == "no attachments")
                Mail.sendEmail(txtReciver.Text, @"f.isse2009@live.dk", txtsubject.Text, txtSendMailBody.Text, "Testtest");

            else
                Mail.sendEmail(txtReciver.Text, @"f.isse2009@live.dk", txtsubject.Text, txtSendMailBody.Text, "Testtest", lblAttachments.Content.ToString());

            this.Close();

        }

        private void btrAddFileToMail_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                
                // Open document 
                filename = dlg.FileName;
                lblAttachments.Content = filename;
            }
        }
    }
}
