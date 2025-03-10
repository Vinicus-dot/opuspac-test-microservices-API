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

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0): O SDK é necessário para compilar e executar aplicações .NET. Você pode baixá-lo do site oficial e seguir as instruções de instalação para o seu sistema operacional.
- [Docker](https://www.docker.com/): Embora seja opcional, o Docker facilita a execução do banco de dados e do RabbitMQ em contêineres. Após instalar o Docker, você poderá gerenciar facilmente os serviços necessários para o seu microserviço.
- PostgreSQL ou outro banco de dados configurado: O microserviço precisa de um banco de dados para armazenar dados. Certifique-se de que o banco de dados está acessível e configurado corretamente.
- RabbitMQ (se aplicável): Necessário para a comunicação assíncrona entre microserviços. Você deve garantir que o RabbitMQ esteja em execução e acessível.

### Configuração das Variáveis de Ambiente

Para configurar as variáveis de ambiente, você pode criar um arquivo chamado `.env` ou definir as variáveis diretamente no seu ambiente. As variáveis que você precisa configurar incluem:

- **DEFAULT_CONNECTION**: Esta variável deve conter a string de conexão para o seu banco de dados, incluindo o host, a porta, o nome do banco de dados, o nome de usuário e a senha.
- **RABBIT_CONNECTION**: Esta variável deve conter a URL de conexão para o RabbitMQ, que geralmente inclui o protocolo, o usuário e a senha.
- **ENCRYPTION_CLAIMS_KEY**: Esta variável deve conter uma chave de criptografia que você usará para proteger informações sensíveis.

### Executando o Banco de Dados e RabbitMQ com Docker

Se você optar por executar o banco de dados e o RabbitMQ usando Docker, siga os passos abaixo:

1. **Baixar e instalar o Docker**: Acesse o site do [Docker](https://www.docker.com) e siga as instruções para instalar o Docker no seu sistema.
2. **Iniciar os serviços**: Após instalar o Docker, você pode iniciar os serviços necessários usando o comando `docker-compose up`. Este comando irá criar e iniciar os contêineres definidos no seu arquivo `docker-compose.yml`. Você também pode iniciar os serviços manualmente com os seguintes comandos:

   Para iniciar o PostgreSQL:
   ```
   docker run --name my-postgres -e POSTGRES_PASSWORD=mysecretpassword -p 5433:5432 -d --rm postgres:latest
   ```

   Para iniciar o RabbitMQ:
   ```
   docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
   ```
3. **Parar os serviços**: Para parar os serviços em execução, você pode usar o comando `docker-compose down`, que irá parar e remover os contêineres.

### Criando e Aplicando Migrações

Caso precise criar uma nova migração para o Entity Framework Core, você pode usar os seguintes comandos:

- Para adicionar uma migração para o `ProductService`, execute `dotnet ef migrations add ProductService --project ProductService`.
- Para adicionar uma migração para o `OrderService`, execute `dotnet ef migrations add OrderService --project OrderService`.
- Para adicionar uma migração para o `AuthenticationService`, execute `dotnet ef migrations add AuthenticationService --project AuthenticationService`.

Esses comandos irão gerar as migrações necessárias para atualizar o esquema do banco de dados de acordo com as alterações feitas nos modelos.

### Configurando o Banco de Dados

Se o projeto utilizar Entity Framework Core, você deve aplicar as migrações para atualizar o banco de dados. Use os seguintes comandos:

- Para atualizar o banco de dados do `ProductService`, execute `dotnet ef database update --project ProductService`.
- Para atualizar o banco de dados do `OrderService`, execute `dotnet ef database update --project OrderService`.
- Para atualizar o banco de dados do `AuthenticationService`, execute `dotnet ef database update --project AuthenticationService`.

Esses comandos irão aplicar as migrações pendentes e garantir que o banco de dados esteja em sincronia com os modelos do seu projeto.

### Executando a Aplicação

Para executar cada um dos microserviços, você pode usar os seguintes comandos:

- Para iniciar o `ProductService`, execute `dotnet run ProductService`.
- Para iniciar o `OrderService`, execute `dotnet run OrderService`.
- Para iniciar o `AuthenticationService`, execute `dotnet run AuthenticationService`.

Esses comandos irão compilar e iniciar os serviços, permitindo que você interaja com a aplicação e teste suas funcionalidades.
