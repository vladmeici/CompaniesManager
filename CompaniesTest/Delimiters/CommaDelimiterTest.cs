using CompaniesManager.Services.Delimiters;
using NUnit.Framework;

namespace CompaniesTest.Delimiters
{
    internal class CommaDelimiterTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("Company A, John Smith, (301) 111-1234, 9, jsmith@rapidadvance.info", true)]
        [TestCase("Company JK#1900#Jimmy Green#+1 (301) 667-1234", false)]
        [TestCase("Creative Name Corp.-2000-+13016674477-bstone@rapidadvance.info-Bob-Stone", false)]
        [TestCase("", false)]
        [TestCase("Company A, John Smith, (301)", false)]
        public void TestIfLineFollowsCommaDelimterRules(string line, bool expected)
        {
            // Arrange
            var commaDelimter = new CommaDelimiter(5);

            // Act
            var actual = commaDelimter.IsPresentIn(line);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("Company A, John Smith, (301) 111-1234, 9, jsmith@rapidadvance.info", "Company A")]
        [TestCase("", null)]
        [TestCase("Company A# John Smith# (301)", null)]
        public void TestIfCompanyIsExtractedFromLine(string line, string expected)
        {
            // Arrange
            var commaDelimter = new CommaDelimiter(5);

            // Act
            var actual = commaDelimter.ExtractCompanyFrom(line);

            // Assert
            Assert.AreEqual(expected, actual?.CompanyName);
        }
    }
}
