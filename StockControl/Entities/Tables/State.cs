using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities
{
    public class State : ITableRow
    {
        public int IdState { get; set; }
        public string Name { get; set; }

        public int GetId()
        {
            return IdState;
        }

        public string GetIdName()
        {
            return "id_state";
        }
    }
}
