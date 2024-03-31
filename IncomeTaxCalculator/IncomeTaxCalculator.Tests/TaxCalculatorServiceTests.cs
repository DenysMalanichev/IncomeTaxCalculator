using IncomeTaxCalculator.Application.Exceptions;
using IncomeTaxCalculator.Application.Services;
using IncomeTaxCalculator.Domain.Contracts.TaxCalculator;

namespace IncomeTaxCalculator.Tests;

public class TaxCalculatorServiceTests
{
    [Fact]
    public void CalculateTaxSummary_GrossAnnualSalaryIsNegative_ThrowsNegativeSalaryException()
    {
        // Arrange
        var taxCalculatorService = new TaxCalculatorService();
        TaxCalculatorRequest negativeSalary = new() { Salary = -100 };
        
        // Act & Assert
        Assert.Throws<NegativeSalaryException>(() => taxCalculatorService.CalculateTaxSummary(negativeSalary));
    }
    
    [Theory]
    [InlineData(100_000, 35_000)]
    [InlineData(40_000, 11_000)]
    [InlineData(10_000, 1_000)]
    [InlineData(5_000, 0)]
    [InlineData(0.1, 0)]
    [InlineData(0, 0)]
    public void CalculateTaxSummary_GrossAnnualSalaryValid_ReturnsCorrespondingTax(
        decimal grossAnnualSalary,
        decimal expectedTax)
    {
        // Arrange
        TaxCalculatorRequest salary = new() { Salary = grossAnnualSalary };
        var taxCalculatorService = new TaxCalculatorService();
        
        // Act
        var taxResult = taxCalculatorService.CalculateTaxSummary(salary);
        
        // Assert
        Assert.Equal(expectedTax, taxResult.AnnualTaxPaid);
    }
}