using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class Material : ITableRow
    {
        public int IdMaterial { get; set; }

        public string Name { get; set; }
        public string DateProducing { get; set; }

        public int IdManufacturer { get; set; }
        public int IdMaterialType { get; set; }
        public int IdFashion { get; set; }

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
