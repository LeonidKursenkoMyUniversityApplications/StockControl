using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.Tables
{
    public class MaterialView : ITableRow
    {
        public int IdMaterial { get; set; }

        public string Name { get; set; }
        public string DateProducing { get; set; }

        public string Manufacturer { get; set; }
        public string MaterialType { get; set; }
        public string Fashion { get; set; }

        public int GetId()
        {
            return IdMaterial;
        }

        public string GetIdName()
        {
            return "id_material";
        }
    }
}
