﻿using OpenPop.Mime;
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
            System.Threading.Thread.Sleep(1500);
                    string hostname = "pop3.live.com";
        int port = 995; 
        bool useSsl = true;
        string username = @"f.isse2009@live.dk";
        string password = "Testtest";
            List<Message> myMails = Mail.FetchAllMessages(hostname, port, useSsl, username, password);

            txtMailList.Text = myMails.Count.ToString();
            

        }
    }
}
