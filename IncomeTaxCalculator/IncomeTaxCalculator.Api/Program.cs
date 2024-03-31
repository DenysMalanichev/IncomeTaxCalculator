using IncomeTaxCalculator.Application.Configs;
using IncomeTaxCalculator.Configs;
using IncomeTaxCalculator.Middleware;

namespace IncomeTaxCalculator;

public class Program
{
    private const string AllowFrontEndSpecificOrigins = "_frontEndSpecificOrigins";
    
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        builder.AddCorsPolicy(AllowFrontEndSpecificOrigins);

        builder.Services.AddCustomServices();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseCors(AllowFrontEndSpecificOrigins);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}