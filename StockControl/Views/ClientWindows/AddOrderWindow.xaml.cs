using StockControl.Controllers.Adapters;
using StockControl.Entities;
using StockControl.Entities.Tables;
using StockControl.Exceptions;
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

namespace StockControl.Views.ClientWindows
{
    /// <summary>
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private static AddOrderWindow instance;
        
        BufferData bufferData;
        public AddOrderWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            
            string str; 
            str = bufferData.NameOfAttribDictinionary["Date"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["AmountMaterial"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["Material"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["Destination"];
            label5.Content = str;
            str = bufferData.NameOfAttribDictinionary["Fashion"];
            label6.Content = str;
            str = bufferData.NameOfAttribDictinionary["MaterialType"];
            label7.Content = str;
            

            var nameLst = new List<string>(bufferData.MaterialTable.Data.Select(x => x.Name));
            var destLst = new List<string>(bufferData.DestinationTable.Data.Select(x => x.Type));
            //var fashionLst = new List<string>(bufferData.FashionTable.Data.Select(x => x.Name));
            //var typeLst = new List<string>(bufferData.MaterialTypeTable.Data.Select(x => x.Name));

            comboBox4.ItemsSource = nameLst;
            comboBox5.ItemsSource = destLst;
            //comboBox6.ItemsSource = fashionLst;
            //comboBox7.ItemsSource = typeLst;
            comboBox4.SelectedIndex = 0;
            string nameMaterial = comboBox4.SelectedItem.ToString();
            var materialLst = bufferData.MaterialViewTable.Data.FindAll(x => x.Name == nameMaterial);
            comboBox6.ItemsSource = materialLst.Select(x => x.Fashion);
            comboBox7.ItemsSource = materialLst.Select(x => x.MaterialType);

            datePicker2.SelectedDate = DateTime.Now.Date;
            textBox3.Text = "";
            //comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;

        }

        public static AddOrderWindow GetInstance()
        {
            if(instance == null)
            {
                instance = new AddOrderWindow();
            }
            return instance;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string dates = datePicker2.SelectedDate.Value.ToString("d");
            string amounts = textBox3.Text;
            string mat = comboBox4.SelectedItem.ToString();
            string dest = comboBox5.SelectedItem.ToString();
            string fashion = comboBox6.SelectedItem.ToString();
            string type = comboBox7.SelectedItem.ToString();

            try
            {
                if (EditAdapter.AddOrder(dates, amounts, mat, dest, fashion, type) == true)
                    MessageBox.Show("Заявка успішно подана");
                else MessageBox.Show("Заявку відхилено, на складі не вистачає матеріалів даного типу");
                                
                this.Close();
            }
            catch (InputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (UnknownMaterialException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string nameMaterial = comboBox4.SelectedItem.ToString();
            var materialLst = bufferData.MaterialViewTable.Data.FindAll(x => x.Name == nameMaterial);
            comboBox6.ItemsSource = materialLst.Select(x => x.Fashion);
            comboBox7.ItemsSource = materialLst.Select(x => x.MaterialType);

            comboBox6.SelectedIndex = 0;
            comboBox7.SelectedIndex = 0;
        }

        private void comboBox6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
