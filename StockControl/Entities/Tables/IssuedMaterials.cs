using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class IssuedMaterials : ITableRow
    {
        public int IdIssuedMaterial { get; set; }

        public string Date { get; set; }

        public int AmountMaterial { get; set; }

        public int IdMaterial { get; set; }

        public int IdDestination { get; set; }
        public int GetId()
        {
            return IdIssuedMaterial;
        }

        public string GetIdName()
        {
            return "id_issued_material";
        }

        public int GetMonth()
        {
            return DateTime.Parse(Date).Month;
        }

        public DateTime GetDate()
        {
            return DateTime.Parse(Date);
        }
    }
}
