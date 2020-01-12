using StockControl.Controllers.Adapters;
using StockControl.Entities;
using StockControl.Entities.Tables;
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

namespace StockControl.Views.EditWindows
{
    /// <summary>
    /// Interaction logic for OrdersViewEditWindow.xaml
    /// </summary>
    public partial class OrdersViewEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public OrdersViewEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            OrdersView ordersView = (OrdersView)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdOrder"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Date"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["AmountMaterial"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["Material"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["Destination"];
            label5.Content = str;
            str = bufferData.NameOfAttribDictinionary["Status"];
            label6.Content = str;

            var lst1 = new List<string>(bufferData.MaterialTable.Data.Select(x => x.Name));
            var lst2 = new List<string>(bufferData.DestinationTable.Data.Select(x => x.Type));
            var lst3 = new List<string>(bufferData.StateTable.Data.Select(x => x.Name));

            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;
            comboBox6.ItemsSource = lst3;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = ordersView.IdOrder;
                textBox1.Text = ordersView.IdOrder.ToString();

                datePicker2.SelectedDate = DateTime.Parse(ordersView.Date);

                textBox3.Text = ordersView.AmountMaterial.ToString();

                int index = lst1.FindIndex(x => x == ordersView.Material);
                comboBox4.SelectedIndex = index;
                index = lst2.FindIndex(x => x == ordersView.Destination);
                comboBox5.SelectedIndex = index;
                index = lst3.FindIndex(x => x == ordersView.Status);
                comboBox6.SelectedIndex = index;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                datePicker2.SelectedDate = DateTime.Now.Date;
                textBox3.Text = "";
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string dates = datePicker2.SelectedDate.Value.ToString("d");
            string amounts = textBox3.Text;
            string mat = comboBox4.SelectedItem.ToString();
            string dest = comboBox5.SelectedItem.ToString();
            string state = comboBox6.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateOrdersView(oldId, ids, dates, amounts, mat, dest, state);
                this.Close();
            }
            catch (InputException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static OrdersViewEditWindow instance;

        public static OrdersViewEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new OrdersViewEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
