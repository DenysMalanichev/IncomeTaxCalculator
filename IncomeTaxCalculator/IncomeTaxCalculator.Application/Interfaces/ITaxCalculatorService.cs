using IncomeTaxCalculator.Domain.Entities;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface ITaxCalculatorService
{
    TaxSummary CalculateTaxSummary(decimal grossAnnualSalary);
}