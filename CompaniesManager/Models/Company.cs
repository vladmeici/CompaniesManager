using System.ComponentModel.DataAnnotations;

namespace CompaniesManager.Models
{
    public class Company
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactPhoneNumber { get; set; }

        public uint YearsInBusiness { get; set; }

        public string ContactEmail { get; set; }

        public uint YearFounded { get; set; }
    }
}
