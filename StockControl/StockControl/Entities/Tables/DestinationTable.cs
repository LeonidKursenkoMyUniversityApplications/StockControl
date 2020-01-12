using StockControl.Entities.Tables;
using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class DestinationTable : ITable<Destination>
    {
        public List<Destination> Data { get; set; }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach(var item in Data)
            {
                idList.Add(item.IdDestination);
            }
            return idList;
        }


        public List<Destination> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Destination
                {
                    IdDestination = dataRow.Field<int>("id_destination"),
                    Type = dataRow.Field<string>("type")
                }).ToList();
            return Data;
        }
    }
}
