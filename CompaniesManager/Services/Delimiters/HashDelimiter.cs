using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using System;

namespace CompaniesManager.Services.Delimiters
{
    public class HashDelimiter : IDelimiter
    {
       public string DelimiterValue { get; }

        public uint SplitLength { get; }

        public HashDelimiter(uint dataToExtract)
        {
            DelimiterValue = "#";
            SplitLength = dataToExtract;
        }

        public Company ExtractCompanyFrom(string line)
        {
            var lineValues = line.Split(DelimiterValue);

            if (lineValues.Length == SplitLength)
            {
                return new Company
                {
                    Id = Guid.NewGuid(),
                    CompanyName = lineValues[0],
                    YearsInBusiness = -1,
                    YearFounded = Convert.ToInt32(lineValues[1]),
                    ContactName = lineValues[2],
                    ContactPhoneNumber = lineValues[3]
                };
            }

            return null;
        }


        public bool IsPresentIn(string line)
        {
            return line.Contains(DelimiterValue) && line.Split(DelimiterValue).Length == SplitLength;
        }
    }
}
