using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class FashionTable : ITable<Fashion>
    {
        public List<Fashion> Data { get; set; }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdFashion);
            }
            return idList;
        }


        public List<Fashion> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Fashion
                {
                    IdFashion = dataRow.Field<int>("id_fashion"),
                    Name = dataRow.Field<string>("name"),
                    Definition = dataRow.Field<string>("definition")
                }).ToList();
            return Data;
        }
    }
}
