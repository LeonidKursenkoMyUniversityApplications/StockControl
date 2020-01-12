using StockControl.Controllers.Adapters;
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
    /// Interaction logic for Report2Window.xaml
    /// </summary>
    public partial class Report2Window : Window
    {
        public Report2Window()
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

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = dataPicker.SelectedDate ?? DateTime.Now;

            var list = StockCalculate.GetExistMaterialsGroupByFashion(date);
            ObservableCollection<ExistMaterial> collection = new ObservableCollection<ExistMaterial>(list);

            ListCollectionView collection2 = new ListCollectionView(collection);
            collection2.GroupDescriptions.Add(new PropertyGroupDescription("Fashion"));
            
            dataGrid.ItemsSource = collection2;
        }

        private static Report2Window instance;

        public static Report2Window GetInstance()
        {
            if (instance == null)
            {
                instance = new Report2Window();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
