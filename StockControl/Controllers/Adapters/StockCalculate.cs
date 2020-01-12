using StockControl.Entities;
using StockControl.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Controllers.Adapters
{
    public class StockCalculate
    {
        public static int GetTotalReceivedMaterial(int idMaterial)
        {
            int total = 0;
            List<ReceivedMaterial> list = BufferData.GetInstance().ReceivedMaterialTable.Data;
            foreach(var it in list)
            {
                if(it.IdMaterial == idMaterial)
                {
                    total += it.Amount;
                }
            }
            return total;
        }

        public static int GetTotalIssuedMaterial(int idMaterial)
        {
            int total = 0;
            List<IssuedMaterials> list = BufferData.GetInstance().IssuedMaterialsTable.Data;
            foreach (var it in list)
            {
                if (it.IdMaterial == idMaterial)
                {
                    total += it.AmountMaterial;
                }
            }
            return total;
        }

        public static int GetTotalExistMaterial(int idMaterial)
        {
            int total = 0;
            total = GetTotalReceivedMaterial(idMaterial) - GetTotalIssuedMaterial(idMaterial);
            return total;
        }

        public static int GetTotalReceivedMaterial(int idMaterial, DateTime date)
        {
            int total = 0;
            List<ReceivedMaterial> list = BufferData.GetInstance().ReceivedMaterialTable.Data;
            foreach (var it in list)
            {
                if ((it.IdMaterial == idMaterial) && (it.GetDate() <= date))
                {
                    total += it.Amount;
                }
            }
            return total;
        }
        public static int GetTotalIssuedMaterial(int idMaterial, DateTime date)
        {
            int total = 0;
            List<IssuedMaterials> list = BufferData.GetInstance().IssuedMaterialsTable.Data;
            foreach (var it in list)
            {
                if ((it.IdMaterial == idMaterial) && (it.GetDate() <= date))
                {
                    total += it.AmountMaterial;
                }
            }
            return total;
        }

        public static int GetTotalIssuedMaterial(int idMaterial, DateTime date, int idDestination)
        {
            int total = 0;
            List<IssuedMaterials> list = BufferData.GetInstance().IssuedMaterialsTable.Data;
            foreach (var it in list)
            {
                DateTime date2 = it.GetDate();
                if ((it.IdMaterial == idMaterial) && 
                    (date2.Day == date.Day) &&
                    (date2.Month == date.Month) &&
                    (date2.Year == date.Year) &&
                    (it.IdDestination == idDestination))
                {
                    total += it.AmountMaterial;
                }
            }
            return total;
        }

        public static int GetTotalExistMaterial(int idMaterial, DateTime date)
        {
            int total = 0;
            total = GetTotalReceivedMaterial(idMaterial, date) - GetTotalIssuedMaterial(idMaterial, date);
            return total;
        }

        // 0) Отримати список наявних на складі матеріалів.
        public static List<ExistMaterial> GetExistMaterialTable()
        {
            List<ExistMaterial> listMaterials = new List<ExistMaterial>();

            List<Material> list = BufferData.GetInstance().MaterialTable.Data;
            foreach (var it in list)
            {
                if (GetTotalExistMaterial(it.IdMaterial) > 0)
                {
                    listMaterials.Add(new ExistMaterial
                    {
                        Id = it.IdMaterial,
                        Name = it.Name,
                        Amount = GetTotalExistMaterial(it.IdMaterial)
                    });
                }
            }
            return listMaterials;
        }

        // 1) Отримати список наявних на складі матеріалів по фасонам (на певну дату).
        public static List<ExistMaterial> GetExistMaterialsGroupByFashion(DateTime date)
        {
            List<ExistMaterial> listMaterials = new List<ExistMaterial>();

            List<Material> list = BufferData.GetInstance().MaterialTable.Data;
            var list2 = BufferData.GetInstance().MaterialViewTable.Data;
            foreach (var it in list)
            {
                
                if (GetTotalExistMaterial(it.IdMaterial, date) > 0)
                {
                    string fashion = list2.Find(x => x.IdMaterial == it.IdMaterial).Fashion;
                    listMaterials.Add(new ExistMaterial
                    {
                        Id = it.IdMaterial,
                        Name = it.Name,
                        Fashion = fashion
                    });
                }
            }
            return listMaterials;
        }



        // 2) Отримати список матеріалів і їх кількість (по фасонам)
        // із вказанням признака видачі та з підведенням підсумків по кожному
        // з матеріалів і загального підсумка за певний день.
        public static List<ExistMaterial> GetMaterialsResult(DateTime date, int idDestination, ref int total)
        {
            List<ExistMaterial> listMaterials = new List<ExistMaterial>();

            List<Material> list = BufferData.GetInstance().MaterialTable.Data;
            var list2 = BufferData.GetInstance().IssuedMaterialsTable.Data;
            var fashionList = BufferData.GetInstance().FashionTable.Data;
            foreach (var it in list)
            {
                int amount = GetTotalIssuedMaterial(it.IdMaterial, date, idDestination);
                if (amount > 0)
                {
                    string fashion = fashionList.Find(x => x.IdFashion == it.IdFashion).Name;
                    listMaterials.Add(new ExistMaterial
                    {
                        Id = it.IdMaterial,
                        Name = it.Name,
                        Amount = amount,
                        Fashion = fashion
                    });
                }
            }

            foreach(var it in listMaterials)
            {
                total += it.Amount;
            }
            return listMaterials;
        }

        // 3) Отримати список матеріалів, не затребованих впродовж місяця.
        public static List<ExistMaterial> GetNotRequiredMaterials(int monthNumber)
        {
            List<ExistMaterial> listMaterials = new List<ExistMaterial>();

            List<Material> list = BufferData.GetInstance().MaterialTable.Data;
            List<IssuedMaterials> list2 = BufferData.GetInstance().IssuedMaterialsTable.Data;
            list2 = list2.FindAll(x => x.GetMonth() == monthNumber);
            foreach (var it in list)
            {
                if (list2.Exists(x => x.IdMaterial == it.IdMaterial) != true)
                {
                    listMaterials.Add(new ExistMaterial
                    {
                        Id = it.IdMaterial,
                        Name = it.Name
                    });
                }
            }
            return listMaterials;
        }

        // 4) Отримати список матеріалів, по заявкам на які були відмови.
        public static List<ExistMaterial> GetNotEnoughMaterials(int monthNumber)
        {
            List<ExistMaterial> listMaterials = new List<ExistMaterial>();

            List<Orders> list = BufferData.GetInstance().OrdersTable.Data;
            List<Material> list2 = BufferData.GetInstance().MaterialTable.Data;
            list = list.FindAll(x => x.IdState == 2 && x.GetMonth() == monthNumber);
            foreach (var it in list)
            {                
                listMaterials.Add(new ExistMaterial
                {
                    Id = it.IdMaterial,
                    Name = list2.Find(x => x.IdMaterial == it.IdMaterial).Name
                });               
            }
            listMaterials = (listMaterials.GroupBy(x => x.Id).Select(group => group.First()).ToList<ExistMaterial>());

            return listMaterials;
        }

       
    }
}
