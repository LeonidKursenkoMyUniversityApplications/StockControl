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
    /// Interaction logic for DestinationEditWindow.xaml
    /// </summary>
    public partial class DestinationEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public DestinationEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            Destination destination = (Destination)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdDestination"];
            label1.Content = str;         
            str = bufferData.NameOfAttribDictinionary["Type"];
            label2.Content = str;
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = destination.IdDestination;
                textBox1.Text = destination.IdDestination.ToString();
                textBox2.Text = destination.Type;
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                oldId = -1;
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string ids = textBox1.Text;
            string type = textBox2.Text;

            try
            {
                EditAdapter.UpdateDestination(oldId, ids, type);
                this.Close();
            }
            catch(InputException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private static DestinationEditWindow instance;

        public static DestinationEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new DestinationEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
