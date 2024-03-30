using IncomeTaxCalculator.Application.TaxHandlers;
using IncomeTaxCalculator.Domain.Constants;
using Moq;

namespace IncomeTaxCalculator.Tests.TaxBandHandlersTests;

public class TaxBandBHandlerTests
{
    [Fact]
    public void HandleTaxCalculation_SalaryWithinTaxBandB_CalculatesCorrectTax()
    {
        // Arrange
        var taxCalculator = new TaxBandBHandler();
        const decimal salary = 12000M;
        const decimal expectedTax = (salary - (int)TaxBands.TaxBandAUpperBound) * (int)TaxBandsPercentage.TaxBandBPercentage / 100;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTax, tax);
    }

    [Fact]
    public void HandleTaxCalculation_SalaryExceedingTaxBandBWithSuccessor_CalculatesCorrectTaxIncludingSuccessor()
    {
        // Arrange
        var successorMock = new Mock<TaxHandler>();
        const decimal additionalTaxFromMockedSuccessor = 2000M;
        successorMock.Setup(s => s.HandleTaxCalculation(It.IsAny<decimal>()))
            .Returns(additionalTaxFromMockedSuccessor);
        
        var taxCalculator = new TaxBandBHandler();
        taxCalculator.SetSuccessor(successorMock.Object);

        const decimal salary = 25000M;
        const decimal expectedTaxFromB = 
            ((int)TaxBands.TaxBandBUpperBound - (int)TaxBands.TaxBandAUpperBound) * 
            (int)TaxBandsPercentage.TaxBandBPercentage / 100.0M + additionalTaxFromMockedSuccessor;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTaxFromB, tax);
    }

    [Fact]
    public void HandleTaxCalculation_SalaryBelowTaxBandB_ReturnsZero()
    {
        // Arrange
        var taxCalculator = new TaxBandBHandler();
        const decimal salaryBelowBandBounds = 5000;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salaryBelowBandBounds);

        // Assert
        Assert.Equal(0M, tax);
    }

    [Fact]
    public void HandleTaxCalculation_NoSuccessorAndSalaryExceedingTaxBandB_ReturnsCorrectTaxForBandBOnly()
    {
        // Arrange
        var taxCalculator = new TaxBandBHandler();
        const decimal salaryExceedsBandBounds = 25000;
        const decimal expectedTax = ((int)TaxBands.TaxBandBUpperBound - (int)TaxBands.TaxBandAUpperBound) *
            (int)TaxBandsPercentage.TaxBandBPercentage / 100.0M;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salaryExceedsBandBounds);

        // Assert
        Assert.Equal(expectedTax, tax);
    }
}