using StockControl.Views;
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

using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using StockControl.Controllers.Adapters;
using StockControl.Exceptions;

namespace StockControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loginTextBox.Text = "leo";
            passwordTextBox.Text = "1234";
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            try
            {
                IdentificationHandler.Identification(login, password);
                this.Close();
            }
            catch(UnknownUserException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
