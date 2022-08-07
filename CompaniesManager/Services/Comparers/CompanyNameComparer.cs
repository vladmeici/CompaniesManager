using CompaniesManager.Models;
using System.Collections.Generic;

namespace CompaniesManager.Services.Comparers
{
    public class CompanyNameComparer : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return x.CompanyName.Trim().CompareTo(y.CompanyName.Trim());
        }
    }
}
