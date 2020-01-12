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
    /// Interaction logic for IssuedMaterialsEditWindow.xaml
    /// </summary>
    public partial class IssuedMaterialsEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public IssuedMaterialsEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            IssuedMaterials IssuedMaterials = (IssuedMaterials)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdIssuedMaterial"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Date"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["AmountMaterial"];
            label3.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdMaterial"];
            label4.Content = str;
            str = bufferData.NameOfAttribDictinionary["IdDestination"];
            label5.Content = str;
            

            var lst1 = bufferData.MaterialTable.GetIdList();
            var lst2 = bufferData.DestinationTable.GetIdList();
            
            comboBox4.ItemsSource = lst1;
            comboBox5.ItemsSource = lst2;
            
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = IssuedMaterials.IdIssuedMaterial;
                textBox1.Text = IssuedMaterials.IdIssuedMaterial.ToString();

                datePicker2.SelectedDate = DateTime.Parse(IssuedMaterials.Date);

                textBox3.Text = IssuedMaterials.AmountMaterial.ToString();

                int index = lst1.FindIndex(x => x == IssuedMaterials.IdMaterial);
                comboBox4.SelectedIndex = index;
                index = lst1.FindIndex(x => x == IssuedMaterials.IdDestination);
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
            string idMat = comboBox4.SelectedItem.ToString();
            string idDest = comboBox5.SelectedItem.ToString();
            
            try
            {
                EditAdapter.UpdateIssuedMaterials(oldId, ids, dates, amounts, idMat, idDest);
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

        private static IssuedMaterialsEditWindow instance;

        public static IssuedMaterialsEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new IssuedMaterialsEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}

