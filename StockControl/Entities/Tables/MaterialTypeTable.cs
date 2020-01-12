using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities
{
    public class MaterialTypeTable : ITable<MaterialType>
    {
        public List<MaterialType> Data { get; set; }

        public List<MaterialType> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new MaterialType
                {
                    IdMaterialType = dataRow.Field<int>("id_material_type"),
                    Name = dataRow.Field<string>("name"),
                    Definition = dataRow.Field<string>("definition")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdMaterialType);
            }
            return idList;
        }
    }
}
