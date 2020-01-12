using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities
{
    public class ReceivedMaterialTable : ITable<ReceivedMaterial>
    {
        public List<ReceivedMaterial> Data { get; set; }

        public List<ReceivedMaterial> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new ReceivedMaterial
                {
                    IdReceivedMaterial = dataRow.Field<int>("id_received_material"),
                    DateReceived = dataRow.Field<DateTime>("date_received").ToString("d"),
                    Amount = dataRow.Field<int>("amount"),
                    IdMaterial = dataRow.Field<int>("id_material")
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
