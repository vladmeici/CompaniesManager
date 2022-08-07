using CompaniesManager.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace CompaniesManager.Services.Sorters
{
    [DisplayName("Sort by company name")]
    public class CompanyNameSorter : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return x.CompanyName.Trim().CompareTo(y.CompanyName.Trim());
        }
    }
}
