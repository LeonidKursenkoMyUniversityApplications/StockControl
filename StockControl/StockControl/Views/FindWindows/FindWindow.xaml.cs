using StockControl.Controllers;
using StockControl.Entities;
using StockControl.Entities.Tables;
using StockControl.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StockControl.Views.FindWindows
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    /// 
    public partial class FindWindow : Window
    {
        private static FindWindow instance;

        private BufferData bufferData;
        public FindWindow()
        {
            InitializeComponent();
            Init();
        }

        public static FindWindow GetInstance()
        {
            if(instance == null)
            {
                instance = new FindWindow();
            }
            return instance;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            instance = null;
        }

        private void Init()
        {
            bufferData = BufferData.GetInstance();
            listBox.ItemsSource = bufferData.ColumnDisplayAndName.Keys;
            listBox.SelectedIndex = 0;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            string condition = new TextRange(workField.Document.ContentStart, workField.Document.ContentEnd).Text;
            //ICollection displayNameList = bufferData.ColumnDisplayAndName.Keys;
            string conditionOriginal = condition;
            condition = CorrectCondition(condition);
            //MessageBox.Show(condition);
            bufferData.DataSet = null;
            try
            {
                bufferData.DataSet = FindValues(bufferData.CurrentTableName, condition);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неправильний вираз:" + conditionOriginal);
            }

        }

        public string CorrectCondition(string condition)
        {
            foreach (var dict in bufferData.ColumnDisplayAndName)
            {
                condition = condition.Replace("'" + dict.Key + "'", "'" + dict.Value + "'");
            }
            foreach (var dict in bufferData.PropNameAndFieldNameDictinionary)
            {
                condition = condition.Replace("'" + dict.Key + "'", "`" + dict.Value + "`");
            }

            Regex rgx = new Regex(@"(\d{1,2})\.(\d{1,2})\.(\d{4})");
            condition = rgx.Replace(condition, @"$3-$2-$1");
            return condition;
        }

        private DataSet FindValues(string tableName, string condition)
        {
            DepotDbConnector depotDbConnector = DepotDbConnector.GetInstance();
            DataSet ds = depotDbConnector.SelectQuerry(
                "SELECT * FROM " + tableName + " where " + condition);
            return ds;
        }

        private int clickCount = 0;
        private void listBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            string displayName = listBox.SelectedItem.ToString();
            //MessageBox.Show(displayName);

            // Name of class property
            string propName = bufferData.NameOfAttribDictinionary.First(x => x.Value == displayName).Key;
            // Name of db table
            string tableName = bufferData.CurrentTableName;
            // Name of db field
            string fieldName = bufferData.PropNameAndFieldNameDictinionary[propName];
            comboBox.ItemsSource = GetValues(tableName, fieldName);
            try
            {                
                comboBox.ItemsSource = GetValues(bufferData.CurrentTableName, bufferData.PropNameAndFieldNameDictinionary[propName]);
            }
            catch (DatabaseActionException ex)
            {
                MessageBox.Show(ex.Message);
            }

            clickCount++;
            if (clickCount == 2)
            {
                workField.Selection.Text = "'" + displayName + "'";
                clickCount = 0;
            }
            ///
            string nameField = bufferData.ColumnDisplayAndName[displayName];
        }

        private List<string> GetValues(string tableName, string propName)
        {
            DepotDbConnector depotDbConnector = DepotDbConnector.GetInstance();
            DataSet ds = depotDbConnector.SelectQuerry("SELECT " + propName + " FROM " + tableName);
      
            List<string> list = new List<string>();
            object val;
            List<object> listObj = new List<object>();
            string s;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                val = row[propName];
                listObj.Add(val);                
            }

            HashSet<object> list2 = new HashSet<object>(listObj);
            listObj = new List<object>(list2);
            listObj.Sort();

            foreach (var it in listObj)
            {
                try
                {
                    s = ((DateTime)it).ToString("d");
                }
                catch
                {
                    s = it.ToString();
                }
                list.Add(s);
            }            
            return list;

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            workField.Selection.Text = "'" + comboBox.SelectedItem + "'";
        }

        private void AndButton_Click(object sender, RoutedEventArgs e)
        {
            workField.Selection.Text = "AND";
        }

        private void OrButton_Click(object sender, RoutedEventArgs e)
        {
            workField.Selection.Text = "OR";
        }

        private void NotButton_Click(object sender, RoutedEventArgs e)
        {
            workField.Selection.Text = "NOT";
        }
    }
}
