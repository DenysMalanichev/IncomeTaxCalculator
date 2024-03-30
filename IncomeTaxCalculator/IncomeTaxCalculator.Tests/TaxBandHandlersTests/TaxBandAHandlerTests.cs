using IncomeTaxCalculator.Application.TaxHandlers;
using IncomeTaxCalculator.Domain.Constants;
using Moq;

namespace IncomeTaxCalculator.Tests.TaxBandHandlersTests;

public class TaxBandAHandlerTests
{
    [Fact]
    public void HandleTaxCalculation_SalaryWithinTaxBandA_CalculatesCorrectTax()
    {
        // Arrange
        var taxCalculator = new TaxBandAHandler();
        const decimal salary = 4000;
        const decimal expectedTax = salary * (decimal)TaxBandsPercentage.TaxBandAPercentage / 100;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTax, tax);
    }

    [Fact]
    public void HandleTaxCalculation_SalaryExceedingTaxBandAWithSuccessor_CalculatesCorrectTax()
    {
        // Arrange
        var successorMock = new Mock<TaxHandler>();
        const decimal additionalTaxFromMockedSuccessor = 500;
        successorMock.Setup(s => s.HandleTaxCalculation(It.IsAny<decimal>()))
            .Returns(additionalTaxFromMockedSuccessor);
        
        var taxCalculator = new TaxBandAHandler();
        taxCalculator.SetSuccessor(successorMock.Object);

        const decimal salary = 10000;
        const decimal expectedTaxFromA = 
            (int)TaxBands.TaxBandAUpperBound * (decimal)TaxBandsPercentage.TaxBandAPercentage / 100 + additionalTaxFromMockedSuccessor;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTaxFromA, tax);
    }

    [Fact]
    public void HandleTaxCalculation_NoSuccessorAndSalaryExceedingTaxBandA_ReturnsCorrectTax()
    {
        // Arrange
        var taxCalculator = new TaxBandAHandler();
        const decimal salary = (decimal)TaxBands.TaxBandAUpperBound + 1000;
        const decimal expectedTax = (decimal)TaxBands.TaxBandAUpperBound * (decimal)TaxBandsPercentage.TaxBandAPercentage / 100;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTax, tax);
    }
}