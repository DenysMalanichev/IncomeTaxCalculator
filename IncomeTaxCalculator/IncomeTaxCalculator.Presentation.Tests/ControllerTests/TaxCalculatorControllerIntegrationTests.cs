using System.Net.Http.Json;
using IncomeTaxCalculator.Domain.Contracts.TaxCalculator;
using IncomeTaxCalculator.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace IncomeTaxCalculator.Presentation.Tests.ControllerTests;

public class TaxCalculatorControllerIntegrationTests : WebApplicationFactory<Program>
{
    private readonly HttpClient _client;

    public TaxCalculatorControllerIntegrationTests()
    {
        _client = this.CreateClient();
    }

    [Fact]
    public async Task CalculateTaxes_Post_ReturnsCorrectResponse()
    {
        // Arrange
        var expectedTaxSummary = new TaxSummary
        {
            GrossAnnualSalary = 40_000.00M,
            GrossMonthlySalary = 3333.33M,
            NetAnnualSalary = 29_000M,
            NetMonthlySalary = 2416.67M,
            AnnualTaxPaid = 11_000.00M,
            MonthlyTaxPaid = 916.67M,
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/TaxCalculator", new TaxCalculatorRequest { Salary = 40_000 });

        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();

        // Assert
        var actualTaxSummary = JsonConvert.DeserializeObject<TaxSummary>(responseString);
        Assert.NotNull(actualTaxSummary);
        
        Assert.Equal(expectedTaxSummary.GrossAnnualSalary, actualTaxSummary.GrossAnnualSalary);
        Assert.Equal(expectedTaxSummary.GrossMonthlySalary, actualTaxSummary.GrossMonthlySalary);
        Assert.Equal(expectedTaxSummary.NetAnnualSalary, actualTaxSummary.NetAnnualSalary);
        Assert.Equal(expectedTaxSummary.NetMonthlySalary, actualTaxSummary.NetMonthlySalary);
        Assert.Equal(expectedTaxSummary.AnnualTaxPaid, actualTaxSummary.AnnualTaxPaid);
        Assert.Equal(expectedTaxSummary.MonthlyTaxPaid, actualTaxSummary.MonthlyTaxPaid);
    }
}