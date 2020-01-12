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
    /// Interaction logic for FashionEditWindow.xaml
    /// </summary>
    public partial class FashionEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public FashionEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            Fashion fashion = (Fashion)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdFashion"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            str = bufferData.NameOfAttribDictinionary["Definition"];
            label3.Content = str;
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = fashion.IdFashion;
                textBox1.Text = fashion.IdFashion.ToString();
                textBox2.Text = fashion.Name;
                textBox3.Text = fashion.Definition;
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
                EditAdapter.UpdateFashion(oldId, ids, name, definition);
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

        private static FashionEditWindow instance;

        public static FashionEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new FashionEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
