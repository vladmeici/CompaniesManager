using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CompaniesManager.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Company name")]
        public string CompanyName { get; set; }

        [DisplayName("Contact name")]
        public string ContactName { get; set; }

        [DisplayName("Contact phone number")]
        public string ContactPhoneNumber { get; set; }

        [DisplayName("Years in business")]
        public int YearsInBusiness { get; set; }

        [DisplayName("Contact email")]
        public string ContactEmail { get; set; }

        [DisplayName("Year founded")]
        public int YearFounded { get; set; }
    }
}
