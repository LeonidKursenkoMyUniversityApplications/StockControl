using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class Orders : ITableRow
    {
        public int IdOrder { get; set; }

        public string Date { get; set; }

        public int AmountMaterial { get; set; }

        public int IdMaterial { get; set; }
        public int IdDestination { get; set; }
        public int IdState { get; set; }

        public int GetId()
        {
            return IdOrder;
        }

        public string GetIdName()
        {
            return "id_order";
        }

        public int GetMonth()
        {
            return DateTime.Parse(Date).Month;
        }
    }
}
