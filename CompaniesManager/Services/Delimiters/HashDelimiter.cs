using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using System;

namespace CompaniesManager.Services.Delimiters
{
    public class HashDelimiter : IDelimiter
    {
       public string DelimiterValue { get; }

        public uint SplitLength { get; }

        public HashDelimiter()
        {
            DelimiterValue = "#";
            SplitLength = 4;
        }

        public Company ExtractCompanyFrom(string line)
        {
            var lineValues = line.Split(DelimiterValue);

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


        public bool IsPresentIn(string line)
        {
            return line.Contains(DelimiterValue) && line.Split(DelimiterValue).Length == SplitLength;
        }
    }
}
