using FluentValidation;

namespace WebApplication1.Filter;



public class ValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validatable = context.Arguments.SingleOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (validatable is null)
        {
            return Results.BadRequest();//Errors
        }


        var validationResult = await _validator.ValidateAsync(validatable);

        if (!validationResult.IsValid)
        {
            // return Results.ValidationProblem(result.ToDictionary());//Errors
            return Results.BadRequest(validationResult.Errors.ToResponse());
        }


        //Before the Endpoint Called
        var result = await next(context);

        //After the endpoint called

        return result;
    }
}
