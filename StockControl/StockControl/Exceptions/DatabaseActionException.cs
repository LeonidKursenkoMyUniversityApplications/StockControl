using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Exceptions
{
    public class DatabaseActionException : Exception
    {
        public DatabaseActionException(string message) : base(message)
        { }
    }
}
