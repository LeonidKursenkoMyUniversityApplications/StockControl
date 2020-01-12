using StockControl.Controllers.Adapters;
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

namespace StockControl.Views
{
    /// <summary>
    /// Interaction logic for Report1Window.xaml
    /// </summary>
    public partial class Report1Window : Window
    {
        public Report1Window()
        {
            InitializeComponent();

            dataGrid.ItemsSource = StockCalculate.GetExistMaterialTable();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            ReportMasterWindow.GetInstance().Show();

            this.Close();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static Report1Window instance;

        public static Report1Window GetInstance()
        {
            if (instance == null)
            {
                instance = new Report1Window();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
