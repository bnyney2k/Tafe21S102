using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace StartFinance.Models
{
    class Appointments
    {
        [PrimaryKey, AutoIncrement]
        public int AppointmentID { get; set; }

        [Unique]
        public string EventName { get; set; }

        [NotNull]
        public string Location { get; set; }

        [NotNull]
        public string PhoneNo { get; set; }

        [NotNull]
        public string Date { get; set; }

        [NotNull]
        public string StarTime { get; set; }

        [NotNull]
        public string EndDate { get; set; }

        [NotNull]
        public string Description { get; set; }      

        [NotNull]
        public string SelectTypeOfService { get; set; }

    }
}
