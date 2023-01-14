using FluentValidation;
using WebApplication1.Filter;
using WebApplication1.Models;

namespace WebApplication1.Endpoints;


public static class CustomerEndpointsV2
{
    public static void AddCustomerEndpointsV2(this WebApplication app)
    {
        app.MapPost("/saveCustomerV2", SaveCustomer).AddEndpointFilter<ValidationFilter<CustomerRequest>>()
            .WithName("CreateCutomerV2")
            .WithOpenApi();
    }

    public static async Task<IResult> SaveCustomer(ILogger<WeatherForecast> logger, CustomerRequest customer)
    {
        //var result = await validator.ValidateAsync(customer);

        //if (!result.IsValid)
        //{
        //    return Results.ValidationProblem(result.ToDictionary());//Errors
        //}

        await Task.CompletedTask;


        logger.LogInformation("Customer Saved called");

        return Results.Ok("Customer Created");
    }
}
