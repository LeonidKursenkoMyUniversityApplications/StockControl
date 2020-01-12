using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class IssuedMaterialsTable : ITable<IssuedMaterials>
    {
        public List<IssuedMaterials> Data { get; set; }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdIssuedMaterial);
            }
            return idList;
        }


        public List<IssuedMaterials> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
               dataRow => new IssuedMaterials
               {
                   IdIssuedMaterial = dataRow.Field<int>("id_issued_material"),
                   Date = dataRow.Field<DateTime>("date").ToString("d"),
                   AmountMaterial = dataRow.Field<int>("amount_material"),
                   IdMaterial = dataRow.Field<int>("id_material"),
                   IdDestination = dataRow.Field<int>("id_destination")
               }).ToList();
            return Data;
        }
    }
}
