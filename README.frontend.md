# Passos que realizei para criar o frontend

Git já deve estar incializado conforme instruções do README.backend.md.

## Extensões recomendadas do VS Code

- C# Dev kit: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit
- IntelliCode for C# Dev Kit: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscodeintellicode-csharp

## Configuração de ambiente Ubuntu 20.04 em WSL2

Verificar nvm e node instalados:

`nvm --version`

`node --version`

Se seu node for menor que 18, atualize segundo a documentação em https://angular.dev/reference/versions:

`nvm install 18`

`nvm use 18`

Instalar CLI do Angular:

`npm install -g @angular/cli`

# Criar projeto Angular e configurações iniciais

`ng new web`

Opções utilizadas:

- CSS: Yes
- Telemetry: No
- SSR: No

Execute isso para ter auto-completions no terminal sem precisar reiniciar:

`source <(ng completion script)`

Teste a aplicação:

`cd web && ng serve`

Abra para testar: http://127.0.0.1:4200

Instale bootstrap:

`npm install bootstrap`

Adicione no arquivo `angular.json`:

```json
"styles": [
    "src/styles.css",
],
"scripts": [
    "./node_modules/bootstrap/dist/js/bootstrap.bundle.min.js"
]
```

Execute o servidor novamente com `ng serve` e se tudo estiver certo, o bootstrap deve estar funcionando.

Se tudo estiver certo, comite as alterações.

# Criar componentes, serviços e interfaces iniciais para livro, autor e assunto

```bash
ng generate component components/livro
ng generate component components/autor
ng generate component components/assunto
ng generate component components/forma
ng generate component components/relatorio_por_autor

ng generate service services/livro
ng generate service services/autor
ng generate service services/assunto
ng generate service services/forma
ng generate service services/relatorio_por_autor

ng generate interface interfaces/livro
ng generate interface interfaces/autor
ng generate interface interfaces/assunto
ng generate interface interfaces/forma
ng generate interface interfaces/relatorio_por_autor
```
