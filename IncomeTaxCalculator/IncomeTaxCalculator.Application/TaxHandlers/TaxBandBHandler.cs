using IncomeTaxCalculator.Domain.Constants;

namespace IncomeTaxCalculator.Application.TaxHandlers;

public class TaxBandBHandler : TaxHandler
{
    public override decimal HandleTaxCalculation(decimal salary)
    {
        decimal tax = 0;
        decimal taxableAmount = Math.Min(
            (int)TaxBands.TaxBandBUpperBound - (int)TaxBands.TaxBandAUpperBound,
            salary - (int)TaxBands.TaxBandAUpperBound);

        if (taxableAmount < 0)
        {
            return 0M;
        }

        tax += taxableAmount * (int)TaxBandsPercentage.TaxBandBPercentage / 100;

        if (Successor is not null)
        {
            tax += Successor.HandleTaxCalculation(salary);
        }

        return tax;
    }
}