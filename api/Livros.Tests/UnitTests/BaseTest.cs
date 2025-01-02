using Livros.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Livros.Tests.UnitTests;

public abstract class BaseTest
{
	protected DbContextOptions<LivrosContext> GetNewContextOptionsWithRandomInmemoryDatabase()
	{
		// Gera nome de banco aleatório para testes independentes
		return new DbContextOptionsBuilder<LivrosContext>()
			 .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}").Options;
	}
}
