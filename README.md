# Avaliação Arquitetura - Jonathan Dias Campos Santiago

## Favo de Mel
Sistema web desenvolvido para gerenciamento pedidos em comanda.
-   O sistema consiste de três perfis de acesso:
    - Administrador: `Tem acesso total ao sistema.`
    - Garçom: `Tem acesso a todas as rotinas do sistema, porem o acesso limitado. Não permitindo editar ou cadastrar um novo produto ou usuário. E contem a permissão de visualizar criar e editar comandas, não sendo possivel confirmar a mesma para a cozinha.`
    - Cozinheiro: `Tem acesso o mesmo acesso que o perfil de garçom, porem o mesmo contem a permissão de confirmar o pedido na cozinha.`
    
A rotina principal do sistema lista todas as comandas do dia, sendo dividido em três situações;
- Aberto, com as seguintes ações `Dependendo do perfil do usuário logado`;
    - Editar;
    - Vizualizar;
    - Confirmar;
- Confirmado;
    - Editar;
    - Vizualizar;
    - Fechar;
- Fechado;
    - Vizualizar;

O sistema contem a atualização em tempo real das comandas, caso usuário cadastre ou efetue uma ação em uma comanda já cadastrada, será atualizada todas as janelas com a rotina principal de comandas aberta.

## Definições de Arquitetura das Aplicações
O sistema foi arquitetado e distribuido com a estrutura dividida em Backend e Frontend.
- O Backend é a parte de gerenciamento de toda as regras de negócio e conexões com bancos de dados e serviços da aplicação.
- O Frontend é a parte visual da aplicação.

- Backend - Api foi desenvolvida usando as seguintes tecnologias:
    - C# .NET com ASP.NET Core 5.0;
    - Entity Framework;
    - PostgreSql;
    - JWT OAuth 2.0 para autenticação e autorização de acesso; 
    - Docker;
    - Docker Compose para orquestramento dos containers do Docker;
    - RabbitMq para serviço de mensageria, na aplicação esta sendo usado para atualizar as comandas em tempo real; 
    - Redis para controle de cache;
- Frontend - Foi desenvolvida usando as seguintes tecnologias:
    - Angular 11 com Typescript;
    - RxJS para programação reativa;
    - Bootstrap;
    - NgxBootstrap;
    - Scss;
    - Docker;
    - Docker Compose para orquestramento dos containers do Docker;
    - Nginx;

#### Serviços configurado em containers no Docker da aplicação:
- Api;
- Aplicação Web com Nginx;
- Banco de dados PostgreSql;
- RabbitMq;
- Flyway para executar os scripts de banco de dados default do sistema;
- Redis;

#### Definições e padrões:
Projeto estruturado com a metodologia DDD e TDD distribuito por camadas:
- Backend
    - FavoDeMel.Api: `Camada de aplicação`;
    - FavoDeMel.Domain: `Cadama de dominio`;
    - FavoDeMel.Repository: `Camada de repositório, responsavel por gerenciar todas as consultas e transações no banco de dados`;
    - FavoDeMel.Service: `Camada de serviço, reponsavel por gerenciar todos os serviços do sistema`;
    - FavoDeMel.Tests: `Camada de teste, reponsavel por gerenciar todos os testes unitários e de integração do sistema`;
- Frontend
    - App
        - componentes: `Camada de armazenamento dos componentes genericos e reutilizaveis, como cards, layout menu etc.`;
        - models: `Camada de armazenamento das entidades tipadas da aplicação`;
        - pages: `Camada de armazenamento dos componentes de paginas da aplicação`;
        - services: `Camada de serviços de consulta na api`;
        - shared: `Camada de armazenamento de funções e serviços compartilhado`;
    - assets
        - images
        - styles
    - enverinments
    
### Execução da aplicação:
Primeiramente deve-se clonar o projeto `git clone https://github.com/jonathandsantiago/processo-seletivo-arquitetura.git` e acessar `cd processo-seletivo-arquitetura`.

Para executar as aplicações é nescessario reservar as seguintes portas:
-  Para a Api `54300` para Https ou `8080` em Http;
- `15432` para o banco de dados postgres;
- `6379` para o redis e `8081` para o redis commander;
- `15672`, `5672` e `15674` para o RabbitMq;
- `54200` para aplicação web executada pelo `docker-compose` ou `4200` pelo `ng s` do angular;

- Para executar a Api:
    - Acesse a pasta `favodemel-api` e exucutar o seguinte comando:      
        `docker-compose up --build`
- Para executar os testes automatizado da Api:
    - Acessando a pasta `favodemel-api` e execute o seguinte comando:    
        `dotnet test ./test/FavoDeMel.Tests`
- Para executar a aplição Web:   
    - Acesse a pasta `favodemel-web` e exucutar o seguinte comando:      
    - `docker-compose up --build`
    Caso deseje executar a aplição fora do container execute os seguintes comandos: 
    - `npm install`
    - `ng s` ou `ng build --prod` para executar em mode de produção;

### Acesso aplicação após a build
- Para visualizar ou executar uma ação direta na Api pelo Swagger acesse: [Api:54300](https://localhost:54300/swagger/index.html)
- Para acessar a aplição web acesse: 
    - [Web:54200](http://localhost:54200) Caso executado a aplição pelo `docker-compose`
    - [Web:4200](http://localhost:4200) Caso executado a aplição pelo `ng s`
        - Usuários:
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
- Para visualizar ou gerencias os cache da aplicação [Redis:8081](http://localhost:8081)
- Para acessar o RabbitMq, serviço de mensageria [RabbitMq:15672](http://localhost:15672/#/queues)
    - Usuário: `guest`
    - Senha: `guest`
- Para acessar o banco de dados PostgreSql
    ```
    server=localhost;
    port=15432;
    userid=admin;
    password=Password_1;
    database=favodemel
    ```

### Definições técnica do sistema
```
```

### Definições do sistema
```
```