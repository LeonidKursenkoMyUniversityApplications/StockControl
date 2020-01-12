using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Interfaces
{
    public interface ITable<T>
    {
        List<int> GetIdList();
        List<T> DataSetToList(DataSet ds);
    }
}
