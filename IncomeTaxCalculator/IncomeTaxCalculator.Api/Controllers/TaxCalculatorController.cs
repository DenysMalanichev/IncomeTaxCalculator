using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Domain.Contracts.TaxCalculator;
using IncomeTaxCalculator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IncomeTaxCalculator.Controllers;

[Route("api/[controller]")]
public class TaxCalculatorController : ControllerBase
{
    private readonly ITaxCalculatorService _taxCalculatorService;

    public TaxCalculatorController(ITaxCalculatorService taxCalculatorService)
    {
        _taxCalculatorService = taxCalculatorService;
    }

    [HttpPost]
    public TaxSummary CalculateTexes([FromBody] TaxCalculatorRequest grossAnnualSalary)
    {
        return _taxCalculatorService.CalculateTaxSummary(grossAnnualSalary);
    }
}