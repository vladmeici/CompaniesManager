using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using System;

namespace CompaniesManager.Services.Delimiters
{
    public class CommaDelimiter : IDelimiter
    {
        public string DelimiterValue { get; }

        public uint SplitLength { get; }

        public CommaDelimiter()
        {
            DelimiterValue = ",";
            SplitLength = 5;
        }

        public Company ExtractCompanyFrom(string line)
        {
            var lineValues = line.Split(DelimiterValue);

            return new Company
            {
                Id = Guid.NewGuid(),
                CompanyName = lineValues[0],
                ContactName = lineValues[1],
                ContactPhoneNumber = lineValues[2],
                YearsInBusiness = Convert.ToInt32(lineValues[3]),
                ContactEmail = lineValues[4],
            };
        }


        public bool IsPresentIn(string line)
        {
            return line.Contains(DelimiterValue) && line.Split(DelimiterValue).Length == SplitLength;
        }
    }
}
