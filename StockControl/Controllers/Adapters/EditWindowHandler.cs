using StockControl.Entities;
using StockControl.Entities.Tables;
using StockControl.Views.ClientWindows;
using StockControl.Views.EditWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers.Adapters
{
    public class EditWindowHandler
    {
        public static void OpenWindow()
        {
            BufferData bufferData = BufferData.GetInstance();
            switch (bufferData.CurrentTableName)
            {
                case "destination":
                    DestinationEditWindow.GetInstance().Show();
                    break;
                case "fashion":
                    FashionEditWindow.GetInstance().Show();
                    break;
                case "manufacturer":
                    ManufacturerEditWindow.GetInstance().Show();
                    break;
                case "material":
                    MaterialEditWindow.GetInstance().Show();
                    break;
                case "material_type":
                    MaterialTypeEditWindow.GetInstance().Show();
                    break;
                case "orders":
                    OrdersEditWindow.GetInstance().Show();
                    break;
                case "received_material":
                    ReceivedMaterialEditWindow.GetInstance().Show();
                    break;
                case "state":
                    StateEditWindow.GetInstance().Show();
                    break;
                case "issued_materials":
                    IssuedMaterialsEditWindow.GetInstance().Show();
                    break;
                case "issuedview":
                    IssuedViewEditWindow.GetInstance().Show();
                    break;
                case "materialview":
                    MaterialViewEditWindow.GetInstance().Show();
                    break;
                case "ordersview":
                    OrdersViewEditWindow.GetInstance().Show();
                    break;
                case "receivedview":
                    ReceivedEditWindow.GetInstance().Show();
                    break;
            }
        }

        public static void Delete(int id)
        {
            BufferData bufferData = BufferData.GetInstance();
            //int id = ((ITableRow)bufferData.CurrentRow).GetId();
            EditAdapter.Delete(bufferData.CurrentTableName, id, 
                ((ITableRow)bufferData.CurrentRow).GetIdName());
            
        }


        public static void OpenAddOrderWindow()
        {
            AddOrderWindow.GetInstance().Show();
        }
    }
}
