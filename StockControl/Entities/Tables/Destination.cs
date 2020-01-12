using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class Destination : ITableRow
    {
        public int IdDestination { get; set; }
        public string Type { get; set; }

        public int GetId()
        {
            return IdDestination;
        }

        public string GetIdName()
        {
            return "id_destination";
        }
    }
}
