using System.Diagnostics.CodeAnalysis;

namespace IncomeTaxCalculator.Domain.Entities;

[ExcludeFromCodeCoverage]
public class TaxSummary
{
    public decimal GrossAnnualSalary { get; set; }

    public decimal GrossMonthlySalary { get; set; }

    public decimal NetAnnualSalary { get; set; }

    public decimal NetMonthlySalary { get; set; }

    public decimal AnnualTaxPaid { get; set; }

    public decimal MonthlyTaxPaid { get; set; }
}