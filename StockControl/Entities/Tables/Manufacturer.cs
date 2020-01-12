using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class Manufacturer : ITableRow
    {
        public int IdManufacturer { get; set; }
        public string Name { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }

        public int GetId()
        {
            return IdManufacturer;
        }

        public string GetIdName()
        {
            return "id_manufacturer";
        }
    }
}
