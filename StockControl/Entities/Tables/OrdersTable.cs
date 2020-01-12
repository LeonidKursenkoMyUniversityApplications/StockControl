using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities
{
    public class OrdersTable : ITable<Orders>
    {
        public List<Orders> Data { get; set; }

        public List<Orders> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Orders
                {
                    IdOrder = dataRow.Field<int>("id_order"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    IdMaterial = dataRow.Field<int>("id_material"),
                    IdDestination = dataRow.Field<int>("id_destination"),
                    IdState = dataRow.Field<int>("id_state")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdOrder);
            }
            return idList;
        }
    }
}
