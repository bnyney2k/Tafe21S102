using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tafe21S102(Personal_info) - Kiel Caralipio
namespace StartFinance.Models
{
    
    public class PersonalInfo
    {
        [PrimaryKey, AutoIncrement]
        public int PersonalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
    
    }
}
