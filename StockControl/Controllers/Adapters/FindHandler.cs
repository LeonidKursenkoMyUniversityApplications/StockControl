using StockControl.Entities;
using StockControl.Views.FindWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers.Adapters
{
    public class FindHandler
    {
        public static void Find()
        {
            BufferData bufferData = BufferData.GetInstance();
            bufferData.EditMode = BufferData.EditModes.Find;

            FindWindow w =FindWindow.GetInstance();
            w.Show();
        }
    }
}
