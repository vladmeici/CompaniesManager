using CompaniesManager.Services.Interfaces;
using System.Collections.Generic;

namespace CompaniesManager.Helpers
{
    public static class DelimitersHelper
    {
        public static IDelimiter GetDelimiter(IEnumerable<IDelimiter> delimiters, string line)
        {
            IDelimiter foundDelimiter = null;

            foreach (var delimiter in delimiters)
            {
                if (delimiter.IsPresentIn(line))
                {
                    foundDelimiter = delimiter;
                }
            }

            return foundDelimiter;
        }
    }
}
