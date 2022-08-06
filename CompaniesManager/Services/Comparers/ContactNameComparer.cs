using CompaniesManager.Models;
using System.Collections.Generic;

namespace CompaniesManager.Services.Sorters
{
    public class ContactNameComparer : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return x.ContactName.Trim().CompareTo(y.ContactName.Trim());
        }
    }
}
