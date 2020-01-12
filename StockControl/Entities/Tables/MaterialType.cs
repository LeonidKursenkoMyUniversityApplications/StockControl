using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class MaterialType : ITableRow
    {
        public int IdMaterialType { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }

        public int GetId()
        {
            return IdMaterialType;
        }

        public string GetIdName()
        {
            return "id_material_type";
        }
    }
}
