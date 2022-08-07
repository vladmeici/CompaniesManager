using CompaniesManager.Models;
using CompaniesManager.Services.Sorters;
using NUnit.Framework;

namespace CompaniesTest
{
    internal class SortersTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Apple", " Samsung", -1)]
        [TestCase(" LG ", " Bosh", 1)]
        [TestCase(" Huawei", "Huawei", 0)]
        public void TestCompanyNameSorter(string companyAName, string companyBName, int expected)
        {
            // Arrange
            var companyNameSorter= new CompanyNameSorter();
            var companyA = new Company
            {
                CompanyName = companyAName
            };
            var companyB = new Company
            {
                CompanyName = companyBName
            };

            // Act
            var compareResult = companyNameSorter.Compare(companyA, companyB);

            // Assert
            Assert.AreEqual(expected, compareResult);
        }

        [Test]
        [TestCase("Apple", 20, " Samsung", 10, -1)]
        [TestCase("LG", 10, " Bosh", 20, 1)]
        [TestCase(" Huawei", 20, "Huawei", 20, 0)]
        public void TestYearsAndNameSorter(string companyAName, int companyAYearsInBusiness, string companyBName, int companyBYearsInBusiness, int expected)
        {
            // Arrange
            var companyNameSorter = new CompanyNameSorter();
            var companyA = new Company
            {
                YearsInBusiness = companyAYearsInBusiness,
                CompanyName = companyAName
            };
            var companyB = new Company
            {
                YearsInBusiness = companyBYearsInBusiness,
                CompanyName = companyBName
            };

            // Act
            var compareResult = companyNameSorter.Compare(companyA, companyB);

            // Assert
            Assert.AreEqual(expected, compareResult);
        }
    }
}
