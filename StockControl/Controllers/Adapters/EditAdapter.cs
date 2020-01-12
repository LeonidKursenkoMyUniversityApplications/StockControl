using StockControl.Entities;
using StockControl.Validates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers.Adapters
{
    public class EditAdapter
    {
        public static void UpdateDestination(int oldId, string ids, string types)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string type = ValidateField.ValidateString(types);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update destination set id_destination = " + id +
                    ", type = '" + type + "' where id_destination = " + oldId + ";";
                
                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into destination (id_destination, type) values (" + id +
                    ", '" + type + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateFashion(int oldId, string ids, string names, string defs)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string name = ValidateField.ValidateString(names);
            string def = ValidateField.ValidateString(defs);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update fashion set id_fashion = " + id +
                    ", name = '" + name + "', definition = '" + def +
                    "' where id_fashion = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into fashion (id_fashion, name, definition) values (" + id +
                    ", '" + name + "', '" + def + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateIssuedMaterials(int oldId, string ids, string dates, string amounts, string idMats, string idDests)
        {
            UpdateOrders(oldId, ids, dates, amounts, idMats, idDests, "1");
        }

        public static void UpdateIssuedView(int oldId, string ids, string dates, string amounts, string mats, string dests)
        {
            BufferData buffer = BufferData.GetInstance();
            int idMat = buffer.MaterialTable.Data.First(x => x.Name == mats).IdMaterial;
            int idDest = buffer.DestinationTable.Data.First(x => x.Type == dests).IdDestination;
            UpdateOrders(oldId, ids, dates, amounts, idMat.ToString(), idDest.ToString(), "1");
        }

        public static void UpdateManufacturer(int oldId, string ids, string names, string countrys, string citys, string tels)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string name = ValidateField.ValidateString(names);
            string country = ValidateField.ValidateString(countrys);
            string city = ValidateField.ValidateString(citys);
            string tel = ValidateField.ValidateTelephone(tels);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update manufacturer set id_manufacturer = " + id +
                    ", name = '" + name + 
                    "', country = '" + country +
                    "', city = '" + city +
                    "', telephone = '" + tel +
                    "' where id_manufacturer = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into manufacturer (id_manufacturer, name, country, city, telephone) values (" + 
                    id + ", '" + 
                    name + "', '" +
                    country + "', '" +
                    city + "', '" +
                    tel + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateMaterial(int oldId, string ids, string names, string dates, string idManufacturers, string idMatTypes, string idFashions)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string name = ValidateField.ValidateString(names);
            string date = ValidateField.ValidateDate(dates);
            int idMan = ValidateField.ValidateNumber(idManufacturers);
            int idMat = ValidateField.ValidateNumber(idMatTypes);
            int idFashion = ValidateField.ValidateNumber(idFashions);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update material set id_material = " + id +
                    ", name = '" + name +
                    "', date_producing = '" + date +
                    "', id_manufacturer = '" + idMan +
                    "', id_material_type = '" + idMat +
                    "', id_fashion = '" + idFashion +
                    "' where id_material = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into material (id_material, name, date_producing, id_manufacturer, id_material_type, id_fashion) values (" +
                    id + ", '" +
                    name + "', '" +
                    date + "', '" +
                    idMan + "', '" +
                    idMat + "', '" +
                    idFashion + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateMaterialType(int oldId, string ids, string names, string defs)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string name = ValidateField.ValidateString(names);
            string def = ValidateField.ValidateString(defs);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update material_type set id_material_type = " + id +
                    ", name = '" + name + "', definition = '" + def +
                    "' where id_material_type = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into material_type (id_material_type, name, definition) values (" + id +
                    ", '" + name + "', '" + def + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateMaterialView(int oldId, string ids, string names, string dates, string manufacturers, string matTypes, string fashions)
        {
            BufferData buffer = BufferData.GetInstance();
            string manufacturer = buffer.ManufacturerTable.Data.First(x => x.Name == manufacturers).IdManufacturer.ToString();
            string matType = buffer.MaterialTypeTable.Data.First(x => x.Name == matTypes).IdMaterialType.ToString();
            string fashion = buffer.FashionTable.Data.First(x => x.Name == fashions).IdFashion.ToString();
            UpdateMaterial(oldId, ids, names, dates, manufacturer, matType, fashion);
        }

        public static void UpdateOrders(int oldId, string ids, string dates, string amounts, string idMats, string idDests, string idStates)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string date = ValidateField.ValidateDate(dates);
            int amount = ValidateField.ValidateNumber(amounts);
            int idMat = ValidateField.ValidateNumber(idMats);
            int idDest = ValidateField.ValidateNumber(idDests);
            int idState = ValidateField.ValidateNumber(idStates);

            if((StockCalculate.GetTotalExistMaterial(idMat) - amount) >= 0)
            {
                idState = 1;
            }
            else
            {
                idState = 2;
            }

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update orders set id_order = " + id +
                    ", date = '" + date +
                    "', amount_material = '" + amount +
                    "', id_material = '" + idMat +
                    "', id_destination = '" + idDest +
                    "', id_state = '" + idState +
                    "' where id_order = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into orders (id_order, date, amount_material, id_material, id_destination, id_state) values (" +
                    id + ", '" +
                    date + "', '" +
                    amount + "', '" +
                    idMat + "', '" +
                    idDest + "', '" +
                    idState + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateOrdersView(int oldId, string ids, string dates, string amounts, string mats, string dests, string states)
        {
            BufferData buffer = BufferData.GetInstance();
            string material = buffer.MaterialTable.Data.First(x => x.Name == mats).IdMaterial.ToString();
            string dest = buffer.DestinationTable.Data.First(x => x.Type == dests).IdDestination.ToString();
            string state = buffer.StateTable.Data.First(x => x.Name == states).IdState.ToString();
            UpdateOrders(oldId, ids, dates, amounts, material, dest, state);
        }

        public static void UpdateReceivedMaterial(int oldId, string ids, string dates, string amounts, string idMats)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string date = ValidateField.ValidateDate(dates);
            int amount = ValidateField.ValidateNumber(amounts);
            int idMat = ValidateField.ValidateNumber(idMats);
            
            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update received_material set id_received_material = " + id +
                    ", date_received = '" + date +
                    "', amount = '" + amount +
                    "', id_material = '" + idMat +
                    "' where id_received_material = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into received_material (id_received_material, date_received, amount, id_material) values (" +
                    id + ", '" +
                    date + "', '" +
                    amount + "', '" +
                    idMat + "');";


                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        public static void UpdateReceivedView(int oldId, string ids, string dates, string amounts, string mats)
        {
            BufferData buffer = BufferData.GetInstance();
            string material = buffer.MaterialTable.Data.First(x => x.Name == mats).IdMaterial.ToString();
            UpdateReceivedMaterial(oldId, ids, dates, amounts, material);
        }

        public static void UpdateState(int oldId, string ids, string names)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = ValidateField.ValidateKey(ids, oldId, bufferData.DestinationTable.GetIdList());
            string name = ValidateField.ValidateString(names);

            string query;

            if (bufferData.EditMode == BufferData.EditModes.Update)
            {
                query = "Update state set id_state = " + id +
                    ", name = '" + name + "' where id_state = " + oldId + ";";

                DepotDbConnector.GetInstance().UpdateQuerry(query);
            }
            if (bufferData.EditMode == BufferData.EditModes.Insert)
            {
                query = "Insert into state (id_state, name) values (" + id +
                    ", '" + name + "');";

                DepotDbConnector.GetInstance().InsertQuerry(query);
            }
        }

        

        public static void Delete(string tableName, int id, string key)
        {
            string query;
            tableName = Correct(tableName);
            query = "Delete from " + tableName + " where " + key + "= " + id + ";";

            DepotDbConnector.GetInstance().DeleteQuerry(query);

        }

        private static string Correct(string tableName)
        {            
            switch(tableName)
            {
                case "issuedview": tableName = "issued_materials"; break;
                case "materialview": tableName = "material"; break;
                case "ordersview": tableName = "orders"; break;
                case "receivedview": tableName = "received_material"; break; 
            }
            return tableName;
        }

        // Adds record to the table orders
        public static bool AddOrder(string dates, string amounts, string mats, string dests, 
            string fashions, string types)
        {
            BufferData bufferData = BufferData.GetInstance();

            int id = bufferData.OrdersViewTable.Data.Max(x => x.IdOrder) + 1;

            string date = ValidateField.ValidateDate(dates);
            int amount = ValidateField.ValidateNumber(amounts);

            mats = ValidateField.ValidateString(mats);
            dests = ValidateField.ValidateString(dests);
            fashions = ValidateField.ValidateString(fashions);
            types = ValidateField.ValidateString(types);

            
            int idDest = bufferData.DestinationTable.Data.First(x => x.Type == dests).IdDestination;
            int idState;
            int idFashion = bufferData.FashionTable.Data.First(x => x.Name == fashions).IdFashion;
            int idType = bufferData.MaterialTypeTable.Data.First(x => x.Name == types).IdMaterialType;
            int idMat = bufferData.MaterialTable.Data.First(
                x => x.Name == mats 
                && x.IdFashion == idFashion 
                && x.IdMaterialType == idType).IdMaterial;

            if ((StockCalculate.GetTotalExistMaterial(idMat) - amount) >= 0)
            {
                idState = 1;
            }
            else
            {
                idState = 2;
            }

            string query;

            query = "Insert into orders (id_order, date, amount_material, id_material, id_destination, id_state) values (" +
                id + ", '" +
                date + "', '" +
                amount + "', '" +
                idMat + "', '" +
                idDest + "', '" +
                idState + "');";

            DepotDbConnector.GetInstance().InsertQuerry(query);
            return (idState == 1) ? true : false;
            
        }

    }
}
