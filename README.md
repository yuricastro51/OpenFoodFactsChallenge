# Projeto OpenFoodFactsChallenge

## Descri��o do Projeto
Este projeto consiste na implementa��o de uma REST API em ASP.NET que utiliza os dados do projeto Open Food Facts. O objetivo � fornecer suporte � equipe de nutricionistas da empresa Fitness Foods LC, permitindo a compara��o r�pida de informa��es nutricionais de alimentos da base do Open Food Facts.

## Tecnologias Utilizadas
- Linguagem: C# com ASP.NET
- HtmlAgilityPack
- MediatR
- MongoDB
- Quartz para sistema de CRON
- Entity Framework
- Docker
- Swagger
- Testes Unit�rios

## Instala��o e Uso
1. Clone o reposit�rio.
2. Abra o projeto em sua IDE preferida.
3. Certifique-se de ter o .NET SDK instalado.
4. Restaure as depend�ncias: `dotnet restore`
5. Execute o aplicativo: `dotnet run`
6. Acesse a API em `http://localhost:5025/swagger`

## Configura��o do Docker
Para construir e executar o cont�iner Docker, siga as instru��es abaixo:

```bash
docker build -t openfoodfactschallenge .
docker run -p 8080:8080 openfoodfactschallenge
```
Acessar a API em `localhost:8080/swagger`

## Testes Unit�rios
Para executar os testes unit�rios, siga as instru��es abaixo:

1. Entre na pasta do projeto de testes: `cd tests/OpenFoodFactsChallenge.Tests`
```bash
dotnet test
```

This is a challenge by Coodesh