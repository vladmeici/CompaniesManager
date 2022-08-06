using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CompaniesManager.Models
{
    public class ImportViewModel
    {
        public List<IFormFile> Files { get; set; }
    }
}
