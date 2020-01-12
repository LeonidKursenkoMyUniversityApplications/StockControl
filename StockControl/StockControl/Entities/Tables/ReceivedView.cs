using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.Tables
{
    public class ReceivedView : ITableRow
    {
        public int IdReceivedMaterial { get; set; }

        public string DateReceived { get; set; }

        public int Amount { get; set; }

        public string Material { get; set; }

        public int GetId()
        {
            return IdReceivedMaterial;
        }

        public string GetIdName()
        {
            return "id_received_material";
        }
    }
}
