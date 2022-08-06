using CompaniesManager.Models;
using CompaniesManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CompaniesManager.Services.FileExtractors
{
    public class ExcelFileExtractor : ICompaniesExtractor
    {
        public bool CanExtract(string fileExtension)
        {
            return fileExtension.EndsWith("xls") || fileExtension.EndsWith("xlsx");
        }

        public List<Company> ExtractCompanies(IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
