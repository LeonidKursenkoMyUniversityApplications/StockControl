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
    /// Interaction logic for ReceivedEditWindow.xaml
    /// </summary>
    public partial class ReceivedEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public ReceivedEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            ReceivedView receivedView = (ReceivedView)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdReceivedMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["DateReceived"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["Amount"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["Material"];
            label4.Content = str;

            var lst1 = new List<string>(bufferData.MaterialTable.Data.Select(x => x.Name));
            comboBox4.ItemsSource = lst1;


            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = receivedView.IdReceivedMaterial;
                textBox1.Text = receivedView.IdReceivedMaterial.ToString();
                datePicker2.SelectedDate = DateTime.Parse(receivedView.DateReceived);
                textBox3.Text = receivedView.Amount.ToString();

                int index = lst1.FindIndex(x => x == receivedView.Material);
                comboBox4.SelectedIndex = index;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                datePicker2.SelectedDate = DateTime.Now.Date;
                textBox3.Text = "";
                comboBox4.SelectedIndex = 0;

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string dates = datePicker2.SelectedDate.Value.ToString("d");
            string amounts = textBox3.Text;
            string mat = comboBox4.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateReceivedView(oldId, ids, dates, amounts, mat);
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

        private static ReceivedEditWindow instance;

        public static ReceivedEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ReceivedEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}

