using CompaniesManager.Models;
using System.Collections.Generic;

namespace CompaniesManager.Helpers
{
    public static class ComparerHelper
    {
        public static IComparer<Company> GetComparer(IEnumerable<IComparer<Company>> comparers, string comparerName)
        {
            IComparer<Company> foundComparer = null;

            foreach (var comparer in comparers)
            {
                if (comparer.GetType().Name.Equals(comparerName))
                {
                    foundComparer = comparer;
                    break;
                }
            }

            return foundComparer;
        }
    }
}
