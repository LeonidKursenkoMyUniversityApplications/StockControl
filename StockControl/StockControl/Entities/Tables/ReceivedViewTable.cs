using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities.Tables
{
    public class ReceivedViewTable : ITable<ReceivedView>
    {
        public List<ReceivedView> Data { get; set; }

        public List<ReceivedView> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new ReceivedView
                {
                    IdReceivedMaterial = dataRow.Field<int>("id_received_material"),
                    DateReceived = dataRow.Field<DateTime>("date_received").ToString("d"),
                    Amount = dataRow.Field<int>("amount"),
                    Material = dataRow.Field<string>("name")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdReceivedMaterial);
            }
            return idList;
        }
    }
}
