# opuspac-test-microservices-API
# Microserviço em .NET 8

Este repositório contém um microserviço desenvolvido em .NET 8. O microserviço utiliza o Entity Framework Core para realizar o acesso ao banco de dados e o RabbitMQ para facilitar a comunicação assíncrona entre os serviços.

## Tecnologias Utilizadas

- **.NET 8**: A plataforma de desenvolvimento utilizada.
- **Entity Framework Core**: ORM (Object-Relational Mapper) que simplifica o acesso a dados.
- **PostgreSQL**: Sistema de gerenciamento de banco de dados relacional (ou outro banco de dados configurado).
- **RabbitMQ**: Solução de mensageria para comunicação entre serviços (caso aplicável).
- **Docker**: Ferramenta opcional para conteinerização dos serviços e bancos de dados.

## Configuração do Ambiente

Antes de iniciar a aplicação, é fundamental garantir que os seguintes requisitos estejam instalados em seu sistema:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0): Necessário para compilar e executar a aplicação.
- [Docker](https://www.docker.com/): Opcional, mas recomendado para executar o banco de dados e o RabbitMQ localmente.
- PostgreSQL: Ou outro banco de dados que você tenha configurado.
- RabbitMQ: Necessário se a aplicação utilizar mensageria.

### Configuração das Variáveis de Ambiente

Para configurar as variáveis de ambiente, crie um arquivo chamado `.env` (ou defina as variáveis diretamente no seu ambiente) com as seguintes configurações:
