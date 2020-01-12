using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Entities.Models
{
    public class SystemUser
    {
        private static SystemUser instance;

        public enum UsersType { Admin, Client }

        public UsersType UserType { get; set; }

        private SystemUser()
        {
            
        }

        public static SystemUser GetInstance()
        {
            if (instance == null)
            {
                instance = new SystemUser();
            }
            return instance;
        }
    }
}
