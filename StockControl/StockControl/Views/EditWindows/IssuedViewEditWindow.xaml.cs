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
    /// Interaction logic for IssuedViewEditWindow.xaml
    /// </summary>
    public partial class IssuedViewEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public IssuedViewEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            IssuedView IssuedView = (IssuedView)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdIssuedMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Date"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["AmountMaterial"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["Material"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["Destination"];
            label5.Content = str;


            var lst1 = new List<string>(bufferData.MaterialTable.Data.Select(x => x.Name));
            var lst2 = new List<string>(bufferData.DestinationTable.Data.Select(x => x.Type));

            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = IssuedView.IdIssuedMaterial;
                textBox1.Text = IssuedView.IdIssuedMaterial.ToString();

                datePicker2.SelectedDate = DateTime.Parse(IssuedView.Date);

                textBox3.Text = IssuedView.AmountMaterial.ToString();

                int index = lst1.FindIndex(x => x == IssuedView.Material);
                comboBox4.SelectedIndex = index;
                index = lst2.FindIndex(x => x == IssuedView.Destination);
                comboBox5.SelectedIndex = index;

            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                datePicker2.SelectedDate = DateTime.Now.Date;
                textBox3.Text = "";
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;

            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string dates = datePicker2.SelectedDate.Value.ToString("d");
            string amounts = textBox3.Text;
            string material = comboBox4.SelectedItem.ToString();
            string dest = comboBox5.SelectedItem.ToString();

            try
            {
                EditAdapter.UpdateIssuedView(oldId, ids, dates, amounts, material, dest);
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

        private static IssuedViewEditWindow instance;

        public static IssuedViewEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new IssuedViewEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}

