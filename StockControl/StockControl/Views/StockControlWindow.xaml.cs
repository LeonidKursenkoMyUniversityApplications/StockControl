using MySql.Data.MySqlClient;
using StockControl.Entities;
using StockControl.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static StockControl.Entities.Models.SystemUser;
using StockControl.Entities.Tables;
using StockControl.Controllers.Adapters;
using StockControl.Exceptions;
using StockControl.Entities.Models;
using StockControl.Views.HelpWindows;
using System.Xml;
using System.IO;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

using Excel = Microsoft.Office.Interop.Excel;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;

namespace StockControl.Views
{
    /// <summary>
    /// Interaction logic for StockControlWindow.xaml
    /// </summary>
    public partial class StockControlWindow : Window
    {
        private DepotDbConnector depotDbConnector;
        private BufferData bufferData;
        //private Dictionary<string, string> tables;
        public StockControlWindow()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            if(SystemUser.GetInstance().UserType == SystemUser.UsersType.Client)
            {
                AddButton.Visibility = Visibility.Hidden;
                UpdateButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                
            }
            if (SystemUser.GetInstance().UserType == SystemUser.UsersType.Admin)
            {
                AddOrderButton.Visibility = Visibility.Hidden;
            }
            // Buffer initializes
            bufferData = BufferData.GetInstance();
            // list of tables initializes
            if (bufferData.Mode == BufferData.Modes.Structure)
                StructureRadioButton1.IsChecked = true;
            else
                ViewRadioButton2.IsChecked = true;

            tableListBox.ItemsSource = bufferData.TablesList;

            depotDbConnector = DepotDbConnector.GetInstance();

            string selectedTable = "state";
            SelectTable(selectedTable);
        }

        private void SelectTable(string selectedTable)
        {      
            bufferData.OpenAllTables();
            dataGrid.ItemsSource = bufferData.GetTable(selectedTable);

            SetDefaultBufferConfig(selectedTable);
        }

        private void SetDefaultBufferConfig(string selectedTable)
        {
            bufferData.CurrentTableName = selectedTable;
            bufferData.CurrentIdRow = 0;
            dataGrid.SelectedIndex = bufferData.CurrentIdRow;
            bufferData.CurrentRow = dataGrid.SelectedItem;
        }

        private void tableListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            string selected = "";
            string selectedTable = "";
            if (tableListBox.SelectedItem != null)
            {
                selected = tableListBox.SelectedItem.ToString();
                selectedTable = bufferData.TablesDictinionary.FirstOrDefault(x => x.Value == selected).Key;
                SelectTable(selectedTable);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bufferData.CurrentIdRow = dataGrid.SelectedIndex;
            bufferData.CurrentRow = dataGrid.SelectedItem;
        }

        private void MasterReportMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ReportMasterWindow.GetInstance().Show();
        }

        private void dataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            

            //update column details when generating
            if (bufferData.NameOfAttribDictinionary.ContainsKey(headername) == true)
            {
                e.Column.Header = bufferData.NameOfAttribDictinionary[headername];
                //bufferData.ColumnName.Add(headername);
                //bufferData.ColumnDisplayName.Add(bufferData.NameOfAttribDictinionary[headername]);
                bufferData.ColumnDisplayAndName.Add(bufferData.NameOfAttribDictinionary[headername],
                    headername);
            }
            e.Column.IsReadOnly = true;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // ... Get RadioButton reference.
            var button = sender as RadioButton;

            if (button.Name == "StructureRadioButton1")
            {
                bufferData.Mode = BufferData.Modes.Structure;
            }
            else
            {
                bufferData.Mode = BufferData.Modes.View;
            }
            //string table = bufferData.CurrentTableName;
            bufferData.Rebuild();
            tableListBox.ItemsSource = bufferData.TablesList;

            //SelectTable(table);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bufferData.EditMode = BufferData.EditModes.Insert;
            EditWindowHandler.OpenWindow();
            SelectTable(bufferData.CurrentTableName);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bufferData.EditMode = BufferData.EditModes.Update;
            
            try
            {
                EditWindowHandler.OpenWindow();
            }
            catch (DatabaseActionException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            SelectTable(bufferData.CurrentTableName);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //bufferData.CurrentRow = dataGrid.SelectedItem;
            int id = ((ITableRow)bufferData.CurrentRow).GetId();

            MessageBoxResult result = MessageBox.Show("Підтвердити видалення запису з кодом " + id,
                "Запит на видалення", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != MessageBoxResult.No)
            {
                try
                {
                    EditWindowHandler.Delete(id);
                }
                catch (DatabaseActionException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            SelectTable(bufferData.CurrentTableName);
        }

        public void MakeListOfColumnNames()
        {
            
            List<string> colNameList = new List<string>();
            foreach(var it in dataGrid.Columns)
            {
                colNameList.Add(it.Header.ToString());
            }
            bufferData.ColumnName = colNameList;

        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            FindHandler.Find();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if (bufferData.EditMode == BufferData.EditModes.Print) return;
            if (bufferData.EditMode != BufferData.EditModes.Find)
            {
                SelectTable(bufferData.CurrentTableName);
            }
            if (bufferData.EditMode == BufferData.EditModes.Find)
            {
                if (bufferData.DataSet != null)
                {
                    bufferData.ColumnDisplayAndName = new Dictionary<string, string>();
                    //dataGrid.ItemsSource = bufferData.DataSet.Tables[0].DefaultView;
                    dataGrid.ItemsSource = bufferData.GetTable(bufferData.CurrentTableName, bufferData.DataSet);
                    SetDefaultBufferConfig(bufferData.CurrentTableName);
                }
            }
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindowHandler.OpenAddOrderWindow();
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
        }

        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Інструкція.htm");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void PrintMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            bufferData.EditMode = BufferData.EditModes.Print;

            PrintDialog Printdlg = new PrintDialog();
            bool flag = (bool)Printdlg.ShowDialog(); 
            if (flag == true)
            {
                Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);
                // sizing of the element.
                dataGrid.Measure(pageSize);
                dataGrid.Arrange(new Rect(5, 5, pageSize.Width, pageSize.Height));
                Printdlg.PrintVisual(dataGrid, "Print");                
            }

        }     

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            bufferData.EditMode = BufferData.EditModes.Print;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".pdf";
            saveFileDialog.Filter = "Pdf documents (.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
                ExportToPdf(dataGrid, saveFileDialog.FileName);


            //ExportToCsv(dataGrid, "d:\\A\\test.xls");
            //ExportToExcel(dataGrid, "d:\\A\\test.xls");
        }

        private void ExportToPdf(DataGrid grid, string fileName)
        {
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);


            PdfPTable table = new PdfPTable(grid.Columns.Count);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(fileName, System.IO.FileMode.Create));
            doc.Open();
            for (int j = 0; j < grid.Columns.Count; j++)
            {
                table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), font));
            }
            table.HeaderRows = 1;
            IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < grid.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text, font));
                            }
                        }
                    }
                }

                doc.Add(table);
                doc.Close();
            }
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

    }
}
