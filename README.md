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
- Projeto desenvolvido em C# .NET com ASP.NET Core 3.1;
- Entity Framework - com MySql;
- Autenticação e autorização OAuth2 - JWT
- Docker
- Messageria com RabbitMq
- Storage com Minio
- Controle de cache com Redis
  
#### Containers Docker:
- Api
- Banco de dados MySql
- RabbitMq
- Redis
- Minio

#### Estrutura projeto:
Projeto estruturado com a metodologia DDD e TDD distribuito por camadas:
- FavoDeMel.Api: `Camada de aplicação;`
- FavoDeMel.Domain: `Cadama de dominio;`
- FavoDeMel.EF.Repository: `Camada de repositório, responsavel por gerenciar todas as consultas e transações no banco de dados;`
- FavoDeMel.Framework: `Camada de framework, reponsavel por conter todos os helpers extensões e facilitadores do sistema;`
- FavoDeMel.IoC: `Camada Ioc, responsavel por gerenciaçar as injeções de dependencias;`
- FavoDeMel.Messaging: `Camada de mesageria, reponsavel por gerenciar todos os eventos e comandos de mesagens entre sistemas;`
- FavoDeMel.Redis.Repository: `Camada de repositório de cache, reponsavel por gerenciar os cache do sistema;`
- FavoDeMel.Service: `Camada de serviço, reponsavel por gerenciar todos os serviços do sistema;`
- FavoDeMel.Tests: `Camada de teste, reponsavel por gerenciar todos os testes unitários e de integração do sistema;`

## Frontend
- Projeto desenvolvido com Angular 9.1 (Typescript);
- Programação Reativa (RxJS);
- Bootstrap;
- Docker;

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
    
        `npm start`
      ou 
      `docker-compose up --build`

### Acesso aplicação após a build
- [Api](https://localhost:44300/swagger/index.html)
- [Web](http://localhost:4200)
- [Redis](http://localhost:8081)
- [RabbitMq](http://localhost:15672)
  ```
    Username: guest
    Password: guest
  ```
- [Minio](http://localhost:9000)
  ```
    Access Key: minio
    Secrt Key: minio123
  ```
- Mysql
    ```
    server=localhost;
    port=3306;
    userid=user;
    password=password;
    database=favodemel-db
    ```

### Anotações
Ao executar o sistema em modo debug, caso não contenha registro no banco de dados, o sistema irá gerar alguns dados para teste
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
