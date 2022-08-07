using CompaniesManager.Helpers;
using CompaniesManager.Models;
using CompaniesManager.Services.Comparers;
using CompaniesManager.Services.Delimiters;
using CompaniesManager.Services.FileExtractors;
using CompaniesManager.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CompaniesTest
{
    internal class HelperTest
    {
        private List<IDelimiter> delimiters;
        private List<Company> companies;
        private List<IComparer<Company>> comparers;

        [SetUp]
        public void Setup()
        {
            delimiters = new List<IDelimiter>
            {
                new CommaDelimiter(5),
                new HashDelimiter(4),
                new HyphenDelimiter(6)
            };

            companies = new List<Company>
            {
                new Company { Id = Guid.NewGuid(), CompanyName = "Company A", ContactEmail = "companyA@email.com", ContactName = "B Mark", ContactPhoneNumber = "001", YearFounded = 1940, YearsInBusiness = 7 },
                new Company { Id = Guid.NewGuid(), CompanyName = "Company B", ContactEmail = "companyB@email.com", ContactName = "D Sara", ContactPhoneNumber = "002", YearFounded = 1960, YearsInBusiness = 20 },
                new Company { Id = Guid.NewGuid(), CompanyName = "Company C", ContactEmail = "companyC@email.com", ContactName = "A John", ContactPhoneNumber = "003", YearFounded = 1985, YearsInBusiness = 20 },
                new Company { Id = Guid.NewGuid(), CompanyName = "Company D", ContactEmail = "companyD@email.com", ContactName = "C Dan", ContactPhoneNumber = "004", YearFounded = 2005, YearsInBusiness = 5 }
            };

            comparers = new List<IComparer<Company>>
            {
                new CompanyNameComparer(),
                new ContactNameComparer(),
                new YearsAndNameComparer()
            };
        }

        [Test]
        [TestCase("comma.txt", true)]
        [TestCase("landscape.jpg", false)]
        [TestCase("data.xls", true)]
        [TestCase("anotherData.xlsx", true)]
        [TestCase("animals.png", false)]
        public void TestCompaniesExtractorHelper(string fileName, bool expected)
        {
            // Arrange
            var extractors = new List<ICompaniesExtractor>
            {
                new TextFileExtractor(delimiters),
                new ExcelFileExtractor()
            };

            // Act
            var extractor = CompaniesExtractorHelper.GetCompaniesExtractor(extractors, fileName);

            // Assert
            Assert.AreEqual(expected, extractor != null);
        }

        [Test]
        [TestCase("Company A, John Smith, (301) 111-1234, 9, jsmith@rapidadvance.info", true)]
        [TestCase("Company JK#1900#Jimmy Green#+1 (301) 667-1234", true)]
        [TestCase("Creative Name Corp.-2000-+13016674477-bstone@rapidadvance.info-Bob-Stone", true)]
        [TestCase("Creative Name Corp.%2000-+13016674477%bstone@rapidadvance.info%Bob%Stone", false)]
        [TestCase("", false)]
        public void TestDelimitersHelper(string line, bool expected)
        {
            // Arrange

            // Act
            var delimiter = DelimitersHelper.GetDelimiter(delimiters, line);

            // Assert
            Assert.AreEqual(expected, delimiter != null);
        }

        [Test]
        [TestCase("CompanyNameComparer", true)]
        [TestCase("ContactNameComparer", true)]
        [TestCase("YearsAndNameComparer", true)]
        [TestCase("AnotherTypeOfComparer", false)]
        [TestCase("", false)]
        public void TestComparerHelper(string comparerName, bool expected)
        {
            // Arrange

            // Act
            var comparer = ComparerHelper.GetComparer(comparers, comparerName);

            // Assert
            Assert.AreEqual(expected, comparer != null);
        }

        [Test]
        [TestCase("CompanyNameComparer", "Company A", "Company D")]
        [TestCase("ContactNameComparer", "Company C", "Company B")]
        [TestCase("YearsAndNameComparer", "Company B", "Company D")]
        public void TestCompaniesSortHelper(string comparerName, string firstCompanyName, string lastCompanyName)
        {
            // Arrange

            // Act
            CompaniesHelper.SortCompanies(companies, comparers, comparerName);

            // Assert
            Assert.AreEqual(companies[0].CompanyName, firstCompanyName);
            Assert.AreEqual(companies[companies.Count - 1].CompanyName, lastCompanyName);
        }

        [Test]
        public void TestCompaniesExportHelper()
        {
            // Arrange
            var expctedExportString = "Company name, Years in business, Contact name, Contact phone number, Contact email\r\n\"Company A\", 7, B Mark, 001, companyA@email.com\r\n\"Company B\", 20, D Sara, 002, companyB@email.com\r\n\"Company C\", 20, A John, 003, companyC@email.com\r\n\"Company D\", 5, C Dan, 004, companyD@email.com\r\n";

            // Act
            var exportedString = CompaniesHelper.ExportCompaniesToCsv(companies);

            // Assert
            Assert.AreEqual(expctedExportString, exportedString);
        }
    }
}
