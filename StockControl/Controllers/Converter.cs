using StockControl.Entities;
using StockControl.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers
{
    public class Converter
    {
        //public static List<ITableRow> ToList(DataSet ds)
        //{
        //    List<ITableRow> empList = null;

        //    return empList;
        //}

        public static List<State> ToStateList(DataSet ds)
        {
            List<State> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new State
                {
                    IdState = dataRow.Field<int>("id_state"),
                    Name = dataRow.Field<string>("name")
                }).ToList();
            return empList;
        }

        public static List<Destination> ToDestinationList(DataSet ds)
        {
            List<Destination> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Destination
                {
                    IdDestination = dataRow.Field<int>("id_destination"),
                    Type = dataRow.Field<string>("type")
                }).ToList();
            return empList;
        }

        public static List<Fashion> ToFashionList(DataSet ds)
        {
            List<Fashion> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Fashion
                {
                    IdFashion = dataRow.Field<int>("id_fashion"),
                    Name = dataRow.Field<string>("name"),
                    Definition = dataRow.Field<string>("definition")
                }).ToList();
            return empList;
        }

        public static List<Manufacturer> ToManufacturerList(DataSet ds)
        {
            List<Manufacturer> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Manufacturer
                {
                    IdManufacturer = dataRow.Field<int>("id_manufacturer"),
                    Name = dataRow.Field<string>("name"),
                    Country = dataRow.Field<string>("country"),
                    City = dataRow.Field<string>("city"),
                    Telephone = dataRow.Field<string>("telephone")
                }).ToList();
            return empList;
        }

        public static List<Material> ToMaterialList(DataSet ds)
        {
            List<Material> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Material
                {
                    IdMaterial = dataRow.Field<int>("id_material"),
                    Name = dataRow.Field<string>("name"),
                    DateProducing = dataRow.Field<DateTime>("date_producing").ToString("d"),
                    IdManufacturer = dataRow.Field<int>("id_manufacturer"),
                    IdMaterialType = dataRow.Field<int>("id_material_type"),
                    IdFashion = dataRow.Field<int>("id_fashion")
                }).ToList();
            return empList;
        }

        public static List<MaterialType> ToMaterialTypeList(DataSet ds)
        {
            List<MaterialType> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new MaterialType
                {
                    IdMaterialType = dataRow.Field<int>("id_material_type"),
                    Name = dataRow.Field<string>("name"),
                    Definition = dataRow.Field<string>("definition")
                }).ToList();
            return empList;
        }

        public static List<Orders> ToOrdersList(DataSet ds)
        {
            List<Orders> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new Orders
                {
                    IdOrder = dataRow.Field<int>("id_order"),                    
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    IdMaterial = dataRow.Field<int>("id_material"),
                    IdDestination = dataRow.Field<int>("id_destination"),                    
                    IdState = dataRow.Field<int>("id_state")
                }).ToList();
            return empList;
        }

        public static List<ReceivedMaterial> ToReceivedMaterialList(DataSet ds)
        {
            List<ReceivedMaterial> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new ReceivedMaterial
                {
                    IdReceivedMaterial = dataRow.Field<int>("id_received_material"),
                    DateReceived = dataRow.Field<DateTime>("date_received").ToString("d"),
                    Amount = dataRow.Field<int>("amount"),
                    IdMaterial = dataRow.Field<int>("id_material")
                }).ToList();
            return empList;
        }

        public static List<IssuedMaterials> ToIssuedMaterialsList(DataSet ds)
        {
            List<IssuedMaterials> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new IssuedMaterials
                {
                    IdIssuedMaterial = dataRow.Field<int>("id_issued_material"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    IdMaterial = dataRow.Field<int>("id_material"),
                    IdDestination = dataRow.Field<int>("id_destination")
                }).ToList();
            return empList;
        }

        public static List<MaterialView> ToMaterialViewList(DataSet ds)
        {
            List<MaterialView> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new MaterialView
                {
                    IdMaterial = dataRow.Field<int>("id_material"),
                    Name = dataRow.Field<string>("name"),
                    DateProducing = dataRow.Field<DateTime>("date_producing").ToString("d"),
                    Manufacturer = dataRow.Field<string>("producer"),
                    MaterialType = dataRow.Field<string>("type"),
                    Fashion = dataRow.Field<string>("fason")
                }).ToList();
            return empList;
        }

        public static List<OrdersView> ToOrdersViewList(DataSet ds)
        {
            List<OrdersView> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new OrdersView
                {
                    IdOrder = dataRow.Field<int>("id_order"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    Material = dataRow.Field<string>("name"),
                    Destination = dataRow.Field<string>("type"),
                    Status = dataRow.Field<string>("status")
                }).ToList();
            return empList;
        }

        public static List<ReceivedView> ToReceivedViewList(DataSet ds)
        {
            List<ReceivedView> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new ReceivedView
                {
                    IdReceivedMaterial = dataRow.Field<int>("id_received_material"),
                    DateReceived = dataRow.Field<DateTime>("date_received").ToString("d"),
                    Amount = dataRow.Field<int>("amount"),
                    Material = dataRow.Field<string>("name")
                }).ToList();
            return empList;
        }

        public static List<IssuedView> ToIssuedViewList(DataSet ds)
        {
            List<IssuedView> empList = ds.Tables[0].AsEnumerable().Select(
                dataRow => new IssuedView
                {
                    IdIssuedMaterial = dataRow.Field<int>("id_issued_material"),
                    Date = dataRow.Field<DateTime>("date").ToString("d"),
                    AmountMaterial = dataRow.Field<int>("amount_material"),
                    Material = dataRow.Field<string>("name"),
                    Destination = dataRow.Field<string>("type")
                }).ToList();
            return empList;
        }
    }
}
