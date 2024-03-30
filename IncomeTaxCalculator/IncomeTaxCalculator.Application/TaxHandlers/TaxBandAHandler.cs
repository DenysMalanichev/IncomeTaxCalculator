using IncomeTaxCalculator.Domain.Constants;

namespace IncomeTaxCalculator.Application.TaxHandlers;

public class TaxBandAHandler : TaxHandler
{
    public override decimal HandleTaxCalculation(decimal salary)
    {
        decimal tax = 0;
        decimal taxableAmount = Math.Min((int)TaxBands.TaxBandAUpperBound, salary);

        tax += taxableAmount * (int)TaxBandsPercentage.TaxBandAPercentage / 100;

        if (Successor is not null)
        {
            tax += Successor.HandleTaxCalculation(salary);
        }

        return tax;
    }
}