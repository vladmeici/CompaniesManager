using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using System;

namespace CompaniesManager.Services.Delimiters
{
    public class HyphenDelimiter : IDelimiter
    {
        public string DelimiterValue { get; }

        public uint SplitLength { get; }

        public HyphenDelimiter(uint dataToExtract)
        {
            DelimiterValue = "-";
            SplitLength = dataToExtract;
        }

        public Company ExtractCompanyFrom(string line)
        {
            var lineValues = line.Split(DelimiterValue);


            if (lineValues.Length == SplitLength)
            {

                var companyName = lineValues[0];
                var yearFounded = lineValues[1];
                var contactPhoneNumber = lineValues[2];
                var contactEmail = lineValues[3];
                var contactName = lineValues[4] + " " + lineValues[5];

                return new Company
                {
                    Id = Guid.NewGuid(),
                    CompanyName = companyName,
                    YearsInBusiness = -1,
                    YearFounded = Convert.ToInt32(yearFounded),
                    ContactPhoneNumber = contactPhoneNumber,
                    ContactEmail = contactEmail,
                    ContactName = contactName
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
