using StockControl.Controllers.Adapters;
using StockControl.Entities;
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
    /// Interaction logic for OrdersEditWindow.xaml
    /// </summary>
    public partial class OrdersEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public OrdersEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            Orders Orders = (Orders)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdOrder"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Date"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["AmountMaterial"];            
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdMaterial"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdDestination"];
            label5.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdState"];
            label6.Content = str;

            var lst1 = bufferData.MaterialTable.GetIdList();
            var lst2 = bufferData.DestinationTable.GetIdList();
            var lst3 = bufferData.StateTable.GetIdList();

            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;
            comboBox6.ItemsSource = lst3;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = Orders.IdOrder;
                textBox1.Text = Orders.IdOrder.ToString();

                datePicker2.SelectedDate = DateTime.Parse(Orders.Date);

                textBox3.Text = Orders.AmountMaterial.ToString();
                
                int index = lst1.FindIndex(x => x == Orders.IdMaterial);
                comboBox4.SelectedIndex = index;
                index = lst2.FindIndex(x => x == Orders.IdDestination);
                comboBox5.SelectedIndex = index;
                index = lst3.FindIndex(x => x == Orders.IdState);
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
            string idMat = comboBox4.SelectedItem.ToString();
            string idDest = comboBox5.SelectedItem.ToString();
            string idState = comboBox6.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateOrders(oldId, ids, dates, amounts, idMat, idDest, idState);
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

        private static OrdersEditWindow instance;

        public static OrdersEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new OrdersEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
