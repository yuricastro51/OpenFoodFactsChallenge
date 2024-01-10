# Projeto OpenFoodFactsChallenge

## Descrição do Projeto
Este projeto consiste na implementação de uma REST API em ASP.NET que utiliza os dados do projeto Open Food Facts. O objetivo é fornecer suporte à equipe de nutricionistas da empresa Fitness Foods LC, permitindo a comparação rápida de informações nutricionais de alimentos da base do Open Food Facts.

## Tecnologias Utilizadas
- Linguagem: C# com ASP.NET
- HtmlAgilityPack
- MediatR
- MongoDB
- Quartz para sistema de CRON
- Entity Framework
- Docker
- Swagger
- Testes Unitários

## Instalação e Uso
1. Clone o repositório.
2. Abra o projeto em sua IDE preferida.
3. Certifique-se de ter o .NET SDK instalado.
4. Restaure as dependências: `dotnet restore`
5. Execute o aplicativo: `dotnet run`
6. Acesse a API em `http://localhost:5025/swagger`

## Configuração do Docker
Para construir e executar o contêiner Docker, siga as instruções abaixo:

```bash
docker build -t openfoodfactschallenge .
docker run -p 8080:8080 openfoodfactschallenge
```
Acessar a API em `localhost:8080/swagger`

## Testes Unitários
Para executar os testes unitários, siga as instruções abaixo:

1. Entre na pasta do projeto de testes: `cd tests/OpenFoodFactsChallenge.Tests`
```bash
dotnet test
```

This is a challenge by Coodesh