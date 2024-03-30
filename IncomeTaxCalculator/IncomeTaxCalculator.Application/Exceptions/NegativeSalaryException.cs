using System.Diagnostics.CodeAnalysis;

namespace IncomeTaxCalculator.Application.Exceptions;

[ExcludeFromCodeCoverage]
public class NegativeSalaryException : Exception
{
    public NegativeSalaryException()
        : base("Gross Annual Salary can not be a negative number.")
    {
    }
    
    public NegativeSalaryException(string msg, Exception? innerException)
        : base(msg, innerException)
    {
    }
}