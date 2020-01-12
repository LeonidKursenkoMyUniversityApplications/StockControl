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
    /// Interaction logic for ManufacturerEditWindow.xaml
    /// </summary>
    public partial class ManufacturerEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public ManufacturerEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            Manufacturer manufacturer = (Manufacturer)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdManufacturer"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["Country"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["City"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["Telephone"];
            label5.Content = str;
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = manufacturer.IdManufacturer;
                textBox1.Text = manufacturer.IdManufacturer.ToString();
                textBox2.Text = manufacturer.Name;
                textBox3.Text = manufacturer.Country;
                textBox4.Text = manufacturer.City;
                textBox5.Text = manufacturer.Telephone;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string name = textBox2.Text;
            string country = textBox3.Text;
            string city = textBox4.Text;
            string telephone = textBox5.Text;

            try
            {
                EditAdapter.UpdateManufacturer(oldId, ids, name, country, city, telephone);
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

        private static ManufacturerEditWindow instance;

        public static ManufacturerEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new ManufacturerEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
