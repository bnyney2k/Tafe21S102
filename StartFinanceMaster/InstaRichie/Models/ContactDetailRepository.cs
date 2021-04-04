using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using System.IO;
using System.Collections.Generic;

//Author: Matt Gerlach

namespace StartFinance.Models
{
    class ContactDetailRepository : IContactDetailRepository
    {
        string path;
        SQLite.Net.SQLiteConnection conn;

        public ContactDetailRepository()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            conn = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);
            conn.CreateTable<ContactDetail>();
        }

        public bool DeleteCustomer(int contactId)
        {
            return conn.Delete(contactId) > 0;
        }

        public IEnumerable<ContactDetail> GetContactDetails()
        {
            return conn.Table<ContactDetail>().ToList();
        }

        public ContactDetail GetContactDetailById(int contactId)
        {
            var contactDetail = this.GetContactDetails();
            return contactDetail.First(c => c.ContactId == contactId);
        }

        public bool InsertCustomer(ContactDetail contactDetail)
        {
            return conn.Insert(contactDetail) > 0;
        }


        public bool UpdateCustomer(ContactDetail contactDetail)
        {
            return conn.Update(contactDetail) > 0;
        }

    }
}
