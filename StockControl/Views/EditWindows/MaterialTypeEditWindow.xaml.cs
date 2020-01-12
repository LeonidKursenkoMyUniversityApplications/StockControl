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
    /// Interaction logic for MaterialTypeEditWindow.xaml
    /// </summary>
    public partial class MaterialTypeEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public MaterialTypeEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            MaterialType materialType = (MaterialType)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdMaterialType"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["Definition"];
            label3.Content = str;
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = materialType.IdMaterialType;
                textBox1.Text = materialType.IdMaterialType.ToString();
                textBox2.Text = materialType.Name;
                textBox3.Text = materialType.Definition;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string name = textBox2.Text;
            string definition = textBox3.Text;

            try
            {
                EditAdapter.UpdateMaterialType(oldId, ids, name, definition);
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

        private static MaterialTypeEditWindow instance;

        public static MaterialTypeEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new MaterialTypeEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
