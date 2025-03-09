# opuspac-test-microservices-API
# Microserviço em .NET 8

Este repositório contém um microserviço desenvolvido em .NET 8. O microserviço utiliza Entity Framework Core para acesso ao banco de dados e RabbitMQ para comunicação assíncrona.

## Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- PostgreSQL (ou outro banco de dados configurado)
- RabbitMQ (caso haja mensageria)
- Docker (opcional para conteinerização)

## Configuração do Ambiente

Antes de rodar a aplicação, certifique-se de que tem os seguintes requisitos instalados:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (opcional para executar banco de dados e RabbitMQ localmente)
- PostgreSQL ou outro banco de dados configurado
- RabbitMQ (se aplicável)

### Configuração das Variáveis de Ambiente

Crie um arquivo `.env` (ou defina diretamente no ambiente) com as seguintes configurações:

```env
DEFAULT_CONNECTION=Host=localhost;Port=5432;Database=MeuBanco;Username=MeuUsuario;Password=MinhaSenha;
RABBIT_CONNECTION=amqp://guest:guest@localhost:5672/
ENCRYPTION_CLAIMS_KEY=INSIRA_SUA_CHAVE_AQUI
```

Caso precise criar uma nova migração:

```sh
dotnet ef migrations add NomeDaMigracao --project ./Caminho/Para/SeuProjeto.Repository
dotnet ef migrations add  ProductService
dotnet ef migrations add  OrderService
dotnet ef migrations add  AuthenticationService
```

### Configurar o banco de dados

Se o projeto utilizar Entity Framework Core, aplique as migrações:

```sh
dotnet ef database update  ProductService
dotnet ef database update  OrderService
dotnet ef database update  AuthenticationService
```

### Executar a aplicação

```sh
dotnet run ProductService
dotnet run OrderService
dotnet run AuthenticationService
```





