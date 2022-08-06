using CompaniesManager.Models;

namespace CompaniesManager.Services.Interfaces
{
    public interface IDelimiter
    {
        string DelimiterValue { get; }
        uint SplitLength { get; }

        Company ExtractCompanyFrom(string line);

        bool IsPresentIn(string line);
    }
}
