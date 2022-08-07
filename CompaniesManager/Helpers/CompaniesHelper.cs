using CompaniesManager.Models;
using System.Collections.Generic;
using System.Text;

namespace CompaniesManager.Helpers
{
    public static class CompaniesHelper
    {
        public static void SortCompanies(List<Company> companies,IEnumerable<IComparer<Company>> _sorters, string sorterName)
        {
            var sorter = SorterHelper.GetSorter(_sorters, sorterName);

            if (sorter != null)
            {
                companies.Sort((x, y) => sorter.Compare(x, y));
            }
        }

        public static string ExportCompaniesToCsv(List<Company> companies)
        {
            StringBuilder sb = new StringBuilder();

            var companyName = DisplayNameHelper.GetDisplayName(typeof(Company), "CompanyName");
            var yearsInBusiness = DisplayNameHelper.GetDisplayName(typeof(Company), "YearsInBusiness");
            var contactName = DisplayNameHelper.GetDisplayName(typeof(Company), "ContactName");
            var contactPhoneNumber = DisplayNameHelper.GetDisplayName(typeof(Company), "ContactPhoneNumber");
            var contactEmail = DisplayNameHelper.GetDisplayName(typeof(Company), "ContactEmail");

            sb.Append(string.Join(",", $"{companyName}, {yearsInBusiness}, {contactName}, {contactPhoneNumber}, {contactEmail}"));
            sb.Append("\r\n");

            foreach (var company in companies)
            {
                var yearsInBusinessValue = company.YearsInBusiness >= 0 ? company.YearsInBusiness.ToString() : string.Empty;

                sb.Append($"\"{company.CompanyName}\", {yearsInBusinessValue}, {company.ContactName}, {company.ContactPhoneNumber}, {company.ContactEmail}");
                sb.Append("\r\n");
            }

            return sb.ToString();
        }
    }
}
