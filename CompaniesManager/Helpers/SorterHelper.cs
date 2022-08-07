using CompaniesManager.Models;
using System.Collections.Generic;

namespace CompaniesManager.Helpers
{
    public static class SorterHelper
    {
        public static IComparer<Company> GetSorter(IEnumerable<IComparer<Company>> sorters, string sorterName)
        {
            IComparer<Company> foundSorter = null;

            foreach (var sorter in sorters)
            {
                if (sorter.GetType().Name.Equals(sorterName))
                {
                    foundSorter = sorter;
                    break;
                }
            }

            return foundSorter;
        }
    }
}
