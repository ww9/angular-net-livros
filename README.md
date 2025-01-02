# Livros CRUD em .NET and Angular

# Comandos úteis para API

`cd api/Livros.Tests && dotnet watch test`

Executa testes de backend e recarrega automaticamente quando um arquivo é criado ou alterado. Bom para TDD.

`cd api && dotnet watch --project Livros.API run`

Executa backend e recarrega automaticamente quando um arquivo é criado ou alterado.

# TODO

- Comentar métodos das Controllers da API para incrementar documentação do Swagger.
- Cobertura de código C# com CLI (já funciona no VSCode).
- Testes end to end no Angular.
- Implementar preço do livro dependend oda forma de compra.
- Implementar relatório com View do banco. Isso requer um banco de dados relacional. Pode ser o SQLite.
