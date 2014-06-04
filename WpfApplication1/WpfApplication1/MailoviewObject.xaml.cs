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
    /// Interaction logic for MailoviewObject.xaml
    /// </summary>
    public partial class MailoviewObject : UserControl
    {
        private static readonly DependencyProperty SenderProperty = DependencyProperty.Register("Sender", typeof(string), typeof(MailoviewObject), new UIPropertyMetadata(""));
        private static readonly DependencyProperty SubjectProperty = DependencyProperty.Register("Subject", typeof(string), typeof(MailoviewObject), new UIPropertyMetadata(""));
        private static readonly DependencyProperty DateProperty = DependencyProperty.Register("Date", typeof(string), typeof(MailoviewObject), new UIPropertyMetadata(""));

        public string Sender {
            get { return (string)GetValue(SenderProperty); }
            set { SetValue(SenderProperty, value); }
        }
        public string Subject
        {
            get { return (string)GetValue(SubjectProperty); }
            set { SetValue(SubjectProperty, value); }
        }
        public string Date
        {
            get { return (string)GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public string Message { get; set; }

        public MailoviewObject(MailObjects mail)
        {
            InitializeComponent();
            Sender = mail.sender;
            Subject = mail.subject;
            Date = mail.date;
            Message = mail.message;
            this.DataContext = this;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).browserMailRead.NavigateToString(Message);
        }
    }
}
