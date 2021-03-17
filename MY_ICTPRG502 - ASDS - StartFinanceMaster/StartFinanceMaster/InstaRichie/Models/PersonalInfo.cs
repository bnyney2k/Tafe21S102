using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartFinance.Models
{
    public class PersonalInfo
    {
        [PrimaryKey, AutoIncrement]
        public int PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int MobilePhone { get; set; }
    
    }
}
