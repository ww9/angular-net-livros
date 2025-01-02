using Microsoft.AspNetCore.Builder;

public static class MiddlewareExtensions
{
	public static IApplicationBuilder UseValidationExceptionMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ValidationExceptionMiddleware>();
	}
}