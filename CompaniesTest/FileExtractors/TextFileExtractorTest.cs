using CompaniesManager.Services.Delimiters;
using CompaniesManager.Services.FileExtractors;
using CompaniesManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace CompaniesTest.FileExtractors
{
    internal class TextFileExtractorTest
    {
        private List<IDelimiter> delimiters;
        private TextFileExtractor extractor;

        private const string testDataFolderPath = @"..\..\..\TestData";

        [SetUp]
        public void Setup()
        {
            delimiters = new List<IDelimiter>
            {
                new CommaDelimiter(5),
                new HashDelimiter(4),
                new HyphenDelimiter(6)
            };

            extractor = new TextFileExtractor(delimiters);
        }

        [Test]
        [TestCase("comma.txt", true)]
        [TestCase("landscape.jpg", false)]
        [TestCase("data.xls", false)]
        [TestCase("anotherData.xlsx", false)]
        [TestCase("animals.png", false)]
        public void TestIfCanExtract(string fileName, bool expected)
        {
            // Arrange

            // Act
            var canExtract = extractor.CanExtract(fileName);

            // Assert
            Assert.AreEqual(expected, canExtract);
        }

        [Test]
        [TestCase("comma.txt", 3)]
        [TestCase("hash.txt", 3)]
        [TestCase("hyphen.txt", 3)]
        [TestCase("empty.txt", 0)]
        public void TestExtractCompaniesFromFile(string fileName, int expected)
        {
            // Arrange
            string filePath = Path.Combine(testDataFolderPath, fileName);
            using var stream = File.OpenRead(filePath);
            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = "text/plain"
            };

            // Act
            var companies = extractor.ExtractCompanies(file);

            // Assert
            Assert.AreEqual(expected, companies.Count);
        }
    }
}
