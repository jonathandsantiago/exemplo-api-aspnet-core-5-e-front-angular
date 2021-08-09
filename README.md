# Avaliação Arquitetura - Jonathan Dias Campos Santiago

## Favo de Mel

Sistema web de controle de comandas:
O Sistema inicia como rotina principal a rotina de listagem de comandas, sendo dividido em três situações;
- Aberto;
    - Editar `Contem validação por perfil`;
    - Vizualizar;
    - Confirmar `Contem validação por perfil`;
- Confirmado;
    - Editar `Contem validação por perfil`;
    - Vizualizar;
    - Fechar `Contem validação por perfil`;
- Fechado;
    - Vizualizar;

O Controle de acesso do sistema é gerenciado por perfil.
- Administrado;
    - Tem acesso a todas as ações e rotina no sistema;
- Garçom;
    - Tem acesso a cadastro de comandas e ação de cadastro/edição/visualização e fechamento de conta;
- Cozinheiro;
    - Tem acesso a cadastro de comandas e ação de confirmar pedidos;

## Backend
- Projeto desenvolvido em C# .NET com ASP.NET Core 5.0;
- Entity Framework - com PostgreSql;
- Autenticação e autorização OAuth2 - JWT
- Docker
- Messageria com Kafka
- Controle de cache com Redis
  
#### Containers Docker:
- Api
- Banco de dados PostgreSql
- Kafka
- Redis

#### Estrutura projeto:
Projeto estruturado com a metodologia DDD e TDD distribuito por camadas:
- FavoDeMel.Api: `Camada de aplicação;`
- FavoDeMel.Domain: `Cadama de dominio;`
- FavoDeMel.Repository: `Camada de repositório, responsavel por gerenciar todas as consultas e transações no banco de dados;`
- FavoDeMel.Service: `Camada de serviço, reponsavel por gerenciar todos os serviços do sistema;`
- FavoDeMel.Tests: `Camada de teste, reponsavel por gerenciar todos os testes unitários e de integração do sistema;`

## Frontend
- Projeto desenvolvido com Angular 11 (Typescript);
- Programação Reativa (RxJS);
- Bootstrap;
- NgxBootstrap;
- Scss;
- Docker;
- Nginx

#### Containers Docker:
- Web

#### Estrutura projeto:
Projeto estruturado distribuito por camadas:
- App
    - componentes;
    - models;
    - pages;
    - services;
    - shared;
- assets
    - images
    - styles
- enverinments

### BUILD
- Api:
    - Acessar a pasta da solução do projeto api e exucutar o seguinte comando:      
      `docker-compose up --build`
- Api - Teste:
    - Acessar a pasta da solução do projeto api e exucutar o seguinte comando:    
        `dotnet test	test/FavoDeMel.Tests`
- Web:      
    - Acessar a pasta da solução do projeto e exucutar o seguinte comando:
    - `docker-compose up --build`
    ou
    - `ng install`
    - `npm start`
    - `ng s`
    - `ng build --prod`

### Acesso aplicação após a build
- [Api](https://localhost:44300/swagger/index.html)
- [Web](http://localhost:4200)
- [Redis](http://localhost:8081)
- [kafdrop](http://localhost:19000)
- Kafka(kafka:29092)
    ```
      port=29092;
      BrokerConnect=kafka:29092;
    ```
- PostgreSql
    ```
    server=localhost;
    port=15432;
    userid=admin;
    password=Password_1;
    database=favodemel
    ```

### Kafka
    A Mensageria foi montada com kafka e para que o frontend comunique em tempo real com kafka foi projetado um WebSocket.
    Foi configurado no docker compose para levantar o kafdrop: Kafdrop é uma IU da web para visualizar os tópicos do Kafka

### Anotações
Ao executar a Api em modo debug pelo Visual Studio, caso não contenha registro no banco de dados, o sistema irá gerar alguns dados para teste
cadastrando três usuários com os seguintes perfís:
- Administrador:
    ```
        Login: Admin
        Senha: Admin
    ```
- Garçom:
    ```
        Login: Garçom
        Senha: Garçom
    ```
- Cozinheiro:
    ```
        Login: Cozinheiro
        Senha: Cozinheiro
    ```
