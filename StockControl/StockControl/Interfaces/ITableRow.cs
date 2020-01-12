using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.Tables
{
    public interface ITableRow
    {
        string GetIdName();
        int GetId();

    }
}
