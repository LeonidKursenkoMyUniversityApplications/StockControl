using StockControl.Entities.Models;
using StockControl.Entities.SystemTables;
using StockControl.Exceptions;
using StockControl.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers.Adapters
{
    public class IdentificationHandler
    {
        public static void Identification(string login, string password)
        {


            SystemUser user = SystemUser.GetInstance();
            //user.UserType = SystemUser.UsersType.Admin;
            //user.UserType = SystemUser.UsersType.Client;
            user.UserType = FindUser(login, password);

            StockControlWindow scWindow = new StockControlWindow();
            scWindow.Show();
        }

        public static SystemUser.UsersType FindUser(string login, string password)
        {
            string querry = "Select * from users where login='" + login + "' and password='" + password + "';";

            DataSet ds = DepotDbConnector.GetInstance().SelectQuerry(querry);
            UserTable userTable = new UserTable();
            string type;
            try
            {
                type = userTable.DataSetToList(ds)
                    .First(x => (x.Login == login) && (x.Password == password))
                    .Type;
            }
            catch
            {
                throw new UnknownUserException("Неправильний логін або пароль");
            }
            switch(type)
            {
                case "admin": return SystemUser.UsersType.Admin;
                default: return SystemUser.UsersType.Client;
            }
        }
            
    }
}
