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
    /// Interaction logic for ReportMasterWindow.xaml
    /// </summary>
    public partial class ReportMasterWindow : Window
    {
        private List<string> lst;
        public ReportMasterWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            lst = new List<string>();
            lst.Add("1) Звіт залишків матеріалів");
            lst.Add("2) Звіт по матеріалам по фасонам (на певну дату)");
            lst.Add("3) Звіт по матеріалам по фасонам з підведенням підсумків (за певний день)");
            lst.Add("4) Звіт по невикористаним матеріалам впродовж місяця");
            lst.Add("5) Звіт по матеріалам, по заявкам на які були відмови впродовж місяця");

            comboBox.ItemsSource = lst;
            comboBox.SelectedIndex = 0;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            string selected = comboBox.SelectedItem.ToString();
            int index = lst.FindIndex(a => a == selected);

            switch (index)
            {
                case 0:
                    Report1Window.GetInstance().Show();
                    this.Close();
                    break;
                case 1:
                    Report2Window.GetInstance().Show();
                    this.Close();
                    break;
                case 2:
                    Report3Window.GetInstance().Show();
                    this.Close();
                    break;
                case 3:
                    Report4Window.GetInstance().Show();
                    this.Close();
                    break;
                case 4:
                    Report5Window.GetInstance().Show();
                    this.Close();
                    break;
            }
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static ReportMasterWindow instance;

        public static ReportMasterWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ReportMasterWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
