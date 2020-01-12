using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class StateTable : ITable<State>
    {
        public List<State> Data { get; set; }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdState);
            }
            return idList;
        }


        public List<State> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new State
                {
                    IdState = dataRow.Field<int>("id_state"),
                    Name = dataRow.Field<string>("name")
                }).ToList();
            return Data;
        }
    }
}
