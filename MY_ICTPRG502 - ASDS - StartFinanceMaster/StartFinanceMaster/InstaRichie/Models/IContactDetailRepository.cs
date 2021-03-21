using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Author: Matt Gerlach

namespace StartFinance.Models
{
    public interface IContactDetailRepository
    {
        IEnumerable<ContactDetail> GetContactDetails();
        ContactDetail GetContactDetailById(int contactId);
        bool InsertCustomer(ContactDetail contactDetail);
        bool DeleteCustomer(int contactId);
        bool UpdateCustomer(ContactDetail contactDetail);
    }
}
