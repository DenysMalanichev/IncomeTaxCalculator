using IncomeTaxCalculator.Application.Exceptions;
using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.TaxHandlers;
using IncomeTaxCalculator.Domain.Contracts.TaxCalculator;
using IncomeTaxCalculator.Domain.Entities;

namespace IncomeTaxCalculator.Application.Services;

public class TaxCalculatorService : ITaxCalculatorService
{
    public TaxSummary CalculateTaxSummary(TaxCalculatorRequest grossAnnualSalary)
    {
        if (grossAnnualSalary.Salary < 0)
        {
            throw new NegativeSalaryException();
        }

        var taxHandler = SetupTaxBoundHandlersChain();

        var annualTaxPaid = taxHandler.HandleTaxCalculation(grossAnnualSalary.Salary);
        
        return new TaxSummary
        {
            GrossAnnualSalary = Math.Round(grossAnnualSalary.Salary, 2),
            GrossMonthlySalary = Math.Round(grossAnnualSalary.Salary / 12M, 2),
            NetAnnualSalary = Math.Round(grossAnnualSalary.Salary - annualTaxPaid, 2),
            NetMonthlySalary = Math.Round((grossAnnualSalary.Salary - annualTaxPaid) / 12M, 2),
            AnnualTaxPaid = Math.Round(annualTaxPaid, 2),
            MonthlyTaxPaid = Math.Round(annualTaxPaid / 12M, 2),
        };
    }

    /// <summary>
    /// Creates handlers for all 3 Tax Bounds and chain them in BoundA, BoundB, BoundC order.
    /// Returns first handler in this chain.
    /// </summary>
    private static TaxHandler SetupTaxBoundHandlersChain()
    {
        var taxBandCHandler = new TaxBandCHandler();
        
        var taxBandBHandler = new TaxBandBHandler();
        taxBandBHandler.SetSuccessor(taxBandCHandler);
            
        var taxBandAHandler = new TaxBandAHandler();
        taxBandAHandler.SetSuccessor(taxBandBHandler);

        return taxBandAHandler;
    }
}