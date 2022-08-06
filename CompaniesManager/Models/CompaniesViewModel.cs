using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CompaniesManager.Models
{
    public class CompaniesViewModel
    {
        public string CurrentComparer { get; set; }
        public List<Company> Companies { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
