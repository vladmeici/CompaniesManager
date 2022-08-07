using CompaniesManager.Services.Delimiters;
using NUnit.Framework;

namespace CompaniesTest.Delimiters
{
    internal class HashDelimiterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Company A, John Smith, (301) 111-1234, 9, jsmith@rapidadvance.info", false)]
        [TestCase("Company JK#1900#Jimmy Green#+1 (301) 667-1234", true)]
        [TestCase("Creative Name Corp.-2000-+13016674477-bstone@rapidadvance.info-Bob-Stone", false)]
        [TestCase("", false)]
        [TestCase("Company A, John Smith, (301)", false)]
        public void TestIfLineFollowsHashDelimterRules(string line, bool expected)
        {
            // Arrange
            var hashDelimter = new HashDelimiter(4);

            // Act
            var actual = hashDelimter.IsPresentIn(line);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("Company JK#1900#Jimmy Green#+1 (301) 667-1234", "Company JK")]
        [TestCase("", null)]
        [TestCase("Company A, John Smith, (301)", null)]
        public void TestIfCompanyIsExtractedFromLine(string line, string expected)
        {
            // Arrange
            var hashDelimter = new HashDelimiter(4);

            // Act
            var actual = hashDelimter.ExtractCompanyFrom(line);

            // Assert
            Assert.AreEqual(expected, actual?.CompanyName);
        }
    }
}
