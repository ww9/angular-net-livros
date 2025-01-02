using Livros.API.Helpers;
using Livros.Application.Errors;

public class ValidationExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public ValidationExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (ValidationException ex)
		{
			context.Response.StatusCode = StatusCodes.Status400BadRequest;
			context.Response.ContentType = "application/json";
			var response = ValidationErrorResponse.GetValidationErrorResponse(ex);
			await context.Response.WriteAsJsonAsync(response.Value);
		}
	}
}

