using IncomeTaxCalculator.Application.TaxHandlers;
using IncomeTaxCalculator.Domain.Constants;
using Moq;

namespace IncomeTaxCalculator.Tests.TaxBandHandlersTests;

public class TaxBandCHandlerTests
{
    [Fact]
    public void HandleTaxCalculation_SalaryWithinTaxBandC_CalculatesCorrectTax()
    {
        // Arrange
        var taxCalculator = new TaxBandCHandler();
        const decimal salary = 30000;
        const decimal expectedTax = (salary - (int)TaxBands.TaxBandBUpperBound) * (int)TaxBandsPercentage.TaxBandCPercentage / 100;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTax, tax);
    }

    [Fact]
    public void HandleTaxCalculation_SalaryBelowTaxBandCThreshold_ReturnsZero()
    {
        // Arrange
        var taxCalculator = new TaxBandCHandler();
        const decimal salaryBelowTaxBand = 19000;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salaryBelowTaxBand);

        // Assert
        Assert.Equal(0M, tax);
    }

    [Fact]
    public void HandleTaxCalculation_SalaryExceedingTaxBandCWithSuccessor_CalculatesCorrectTaxIncludingSuccessor()
    {
        // Arrange
        var successorMock = new Mock<TaxHandler>();
        const decimal additionalTaxFromMockedSuccessor = 1000M;
        successorMock.Setup(s => s.HandleTaxCalculation(It.IsAny<decimal>()))
            .Returns(additionalTaxFromMockedSuccessor);
        
        var taxCalculator = new TaxBandCHandler();
        taxCalculator.SetSuccessor(successorMock.Object);

        const decimal salaryExceedsTaxBand = 50000;
        const decimal expectedTaxFromC = (salaryExceedsTaxBand - (int)TaxBands.TaxBandBUpperBound) *
            (int)TaxBandsPercentage.TaxBandCPercentage / 100 + additionalTaxFromMockedSuccessor;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salaryExceedsTaxBand);

        // Assert
        Assert.Equal(expectedTaxFromC, tax);
    }

    [Fact]
    public void HandleTaxCalculation_NoSuccessorAndSalaryExceedingTaxBandC_ReturnsCorrectTaxForBandCOnly()
    {
        // Arrange
        var taxCalculator = new TaxBandCHandler();
        const decimal salary = 50000;
        const decimal expectedTax = (salary - (int)TaxBands.TaxBandBUpperBound) * (int)TaxBandsPercentage.TaxBandCPercentage / 100;

        // Act
        var tax = taxCalculator.HandleTaxCalculation(salary);

        // Assert
        Assert.Equal(expectedTax, tax);
    }
}