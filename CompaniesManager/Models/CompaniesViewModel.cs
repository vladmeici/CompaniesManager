using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CompaniesManager.Models
{
    public class CompaniesViewModel
    {
        public string CurrentSorter { get; set; }
        public List<Company> Companies { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
