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
    /// Interaction logic for StateEditWindow.xaml
    /// </summary>
    public partial class StateEditWindow : Window
    {
        private int oldId;
        BufferData bufferData;
        public StateEditWindow()
        {
            InitializeComponent();

            bufferData = BufferData.GetInstance();
            State state = (State)bufferData.CurrentRow;

            string str = bufferData.NameOfAttribDictinionary["IdState"];
            label1.Content = str;
            str = bufferData.NameOfAttribDictinionary["Name"];
            label2.Content = str;
            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                oldId = state.IdState;
                textBox1.Text = state.IdState.ToString();
                textBox2.Text = state.Name;
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
                EditAdapter.UpdateState(oldId, ids, type);
                this.Close();
            }
            catch (InputException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static StateEditWindow instance;

        public static StateEditWindow GetInstance()
        {
            if (instance == null)
            {
                instance = new StateEditWindow();
            }
            return instance;
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            instance = null;
        }
    }
}
