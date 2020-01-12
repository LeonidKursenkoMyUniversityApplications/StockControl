using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class Fashion : ITableRow
    {
        public int IdFashion { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }

        public int GetId()
        {
            return IdFashion;
        }

        public string GetIdName()
        {
            return "id_fashion";
        }
    }
}
