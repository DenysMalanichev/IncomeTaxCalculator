namespace IncomeTaxCalculator.Application.TaxHandlers;

public abstract class TaxHandler
{
    protected TaxHandler? Successor;
    
    public void SetSuccessor(TaxHandler? successor)
    {
        Successor = successor;
    }

    public abstract decimal HandleTaxCalculation(decimal salary);
}