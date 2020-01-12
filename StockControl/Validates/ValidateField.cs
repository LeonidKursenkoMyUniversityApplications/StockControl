using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Validates
{
    public class ValidateField
    {
        public static int ValidateKey(string ids, int oldId, List<int> idList)
        {
            int id = ValidateNumber(ids);
            
            if (id <= 0) throw new InputException("Недопустимі значення ключа");

            if(id != oldId)
            {
                if(idList.Find(x => x == id) != 0) throw new InputException("Порушення унікальності ключа");
            }

            return id;
        }
        public static int ValidateNumber(string nums)
        {
            int num = 0;
            try
            {
                num = Convert.ToInt32(nums);
            }
            catch (Exception ex)
            {
                throw new InputException("Недопустимі символи для числового поля");
            }
            if (num < 0) throw new InputException("Недопустимі від'ємні значення числового поля");
            return num;
        }

        public static string ValidateDate(string date)
        {
            //string sPattern = "^\\d{2}\\d{2}\\d{4}$";
            DateTime d;
            try
            {
                d = DateTime.Parse(date);
            }
            catch (Exception ex)
            {
                throw new InputException("Неправильний формат дати: " + date);
            }
            try
            {
                date = d.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                throw new InputException("Неправильний формат дати");
            }

            return date;
        }

        public static string ValidateString(string str)
        {
            return str;
        }

        public static string ValidateTelephone(string telephone)
        {
            return telephone;
        }
    }
}
