using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.SystemTables
{
    public class UserTable
    {
        public List<User> Data { get; set; }
        public List<User> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new User
                {
                    Id = dataRow.Field<int>("idusers"),
                    Login = dataRow.Field<string>("login"),
                    Password = dataRow.Field<string>("password"),
                    Type = dataRow.Field<string>("type")
                }).ToList();
            return Data;
        }
    }
}
