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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompaniesManager.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEnumerable<IComparer<Company>> _sorters;
        private readonly IEnumerable<ICompaniesExtractor> _extractors;
        private readonly List<SelectListItem> _sortMethods;

        private const string exportFileName = "Companies.csv";

        public CompaniesController(ApplicationDbContext db, IEnumerable<IComparer<Company>> sorters, IEnumerable<ICompaniesExtractor> extractors)
        {
            _db = db;
            _sorters = sorters;
            _extractors = extractors;

            _sortMethods = new List<SelectListItem>();

            foreach (var sorter in _sorters)
            {
                var sorterName = sorter.GetType().Name;
                var sorterDisplayName = DisplayNameHelper.GetDisplayName(sorter.GetType());
                _sortMethods.Add(new SelectListItem { Value = sorterName, Text = sorterDisplayName });
            }
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var companies = await _db.Companies.ToListAsync();
                return View(new CompaniesViewModel { Companies = companies, CurrentSorter = string.Empty, SortMethods = _sortMethods  });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> Sort(string sorterName)
        {
            try
            {
                var companies = await _db.Companies.ToListAsync();

                CompaniesHelper.SortCompanies(companies, _sorters, sorterName);

                return View("Index", new CompaniesViewModel { Companies = companies, CurrentSorter = sorterName, SortMethods = _sortMethods });
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        public async Task<IActionResult> Export(string sorterName)
        {
            try
            {
                var companies = await _db.Companies.ToListAsync();

                if (!string.IsNullOrEmpty(sorterName))
                {
                    CompaniesHelper.SortCompanies(companies, _sorters, sorterName);
                }

                var exportedString = CompaniesHelper.ExportCompaniesToCsv(companies);

                return File(Encoding.UTF8.GetBytes(exportedString), "text/csv", exportFileName);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Import(CompaniesViewModel model)
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
