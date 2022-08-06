using CompaniesManager.Helpers;
using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace CompaniesManager.Services.FileExtractors
{
    public class TextFileExtractor : ICompaniesExtractor
    {
        private readonly IEnumerable<IDelimiter> _delimiters;

        private List<string> _lines;

        private IDelimiter _delimiter;

        public TextFileExtractor(IEnumerable<IDelimiter> delimiters)
        {
            _delimiters = delimiters;
        }

        private void ReadLines(IFormFile file)
        {
            _lines = new List<string>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    _lines.Add(reader.ReadLine());
            }
        }

        private void SetDelimiter()
        {
            if (_lines.Count > 0)
            {
                _delimiter = DelimitersHelper.GetDelimiter(_delimiters, _lines[0]);
            }
        }

        public bool CanExtract(string fileExtension)
        {
            return fileExtension.EndsWith("txt");
        }

        public List<Company> ExtractCompanies(IFormFile file)
        {
            var companies = new List<Company>();

            ReadLines(file);
            SetDelimiter();

            foreach (var line in _lines)
            {
                var company = _delimiter.ExtractCompanyFrom(line);

                if (company != null)
                {
                    companies.Add(company);
                }
            }

            return companies;
        }
    }
}
