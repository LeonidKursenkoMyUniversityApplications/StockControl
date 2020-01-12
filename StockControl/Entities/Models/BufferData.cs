using StockControl.Controllers;
using StockControl.Entities.Models;
using StockControl.Entities.Tables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockControl.Entities.Models.SystemUser;

namespace StockControl.Entities
{
    public class BufferData
    {
        private static BufferData instance;
        
        // User types
        public UsersType UserType { get; set; }
        // View modes
        public enum Modes { Structure, View}
        public Modes Mode { get; set; }

        public enum EditModes { Insert, Update, Find, Print}
        public EditModes EditMode { get; set; }
        // Tables referes
        public Dictionary<string, string> TablesDictinionary { get; set; }

        public Dictionary<string, string> DbTablesAndTablesDictinionary { get; set; }
        public Dictionary<string, string> NameOfAttribDictinionary { get; set; }
        public Dictionary<string, string> PropNameAndFieldNameDictinionary { get; set; }
        public ICollection TablesList { get; set; }
        public ICollection TablesNameList { get; set; }

        // Current table values
        public string CurrentTableName { get; set; }
        public int CurrentIdRow { get; set; }
        public object CurrentRow { get; set; }

        public List<string> ColumnName { get; set; }
        public List<string> ColumnDisplayName { get; set; }

        public Dictionary<string, string> ColumnDisplayAndName { get; set; }

        #region Tables
        public DestinationTable DestinationTable { get; set; }
        public FashionTable FashionTable { get; set; }
        public IssuedMaterialsTable IssuedMaterialsTable { get; set; }
        public IssuedViewTable IssuedViewTable { get; set; }
        public ManufacturerTable ManufacturerTable { get; set; }
        public MaterialTable MaterialTable { get; set; }
        public MaterialTypeTable MaterialTypeTable { get; set; }
        public MaterialViewTable MaterialViewTable { get; set; }
        public OrdersTable OrdersTable { get; set; }
        public OrdersViewTable OrdersViewTable { get; set; }
        public ReceivedMaterialTable ReceivedMaterialTable { get; set; }
        public ReceivedViewTable ReceivedViewTable { get; set; }
        public StateTable StateTable { get; set; }
        #endregion

        public DataSet DataSet { get; set; }

        private BufferData()
        {
            UserType = SystemUser.GetInstance().UserType;
            Mode = Modes.Structure;
            Rebuild();
            NameOfAttribDictinionary = new Dictionary<string, string>();
            InitListOfNameOfAttrib();

            DestinationTable = new DestinationTable();
            FashionTable = new FashionTable();
            IssuedMaterialsTable = new IssuedMaterialsTable();
            IssuedViewTable = new IssuedViewTable();
            ManufacturerTable = new ManufacturerTable();
            MaterialTable = new MaterialTable();
            MaterialTypeTable = new MaterialTypeTable();
            MaterialViewTable = new MaterialViewTable();
            OrdersTable = new OrdersTable();
            OrdersViewTable = new OrdersViewTable();
            ReceivedMaterialTable = new ReceivedMaterialTable();
            ReceivedViewTable = new ReceivedViewTable();
            StateTable = new StateTable();

            RelateDbTableNameWithClasses();
        }

        public static BufferData GetInstance()
        {
            if (instance == null)
            {
                instance = new BufferData();
            }
            return instance;
        }

        public void OpenTable(string tableName)
        {
            DepotDbConnector depotDbConnector = DepotDbConnector.GetInstance();
            DataSet ds = depotDbConnector.SelectQuerry("SELECT * FROM " + tableName);       
            //MessageBox.Show(tableName);
            switch (tableName)
            {
                case "state":
                    StateTable.DataSetToList(ds);
                    break;
                case "destination":
                    DestinationTable.DataSetToList(ds);
                    break;
                case "fashion":
                    FashionTable.DataSetToList(ds);
                    break;
                case "manufacturer":
                    ManufacturerTable.DataSetToList(ds);
                    break;
                case "material":
                    MaterialTable.DataSetToList(ds);
                    break;
                case "material_type":
                    MaterialTypeTable.DataSetToList(ds);
                    break;
                case "orders":
                    OrdersTable.DataSetToList(ds);
                    break;
                case "received_material":
                    ReceivedMaterialTable.DataSetToList(ds);
                    break;
                case "issued_materials":
                    IssuedMaterialsTable.DataSetToList(ds);
                    break;
                case "materialview":
                    MaterialViewTable.DataSetToList(ds);
                    break;
                case "issuedview":
                    IssuedViewTable.DataSetToList(ds);
                    break;
                case "receivedview":
                    ReceivedViewTable.DataSetToList(ds);
                    break;
                case "ordersview":
                    OrdersViewTable.DataSetToList(ds);
                    break;
            }
        }

        internal IEnumerable GetTable(string tableName)
        {
            switch (tableName)
            {
                case "state":
                    return StateTable.Data;
                case "destination":
                    return DestinationTable.Data;                   
                case "fashion":
                    return FashionTable.Data;
                case "manufacturer":
                    return ManufacturerTable.Data;
                case "material":
                    return MaterialTable.Data;
                case "material_type":
                    return MaterialTypeTable.Data;
                case "orders":
                    return OrdersTable.Data;
                case "received_material":
                    return ReceivedMaterialTable.Data;
                case "issued_materials":
                    return IssuedMaterialsTable.Data;
                case "materialview":
                    return MaterialViewTable.Data;
                case "issuedview":
                    return IssuedViewTable.Data;
                case "receivedview":
                    return ReceivedViewTable.Data;
                case "ordersview":
                    return OrdersViewTable.Data;
            }
            throw new Exception();
        }

        internal IEnumerable GetTable(string tableName, DataSet ds)
        {
            switch (tableName)
            {
                case "state":                    
                    return StateTable.DataSetToList(ds);
                case "destination":
                    return DestinationTable.DataSetToList(ds);
                case "fashion":
                    return FashionTable.DataSetToList(ds);
                case "manufacturer":
                    return ManufacturerTable.DataSetToList(ds);
                case "material":
                    return MaterialTable.DataSetToList(ds);
                case "material_type":
                    return MaterialTypeTable.DataSetToList(ds);
                case "orders":
                    return OrdersTable.DataSetToList(ds);
                case "received_material":
                    return ReceivedMaterialTable.DataSetToList(ds);
                case "issued_materials":
                    return IssuedMaterialsTable.DataSetToList(ds);
                case "materialview":
                    return MaterialViewTable.DataSetToList(ds);
                case "issuedview":
                    return IssuedViewTable.DataSetToList(ds);
                case "receivedview":
                    return ReceivedViewTable.DataSetToList(ds);
                case "ordersview":
                    return OrdersViewTable.DataSetToList(ds);
            }
            throw new Exception();
        }

        public void OpenAllTables()
        {
            foreach(var tableName in DbTablesAndTablesDictinionary.Keys)
            {
                OpenTable(tableName.ToString());
            }

            ColumnDisplayAndName = new Dictionary<string, string>();
        }

        public void Rebuild()
        {
            TablesDictinionary = new Dictionary<string, string>();
            Init();
            TablesList = TablesDictinionary.Values;
            TablesNameList = TablesDictinionary.Keys;

            // select table
            foreach (var it in TablesList)
            {
                CurrentTableName = (string)it;
                break;
            }
            // select row
            CurrentIdRow = 0;
        }

        private void Init()
        {
            if (UserType == UsersType.Admin)
            {
                if (Mode == Modes.Structure)
                {
                    TablesDictinionary.Add("destination", "Місце призначення");
                    TablesDictinionary.Add("fashion", "Фасони");
                    TablesDictinionary.Add("manufacturer", "Виробники");
                    TablesDictinionary.Add("material", "Матеріали");
                    TablesDictinionary.Add("material_type", "Види матеріалів");
                    TablesDictinionary.Add("orders", "Замовлення");
                    TablesDictinionary.Add("received_material", "Отримані матеріали");
                    TablesDictinionary.Add("state", "Статус замовлення");
                    TablesDictinionary.Add("issued_materials", "Видані матеріали");
                }
                else
                {
                    TablesDictinionary.Add("destination", "Місце призначення");
                    TablesDictinionary.Add("fashion", "Фасони");
                    TablesDictinionary.Add("manufacturer", "Виробники");
                    TablesDictinionary.Add("materialview", "Матеріали");
                    TablesDictinionary.Add("material_type", "Види матеріалів");
                    TablesDictinionary.Add("ordersview", "Замовлення");
                    TablesDictinionary.Add("receivedview", "Отримані матеріали");
                    TablesDictinionary.Add("state", "Статус замовлення");
                    TablesDictinionary.Add("issuedview", "Видані матеріали");
                }
            }
            else
            {
                if (Mode == Modes.Structure)
                {
                    //TablesDictinionary.Add("destination", "Місце призначення");
                    TablesDictinionary.Add("fashion", "Фасони");
                    TablesDictinionary.Add("manufacturer", "Виробники");
                    TablesDictinionary.Add("material", "Матеріали");
                    TablesDictinionary.Add("material_type", "Види матеріалів");
                    TablesDictinionary.Add("orders", "Замовлення");
                    TablesDictinionary.Add("received_material", "Отримані матеріали");
                    TablesDictinionary.Add("state", "Статус замовлення");
                    TablesDictinionary.Add("issued_materials", "Видані матеріали");
                }
                else
                {
                    //TablesDictinionary.Add("destination", "Місце призначення");
                    TablesDictinionary.Add("fashion", "Фасони");
                    TablesDictinionary.Add("manufacturer", "Виробники");
                    TablesDictinionary.Add("materialview", "Матеріали");
                    TablesDictinionary.Add("material_type", "Види матеріалів");
                    TablesDictinionary.Add("ordersview", "Замовлення");
                    TablesDictinionary.Add("receivedview", "Отримані матеріали");
                    TablesDictinionary.Add("state", "Статус замовлення");
                    TablesDictinionary.Add("issuedview", "Видані матеріали");
                }
            }
        }

        private void InitListOfNameOfAttrib()
        {            
            NameOfAttribDictinionary.Add("IdDestination", "Код місця призначення");
            NameOfAttribDictinionary.Add("Type", "Тип");
            NameOfAttribDictinionary.Add("IdFashion", "Код фасону");
            NameOfAttribDictinionary.Add("Name", "Назва");
            NameOfAttribDictinionary.Add("Definition", "Опис");
            NameOfAttribDictinionary.Add("IdIssuedMaterial", "Код операції видачі матеріалу");
            NameOfAttribDictinionary.Add("Date", "Дата");
            NameOfAttribDictinionary.Add("AmountMaterial", "Кількість матеріалів");
            NameOfAttribDictinionary.Add("IdMaterial", "Код матеріалу");
            NameOfAttribDictinionary.Add("IdManufacturer", "Код виробника");
            NameOfAttribDictinionary.Add("Country", "Країна");
            NameOfAttribDictinionary.Add("City", "Місто");
            NameOfAttribDictinionary.Add("Telephone", "Телефон");
            NameOfAttribDictinionary.Add("DateProducing", "Дата випуску");
            NameOfAttribDictinionary.Add("DateReceived", "Дата отримання");
            NameOfAttribDictinionary.Add("IdMaterialType", "Код типу матеріалу");
            NameOfAttribDictinionary.Add("IdOrder", "Код замовлення");
            NameOfAttribDictinionary.Add("IdState", "Код стану замовлення");
            NameOfAttribDictinionary.Add("IdReceivedMaterial", "Код отримання матеріалу");
            NameOfAttribDictinionary.Add("Amount", "Кількість");

            NameOfAttribDictinionary.Add("Manufacturer", "Виробник");
            NameOfAttribDictinionary.Add("MaterialType", "Тип матеріалу");
            NameOfAttribDictinionary.Add("Fashion", "Фасон");
            NameOfAttribDictinionary.Add("Material", "Матеріал");
            NameOfAttribDictinionary.Add("Destination", "Місце призначення");
            NameOfAttribDictinionary.Add("Status", "Стан");

            InitListOfPropAndFieldName();
        }

        private void InitListOfPropAndFieldName()
        {
            PropNameAndFieldNameDictinionary = new Dictionary<string, string>();

            PropNameAndFieldNameDictinionary.Add("IdDestination", "id_destination");
            PropNameAndFieldNameDictinionary.Add("Type", "type");
            PropNameAndFieldNameDictinionary.Add("IdFashion", "id_fashion");
            PropNameAndFieldNameDictinionary.Add("Name", "name");
            PropNameAndFieldNameDictinionary.Add("Definition", "definition");
            PropNameAndFieldNameDictinionary.Add("IdIssuedMaterial", "id_issued_material");
            PropNameAndFieldNameDictinionary.Add("Date", "date");
            PropNameAndFieldNameDictinionary.Add("AmountMaterial", "amount_material");
            PropNameAndFieldNameDictinionary.Add("IdMaterial", "id_material");
            PropNameAndFieldNameDictinionary.Add("IdManufacturer", "id_manufacturer");
            PropNameAndFieldNameDictinionary.Add("Country", "country");
            PropNameAndFieldNameDictinionary.Add("City", "city");
            PropNameAndFieldNameDictinionary.Add("Telephone", "telephone");
            PropNameAndFieldNameDictinionary.Add("DateProducing", "date_producing");
            PropNameAndFieldNameDictinionary.Add("DateReceived", "date_received");
            PropNameAndFieldNameDictinionary.Add("IdMaterialType", "id_material_type");
            PropNameAndFieldNameDictinionary.Add("IdOrder", "id_order");
            PropNameAndFieldNameDictinionary.Add("IdState", "id_state");
            PropNameAndFieldNameDictinionary.Add("IdReceivedMaterial", "id_received_material");
            PropNameAndFieldNameDictinionary.Add("Amount", "amount");

            PropNameAndFieldNameDictinionary.Add("Manufacturer", "producer");
            PropNameAndFieldNameDictinionary.Add("MaterialType", "type");
            PropNameAndFieldNameDictinionary.Add("Fashion", "fason");
            PropNameAndFieldNameDictinionary.Add("Material", "name");
            PropNameAndFieldNameDictinionary.Add("Destination", "type");
            PropNameAndFieldNameDictinionary.Add("Status", "status");
        }

        private void RelateDbTableNameWithClasses()
        {
            DbTablesAndTablesDictinionary = new Dictionary<string, string>();

            DbTablesAndTablesDictinionary.Add("destination", "DestinationTable");
            DbTablesAndTablesDictinionary.Add("fashion", "FashionTable");
            DbTablesAndTablesDictinionary.Add("manufacturer", "ManufacturerTable");
            DbTablesAndTablesDictinionary.Add("material", "MaterialTable");
            DbTablesAndTablesDictinionary.Add("material_type", "MaterialTypeTable");
            DbTablesAndTablesDictinionary.Add("orders", "OrdersTable");
            DbTablesAndTablesDictinionary.Add("received_material", "ReceivedMaterialTable");
            DbTablesAndTablesDictinionary.Add("state", "StateTable");
            DbTablesAndTablesDictinionary.Add("issued_materials", "IssuedMaterialsTable");
            DbTablesAndTablesDictinionary.Add("materialview", "MaterialViewTable");
            DbTablesAndTablesDictinionary.Add("ordersview", "OrdersViewTable");
            DbTablesAndTablesDictinionary.Add("receivedview", "ReceivedViewTable");
            DbTablesAndTablesDictinionary.Add("issuedview", "IssuedView");
        }

    }
}
