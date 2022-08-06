using CompaniesManager.Services.Interfaces;
using System.Collections.Generic;

namespace CompaniesManager.Helpers
{
    public static class CompaniesExtractorHelper
    {
        public static ICompaniesExtractor GetCompaniesExtractor(IEnumerable<ICompaniesExtractor> extractors, string fileExtension)
        {
            ICompaniesExtractor foundExtractor = null;

            foreach (var extractor in extractors)
            {
                if (extractor.CanExtract(fileExtension))
                {
                    foundExtractor = extractor;
                    break;
                }
            }

            return foundExtractor;
        }
    }
}
