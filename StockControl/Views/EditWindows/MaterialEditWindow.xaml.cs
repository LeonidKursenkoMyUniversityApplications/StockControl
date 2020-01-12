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
    /// Interaction logic for MaterialWindow.xaml
    /// </summary>
    public partial class MaterialEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public MaterialEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            Material Material = (Material)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["DateProducing"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdManufacturer"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdMaterialType"];
            label5.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdFashion"];
            label6.Content = str;
            
            var lst1 = bufferData.ManufacturerTable.GetIdList();
            var lst2 = bufferData.MaterialTypeTable.GetIdList();
            var lst3 = bufferData.FashionTable.GetIdList();

            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;
            comboBox6.ItemsSource = lst3;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = Material.IdMaterial;
                textBox1.Text = Material.IdMaterial.ToString();
                textBox2.Text = Material.Name;

                datePicker3.SelectedDate = DateTime.Parse(Material.DateProducing);

                int index = lst1.FindIndex(x => x == Material.IdManufacturer);
                comboBox4.SelectedIndex = index;
                index = lst1.FindIndex(x => x == Material.IdMaterialType);
                comboBox5.SelectedIndex = index;
                index = lst1.FindIndex(x => x == Material.IdFashion);
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

            string idManufacturer = comboBox4.SelectedItem.ToString();
            string idMatType = comboBox5.SelectedItem.ToString();
            string idFashion = comboBox6.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateMaterial(oldId, ids, name, date, idManufacturer, idMatType, idFashion);
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

        private static MaterialEditWindow instance;

        public static MaterialEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MaterialEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
