using CompaniesManager.Models;
using System.Collections.Generic;

namespace CompaniesManager.Services.Comparers
{
    public class YearsAndNameComparer : IComparer<Company>
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
