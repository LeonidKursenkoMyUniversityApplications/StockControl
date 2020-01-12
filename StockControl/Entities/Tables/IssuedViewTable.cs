using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities.Tables
{
    public class IssuedViewTable : ITable<IssuedView>
    {
        public List<IssuedView> Data { get; set; }

        public List<IssuedView> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new IssuedView
                {
                    IdIssuedMaterial = dataRow.Field<int>("id_issued_material"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    Material = dataRow.Field<string>("name"),
                    Destination = dataRow.Field<string>("type")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdIssuedMaterial);
            }
            return idList;
        }
    }
}
