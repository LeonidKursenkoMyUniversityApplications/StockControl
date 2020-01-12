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
    /// Interaction logic for Report5Window.xaml
    /// </summary>
    public partial class Report5Window : Window
    {
        public Report5Window()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            ReportMasterWindow.GetInstance().Show();

            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num = comboBox.SelectedIndex + 1;
            dataGrid.ItemsSource = StockCalculate.GetNotEnoughMaterials(num);
        }

        private static Report5Window instance;

        public static Report5Window GetInstance()
        {
            if (instance == null)
            {
                instance = new Report5Window();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
