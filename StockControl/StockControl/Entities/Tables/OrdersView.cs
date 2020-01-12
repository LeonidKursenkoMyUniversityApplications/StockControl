using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.Tables
{
    public class OrdersView : ITableRow
    {
        public int IdOrder { get; set; }

        public string Date { get; set; }

        public int AmountMaterial { get; set; }

        public string Material { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }

        public int GetId()
        {
            return IdOrder;
        }

        public string GetIdName()
        {
            return "id_order";
        }
    }
}
