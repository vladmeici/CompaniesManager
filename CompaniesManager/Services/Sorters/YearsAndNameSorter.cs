using CompaniesManager.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace CompaniesManager.Services.Sorters
{
    [DisplayName("Sort by years in business and company name")]
    public class YearsAndNameSorter : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            var compareResult = y.YearsInBusiness.CompareTo(x.YearsInBusiness);
            if (compareResult == 0)
            {
                compareResult = x.CompanyName.Trim().CompareTo(y.CompanyName.Trim());
            }
            return compareResult;
        }
    }
}
