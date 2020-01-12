using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities.Tables
{
    public class MaterialViewTable : ITable<MaterialView>
    {
        public List<MaterialView> Data { get; set; }

        public List<MaterialView> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new MaterialView
                {
                    IdMaterial = dataRow.Field<int>("id_material"),
                    Name = dataRow.Field<string>("name"),
                    DateProducing = dataRow.Field<DateTime>("date_producing").ToString("d"),
                    Manufacturer = dataRow.Field<string>("producer"),
                    MaterialType = dataRow.Field<string>("type"),
                    Fashion = dataRow.Field<string>("fason")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdMaterial);
            }
            return idList;
        }
    }
}
