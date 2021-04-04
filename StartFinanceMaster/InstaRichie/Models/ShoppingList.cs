
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace StartFinance.Models
{

    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement]
        public int ShoppingItemID {get; set;}
        public string ShopName {get; set;}
        public  string NameOfItem { get; set; }
        public DateTime ShoppingDate { get; set; }
        public double PriceQuoted { get; set; }


    }
}
