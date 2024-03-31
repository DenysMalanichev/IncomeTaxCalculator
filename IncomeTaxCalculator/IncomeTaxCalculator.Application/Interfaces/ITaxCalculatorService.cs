using IncomeTaxCalculator.Domain.Contracts.TaxCalculator;
using IncomeTaxCalculator.Domain.Entities;

namespace IncomeTaxCalculator.Application.Interfaces;

public interface ITaxCalculatorService
{
    TaxSummary CalculateTaxSummary(TaxCalculatorRequest grossAnnualSalary);
}