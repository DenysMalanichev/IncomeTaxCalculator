using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Controllers;
using IncomeTaxCalculator.Domain.Entities;
using Moq;

namespace IncomeTaxCalculator.Presentation.Tests.ControllerTests;

public class TaxCalculatorControllerTests
{
    [Fact]
    public void CalculateTaxes_WithValidSalary_ReturnsCorrectTaxSummary()
    {
        // Arrange
        var mockService = new Mock<ITaxCalculatorService>();
        const decimal grossAnnualSalary = 40_000;
        var expectedTaxSummary = new TaxSummary
        {
            GrossAnnualSalary = 40_000.00M,
            GrossMonthlySalary = 3333.333M,
            NetAnnualSalary = 29_000M,
            NetMonthlySalary = 2416.67M,
            AnnualTaxPaid = 11_000.00M,
            MonthlyTaxPaid = 916.67M,
        };
        mockService.Setup(s => s.CalculateTaxSummary(grossAnnualSalary)).Returns(expectedTaxSummary);

        var controller = new TaxCalculatorController(mockService.Object);

        // Act
        var result = controller.CalculateTexes(grossAnnualSalary);

        // Assert
        Assert.IsType<TaxSummary>(result);
        Assert.Equal(expectedTaxSummary.GrossAnnualSalary, result.GrossAnnualSalary);
        Assert.Equal(expectedTaxSummary.GrossMonthlySalary, result.GrossMonthlySalary);
        Assert.Equal(expectedTaxSummary.NetAnnualSalary, result.NetAnnualSalary);
        Assert.Equal(expectedTaxSummary.NetMonthlySalary, result.NetMonthlySalary);
        Assert.Equal(expectedTaxSummary.AnnualTaxPaid, result.AnnualTaxPaid);
        Assert.Equal(expectedTaxSummary.MonthlyTaxPaid, result.MonthlyTaxPaid);
    }
}