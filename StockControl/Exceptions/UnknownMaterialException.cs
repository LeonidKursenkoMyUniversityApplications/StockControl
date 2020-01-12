using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Exceptions
{
    public class UnknownMaterialException : Exception
    {
        public UnknownMaterialException(string message) : base(message) { }
    }
}
