using StockControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StockControl.Entities
{
    public class ManufacturerTable : ITable<Manufacturer>
    {
        public List<Manufacturer> Data { get; set; }

        public List<Manufacturer> DataSetToList(DataSet ds)
        {
            Data = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Manufacturer
                {
                    IdManufacturer = dataRow.Field<int>("id_manufacturer"),
                    Name = dataRow.Field<string>("name"),
                    Country = dataRow.Field<string>("country"),
                    City = dataRow.Field<string>("city"),
                    Telephone = dataRow.Field<string>("telephone")
                }).ToList();
            return Data;
        }

        public List<int> GetIdList()
        {
            List<int> idList = new List<int>();
            foreach (var item in Data)
            {
                idList.Add(item.IdManufacturer);
            }
            return idList;
        }
    }
}
