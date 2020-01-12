using StockControl.Controllers.Adapters;
using StockControl.Entities;
using StockControl.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Report3Window.xaml
    /// </summary>
    public partial class Report3Window : Window
    {
        public Report3Window()
        {
            InitializeComponent();

            comboBox.ItemsSource = BufferData.GetInstance().DestinationTable.Data.Select(x => x.Type);
            comboBox.SelectedIndex = 0;
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportView();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportView();
        }

        private void ReportView()
        {
            DateTime date = dataPicker.SelectedDate ?? DateTime.Now;
            string destination = comboBox.SelectedItem.ToString();
            int idDest = BufferData.GetInstance().DestinationTable.Data.Find(x => x.Type == destination).IdDestination;

            int total = 0;
            var list = StockCalculate.GetMaterialsResult(date, idDest, ref total);

            label3.Content = total;
            
            ObservableCollection<ExistMaterial> collection = new ObservableCollection<ExistMaterial>(list);

            ListCollectionView collection2 = new ListCollectionView(collection);
            collection2.GroupDescriptions.Add(new PropertyGroupDescription("Fashion"));

            dataGrid.ItemsSource = collection2;
        }

        private static Report3Window instance;

        public static Report3Window GetInstance()
        {
            if (instance == null)
            {
                instance = new Report3Window();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
