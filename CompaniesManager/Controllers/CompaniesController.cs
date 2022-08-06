using CompaniesManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using CompaniesManager.Helpers;
using System;
using System.Diagnostics;
using CompaniesManager.Services.Interfaces;

namespace CompaniesManager.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEnumerable<IComparer<Company>> _comparers;
        private readonly IEnumerable<ICompaniesExtractor> _extractors;

        public CompaniesController(ApplicationDbContext db, IEnumerable<IComparer<Company>> comparers, IEnumerable<ICompaniesExtractor> extractors)
        {
            _db = db;
            _comparers = comparers;
            _extractors = extractors;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _db.Companies.ToListAsync();
            return View(new CompaniesViewModel { Companies = companies, CurrentComparer = string.Empty });
        }

        public async Task<IActionResult> Sort(string comparerName)
        {
            var companies = await _db.Companies.ToListAsync();

            CompaniesHelper.SortCompanies(companies, _comparers, comparerName);
            ViewData["currentSort"] = comparerName;

            return View("Index", new CompaniesViewModel { Companies = companies, CurrentComparer = comparerName});
        }

        public async Task<FileResult> Export(string comparerName)
        {
            var companies = await _db.Companies.ToListAsync();

            if(!string.IsNullOrEmpty(comparerName))
            {
                CompaniesHelper.SortCompanies(companies, _comparers, comparerName);
            }

            var exportedString = CompaniesHelper.ExportCompaniesToCsv(companies);

            return File(Encoding.UTF8.GetBytes(exportedString), "text/csv", "Companies.csv");
        }

        [HttpPost]
        public IActionResult Import(ImportViewModel model)
        {
            try
            {
                if (ModelState.IsValid && model.Files != null && model.Files.Count > 0)
                {
                    foreach (var file in model.Files)
                    {
                        var extractor = CompaniesExtractorHelper.GetCompaniesExtractor(_extractors, file.FileName);

                        if (extractor != null)
                        {
                            var extractedCompanies = extractor.ExtractCompanies(file);
                            foreach (var company in extractedCompanies)
                            {
                                _db.Add(company);
                            }
                        }
                    }
                    _db.SaveChanges();
                }
                return RedirectToAction("Index", "Companies");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }
    }
}
