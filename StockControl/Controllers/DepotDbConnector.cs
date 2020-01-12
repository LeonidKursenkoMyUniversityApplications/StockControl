using MySql.Data.MySqlClient;
using StockControl.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers
{
    // Клас підключення до бази даних
    public class DepotDbConnector
    {
        private static DepotDbConnector instance;
        // Параметри налаштування підключення до бази даних
        private string server;
        private string port;
        private string user;
        private string password;        
        private string database;
        private string connString;

        // Конструктор за замовчуванням
        private DepotDbConnector()
        {
            server = "localhost";
            port = "3306";
            user = "leo";
            password = "12op43HJ";
            database = "depotdb";
            connString = "server = " + server +
                "; user = " + user +
                "; database = " + database +
                "; port = " + port +
                "; password =" + password +
                "; charset = utf8";
        }

        // Доступ до бази даних
        public static DepotDbConnector GetInstance()
        {
            if(instance == null)
            {
                instance = new DepotDbConnector();
            }
            return instance;
        }

        // Запит типу Select до бази даних
        public DataSet SelectQuerry(string querry)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch(Exception ex)
            {
                throw new DatabaseActionException("Виникла помилка при звернненні до БД");
            }
            conn.Close();
            return ds;
        }

        //Запит оновлення даних
        public void UpdateQuerry(string querry)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);

                MySqlDataReader MyReader2;
                MyReader2 = cmd.ExecuteReader();
                while (MyReader2.Read())
                {
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseActionException("Виникла помилка при редагуанні запису з  БД");
            }
            conn.Close();
        }

        //Запит на додавання даних
        public void InsertQuerry(string querry)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);

                MySqlDataReader MyReader2;
                MyReader2 = cmd.ExecuteReader();
                while (MyReader2.Read())
                {
                }               
            }
            catch (Exception ex)
            {
                throw new DatabaseActionException("Виникла помилка при створенні нового запису");
            }
            conn.Close();
        
        }
        //Запит на видалення даних
        public void DeleteQuerry(string querry)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(querry, conn);

                MySqlDataReader MyReader2;
                MyReader2 = cmd.ExecuteReader();
                while (MyReader2.Read())
                {
                }
            }
            catch (Exception ex)
            {
                throw new DatabaseActionException("Виникла помилка при видаленні запису");
            }
            conn.Close();
        }

        
    }
}
