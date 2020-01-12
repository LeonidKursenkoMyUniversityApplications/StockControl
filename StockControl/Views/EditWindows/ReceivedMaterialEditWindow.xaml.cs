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
    /// Interaction logic for ReceivedMaterialEditWindow.xaml
    /// </summary>
    public partial class ReceivedMaterialEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public ReceivedMaterialEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            ReceivedMaterial ReceivedMaterial = (ReceivedMaterial)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdReceivedMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["DateReceived"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["Amount"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdMaterial"];
            label4.Content = str;

            var lst1 = bufferData.MaterialTable.GetIdList();            
            comboBox4.ItemsSource = lst1;
            

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = ReceivedMaterial.IdReceivedMaterial;
                textBox1.Text = ReceivedMaterial.IdReceivedMaterial.ToString();
                datePicker2.SelectedDate = DateTime.Parse(ReceivedMaterial.DateReceived);
                textBox3.Text = ReceivedMaterial.Amount.ToString();

                int index = lst1.FindIndex(x => x == ReceivedMaterial.IdMaterial);
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
            string idMat = comboBox4.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateReceivedMaterial(oldId, ids, dates, amounts, idMat);
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

        private static ReceivedMaterialEditWindow instance;

        public static ReceivedMaterialEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ReceivedMaterialEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
