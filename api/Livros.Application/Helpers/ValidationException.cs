using System;
using System.Collections.Generic;

namespace Livros.Application.Errors
{
	public class ValidationException : Exception
	{
		public ValidationException(string message) : base(message)
		{
		}

		public ValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}