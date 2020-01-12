using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities.Tables
{
    public class OrdersViewTable : ITable<OrdersView>
    {
        public List<OrdersView> Data { get; set; }

        public List<OrdersView> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new OrdersView
                {
                    IdOrder = dataRow.Field<int>("id_order"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    Material = dataRow.Field<string>("name"),
                    Destination = dataRow.Field<string>("type"),
                    Status = dataRow.Field<string>("status")
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
