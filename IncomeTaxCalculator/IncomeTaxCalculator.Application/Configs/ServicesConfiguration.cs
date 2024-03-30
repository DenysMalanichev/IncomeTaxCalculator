using System.Diagnostics.CodeAnalysis;
using IncomeTaxCalculator.Application.Interfaces;
using IncomeTaxCalculator.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeTaxCalculator.Application.Configs;

public static class ServicesConfiguration
{
    [ExcludeFromCodeCoverage]
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<ITaxCalculatorService, TaxCalculatorService>();
    }
}