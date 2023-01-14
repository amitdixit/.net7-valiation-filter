using FluentValidation;
using WebApplication1.Models;

namespace WebApplication1.Endpoints;


public static class CustomerEndpoints
{
    public static void AddCustomerEndpoints(this WebApplication app)
    {
        app.MapPost("/saveCustomer", SaveCustomer)
            .WithName("CreateCutomer")
        .WithOpenApi();
    }

    public static async Task<IResult> SaveCustomer(ILogger<WeatherForecast> logger, IValidator<CustomerRequest> validator, CustomerRequest customer)
    {
        var result = await validator.ValidateAsync(customer);

        if (!result.IsValid)
        {
            return Results.ValidationProblem(result.ToDictionary());//Errors
        }

        await Task.CompletedTask;


        logger.LogInformation("Customer Saved ");

        return Results.Ok("Customer Created");
    }
}
