using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities
{
    public class MaterialTable : ITable<Material>
    {
        public List<Material> Data { get; set; }

        public List<Material> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Material
                {
                    IdMaterial = dataRow.Field<int>("id_material"),
                    Name = dataRow.Field<string>("name"),
                    DateProducing = dataRow.Field<DateTime>("date_producing").ToString("d"),
                    IdManufacturer = dataRow.Field<int>("id_manufacturer"),
                    IdMaterialType = dataRow.Field<int>("id_material_type"),
                    IdFashion = dataRow.Field<int>("id_fashion")
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
