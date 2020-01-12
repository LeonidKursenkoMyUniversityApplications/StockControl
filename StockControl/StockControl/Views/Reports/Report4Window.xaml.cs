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
    /// Interaction logic for Report4Window.xaml
    /// </summary>
    public partial class Report4Window : Window
    {
        public Report4Window()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int num = comboBox.SelectedIndex + 1;
            dataGrid.ItemsSource =  StockCalculate.GetNotRequiredMaterials(num);
        }

        private void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            ReportMasterWindow.GetInstance().Show();

            this.Close();
        }

        private static Report4Window instance;

        public static Report4Window GetInstance()
        {
            if (instance == null)
            {
                instance = new Report4Window();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
