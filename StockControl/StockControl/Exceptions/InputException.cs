using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class InputException : Exception
    {
        public InputException(string message) : base(message)
        { }
    }
}
