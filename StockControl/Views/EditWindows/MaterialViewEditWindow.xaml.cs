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
    /// Interaction logic for MaterialViewEditWindow.xaml
    /// </summary>
    public partial class MaterialViewEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public MaterialViewEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            MaterialView MaterialView = (MaterialView)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["DateProducing"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["Manufacturer"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["MaterialType"];
            label5.Content = str;
            str = bufferData.NameOfAttribDictinionary["Fashion"];
            label6.Content = str;

            var lst1 = new List<string>(bufferData.ManufacturerTable.Data.Select(x => x.Name));
            var lst2 = new List<string>(bufferData.MaterialTypeTable.Data.Select(x => x.Name));
            var lst3 = new List<string>(bufferData.FashionTable.Data.Select(x => x.Name));
          
            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;
            comboBox6.ItemsSource = lst3;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = MaterialView.IdMaterial;
                textBox1.Text = MaterialView.IdMaterial.ToString();
                textBox2.Text = MaterialView.Name;

                datePicker3.SelectedDate = DateTime.Parse(MaterialView.DateProducing);

                int index = lst1.FindIndex(x => x == MaterialView.Manufacturer);
                comboBox4.SelectedIndex = index;
                index = lst2.FindIndex(x => x == MaterialView.MaterialType);
                comboBox5.SelectedIndex = index;
                index = lst3.FindIndex(x => x == MaterialView.Fashion);
                comboBox6.SelectedIndex = index;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                textBox2.Text = "";

                datePicker3.SelectedDate = DateTime.Now.Date;

                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string name = textBox2.Text;

            string date = datePicker3.SelectedDate.Value.ToString("d");

            string manufacturer = comboBox4.SelectedItem.ToString();
            string matType = comboBox5.SelectedItem.ToString();
            string fashion = comboBox6.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateMaterialView(oldId, ids, name, date, manufacturer, matType, fashion);
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

        private static MaterialViewEditWindow instance;

        public static MaterialViewEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MaterialViewEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
