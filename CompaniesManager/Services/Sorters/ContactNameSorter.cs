using CompaniesManager.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace CompaniesManager.Services.Sorters
{
    [DisplayName("Sort by contact name")]
    public class ContactNameSorter : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return x.ContactName.Trim().CompareTo(y.ContactName.Trim());
        }
    }
}
