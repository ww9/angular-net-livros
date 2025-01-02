using Livros.Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Helpers;

public class ValidationErrorResponse
{
	public static BadRequestObjectResult GetValidationErrorResponse(ValidationException ex)
	{
		return new BadRequestObjectResult(new
		{
			friendlyError = true,
			message = ex.Message
		});
	}
}
