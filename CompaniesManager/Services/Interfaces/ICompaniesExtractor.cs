using CompaniesManager.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CompaniesManager.Services.Interfaces
{
    public interface ICompaniesExtractor
    {
        bool CanExtract(string fileExtension);
        List<Company> ExtractCompanies(IFormFile file);
    }
}
