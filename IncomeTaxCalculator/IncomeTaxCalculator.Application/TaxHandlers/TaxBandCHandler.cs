using IncomeTaxCalculator.Domain.Constants;

namespace IncomeTaxCalculator.Application.TaxHandlers;

public class TaxBandCHandler : TaxHandler
{
    public override decimal HandleTaxCalculation(decimal salary)
    {
        decimal tax = 0;
        decimal taxableAmount = salary - (int)TaxBands.TaxBandBUpperBound;

        if (taxableAmount < 0)
        {
            return 0M;
        }
        
        tax += taxableAmount * (int)TaxBandsPercentage.TaxBandCPercentage / 100;

        if (Successor is not null)
        {
            tax += Successor.HandleTaxCalculation(salary);
        }

        return tax;
    }
}