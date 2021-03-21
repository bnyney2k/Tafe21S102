using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

//Author: Matt Gerlach

namespace StartFinance.Models
{
    public class ContactDetail
    {
        [PrimaryKey, AutoIncrement]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string MobilePhone { get; set; }
    }
}
