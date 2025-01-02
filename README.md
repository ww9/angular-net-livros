# Livros CRUD em .NET and Angular

# Comandos úteis para API

Executar testes: `cd api/Livros.Tests && ASPNETCORE_ENVIRONMENT=Development dotnet watch test`

Rodar backend: `cd api && ASPNETCORE_ENVIRONMENT=Development dotnet watch --project Livros.API`

Rodar frontend: `cd app && ng serve`

Executa backend e recarrega automaticamente quando um arquivo é criado ou alterado.

# TODO

- TODO: próximo passo é implementar validações no CRUD de assuntos.

  - Descrição deve ser obrigatória, única e respeitar limite de caracteres.
  - Ao editar não deve permitir que a descrição seja alterada para uma descrição já existente.
  - Ao tentar excluir um assunto que está associado a um livro, deve retornar erro.

- Centralizar gerenciamento de erros no Angular.
- Comentar métodos das Controllers da API para incrementar documentação do Swagger.
- Cobertura de código C# com CLI (já funciona no VSCode).
- Testes end to end no Angular.
- Implementar preço do livro dependend oda forma de compra.
- Implementar relatório com View do banco. Isso requer um banco de dados relacional. Pode ser o SQLite.
