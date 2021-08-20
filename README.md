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
O sistema foi arquitetado e distribuido com a estrutura distribuida no Backend e Frontend.
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

#### Serviços configurado em containers no Docker das aplicações:
- Api;
- Aplicação Web com Nginx;
- Banco de dados PostgreSql;
- RabbitMq;
- Flyway para executar os scripts de banco de dados default do sistema;
- Redis;

#### Definições de estrutura das aplicações:
Projeto foi desenvolvido utilizando a metodologia DDD e TDD distribuito em camadas:
- Backend
    - FavoDeMel.Api: `Camada de aplicação`;
    - FavoDeMel.Domain: `Cadama de dominio`;
    - FavoDeMel.Repository: `Camada de repositório, responsavel por gerenciar todas as consultas e transações no banco de dados`;
    - FavoDeMel.Service: `Camada de serviço, reponsavel por gerenciar todos os serviços do sistema`;
    - FavoDeMel.Tests: `Camada de teste, reponsavel por gerenciar todos os testes unitários e de integração do sistema`;
- Frontend
    - App
        - componentes: `Camada dos componentes genéricos e reutilizáveis, como cards, layout menu etc.`;
        - models: `Camada das entidades tipadas da aplicação`;
        - pages: `Camada dos componentes das paginas da aplicação`;
        - services: `Camada de serviços e consulta na api`;
        - shared: `Camada de funções e serviços compartilhado`;
    - assets
        - images
        - styles
    - enverinments
    
### Execução da aplicação:
Primeiramente deve-se clonar o projeto `git clone https://github.com/jonathandsantiago/processo-seletivo-arquitetura.git` e acessar `cd processo-seletivo-arquitetura`.

Para executar as aplicações é nescessario reservar as seguintes portas:
-  Para a Api `54300` em Https e `58080` em Http;
- `15432` para o banco de dados postgres;
- `6379` para o redis e `8081` para o redis commander;
- `15672`, `5672` e `15674` para o RabbitMq;
- `54200` para aplicação web executada pelo `docker-compose` ou `4200` pelo `ng s` do angular;

- Para executar ambas as aplicações simultaneamente acessando a pasta raiz `processo-seletivo-arquitetura` execute o seguinte comando:

    - `docker-compose -f favodemel-api/docker-compose.prod.yml up -d --build && docker-compose -f favodemel-web/docker-compose.yml up -d --build`

- Para executar individualmente a Api:
    - Acesse a pasta `favodemel-api` e exucutar o seguinte comando:      
        - `docker-compose -f docker-compose.prod.yml up -d --build`
- Para executar os testes automatizado da Api:
    - Acessando a pasta `favodemel-api` e execute o seguinte comando:    
        - `dotnet test ./test/FavoDeMel.Tests`
- Para executar individualmente a aplição Web:   
    - Acesse a pasta `favodemel-web` e exucutar o seguinte comando:      
       - `docker-compose up -d --build`
        - Caso deseje executar a aplição fora do container execute os seguintes comandos: 
            - Instale os pacotes `npm install`
            - Execute a aplicação `ng s`;

### Descrição das flags utilizada no compose
 - `up` cria e inicia os contêineres;
 - `-d` ou `--detach` irá executar os contêineres em segundo plano;
 - `--build` irá criar as imagens antes de iniciar os contêineres;
 - `-f` é opcional. Se não utilizar esta flag na linha de comando, o Compose irá percorrer no diretório um arquivo `compose.yaml` ou `docker-compose.yaml`;

### Acesso aplicação após a build
- Para visualizar ou executar uma ação direta na Api pelo Swagger acesse: [Api:54300](https://localhost:54300/swagger/index.html) em Https ou [Api:58080](http://localhost:58080/swagger/index.html)
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

## Definições técnica e padrões dos sistemas
### Definições técnica
- O `Program.cs` utiliza a extensão `MigrateDbContext<FavoDeMelDbContext>()` localizada no projeto `FavoDeMel.Repository.Extensions` recebendo o tipo do DbContext.
    Esta ação irá execultar os `Migrations` ao rodar a build.
    Caso contenha mais de um DbContext basta reclipar a extensão passando o tipo desejado.

    <img src='imagens/ProgramApi.png'>

- Para configurar as injeções de dependências no `Startup` da aplicação. Foi desenvolvido a extensão `.AddApiProvidersAssembly` recebendo o `Microsoft.Extensions.Configuration.IConfiguration` como paramêtro.
Esta extensão irá injetar por convenção todos os providers criado na pasta `FavoDeMel.Api.Providers` assinado com a interface `IApiProvider`.
    <img src='imagens/ConfigureServices.png'>

    <img src='imagens/Providers.png'>

    A interface `IApiProvider` contem o método void `void AddProvider(IServiceCollection services, ISettings<string, object> settings)`:

    <img src='imagens/Provider.png'>

    Este método adiciona todas as injeções de dependência da aplicação. O método espera em parâmetro o `Microsoft.Extensions.DependencyInjection.IServiceCollection` e o `FavoDeMel.Domain.Interfaces.ISettings<TKey, TValue>`.
    
    O `settings` contem todas as chaves e valores dos objetos assinado com a interface `ISettings` esses objetos estão localizados em `FavoDeMel.Domain.Models.Settings`

    <img src='imagens/Settings.png'>

    O valores serão atribuidos ao objeto conforme as configurações no `appsettings`:

    <img src='imagens/Appsettings.png'>

    Construindo o objeto com `Microsoft.Extensions.Options.ConfigureFromConfigurationOptions<TOptions>` passando o nome do objeto no `configuration.GetSection()` no exemplo abaixo estou atribuindo os valores do `RabbitMq` do `appsettings` para o objeto `RabbitMqSettings`:

    <img src='imagens/RabbitMqSettings.png'>
    
    Todos os demais `Settings` deverá seguir o mesmo padrão.

    Os `Settings` faz override em `ToString()` retornando `StringHelper.ConcatLogConfig(this)`, está ação irá printar no log ao levantar a aplicação os valores atribuidos em cada `Settings`:
    
    <img src='imagens/LogSettings.png'>

- O mesmo procedimento é aplicado no `Configure` do `Startup`, utilizando a extensão `.AddApiConfigsAssembly(env, Configuration)`. A extensão espera como parâmetro o `Microsoft.AspNetCore.Builder.IApplicationBuilder` e `Microsoft.AspNetCore.Hosting.IWebHostEnvironment` e irá adicionar todos as configurações localizadas na pasta `FavoDeMel.Api.Configs` assinado com a interface `IApiConfigure`.

    <img src='imagens/Configure.png'>

    <img src='imagens/Configs.png'>
 
    A interface `IApiConfigure` recebe como parâmetro o `IApplicationBuilder`, `IWebHostEnvironment` e `ISettings<string, object>` o settings é o mesmo informado a cima na interface `IApiProvider`.

    <img src='imagens/SwaggerConfigure.png'>

- O provider de mensageria utilizando o `RabbitMq` cria na inicializando os `Endpoint` de recebimento de todos os commands assinado `FavoDeMel.Domain.Interfaces.IMensageriaCommand`. Esta ação irá para cada command um `Exchanges` do tipo `fanout` no `RabbitMq`:

    <img src='imagens/ProviderRabbitMq.png'>

    <img src='imagens/ExchangeRabbitMq.png'>

    Esta ação é necessária para configurar no frontend os serviços de `consumer` webstomp, a atualização em tempo real de cada ação. Caso não aplicado esta configuração, o frontend só vai conseguir se comunicar de forma correta com o rabbitmq após o envio de uma mensage na fila de cada `Command`, que por sua vês o evio na fila cria o `Exchanges` do tipo `fanout`.

    Outra configuração importante para funcionamento da comunicação do fronted com o rabbitmq é no compose. No compose o serviço `favodemel.api.rabbitmq` copia para o volume `/etc/rabbitmq/enabled_plugins` o enabled_plugins localizada na pasta `config` da raiz `favodemel-api`. O arquivo enabled_plugins contem a seguinte configuração `[rabbitmq_management, rabbitmq_web_stomp].` esta ação irá habilitar o `rabbitmq_web_stomp` que por sua vês habilita o uso do STOMP em um contexto de navegador web.
### Padrões
 - No projeto `FavoDeMel.Api` na pasta `Controllers` contem a pasta `Common` onde se localiza todos as controller base e na raiz contem apenas as `Controller` da aplicação.
    As controllers com nescessidade de acesso registro utiliza `[Authorize(JwtBearerDefaults.AuthenticationScheme)]` e as rotas deverá ser criada na classe `FavoDeMel.Domain.Models.Routes.Endpoints`.

    A classe estatica `Recursos` é para definição de nome de cada controller e a classe estatica `Rotas` são as nomenclatura das rotas genérica "Que vai ser a nomenclatura padrão", caso deseje uma nomenclatura especifica de um endpoint de uma controller. 
    Deve-se criar a classe estatica com o nome da controller api ex: `ComandaApi` e dentro dela a nomenclatura de cada endpoint especifico.
    Este modelo é utilizado para centralizar as rotas da api e mantendo um padrão de nomenclatura seguindo em base das já existentes.

    As controllers não deverão ter regras de negócio e nem consulta direta em banco de dados.
    Esta ações deverá ser distruida em serviços, validadores e repositórios.

- No projeto `FavoDeMel.Domain` contem as pastas:
    - `Common` onde contem todas as classes base ou comum
    - `Dtos` onde contem todos os Dtos que serão exposto na api, os `Filtros` e `Mappers`.
        - `Mappers` centraliza todas configurações de mapeamento da entidade com o dto e vice-versa.
    - `Entities` onde contem todos as entidades mapeada no banco de dados. Cada entidade contem sua pasta com a nomenclatura no plural. Ex: `Comandas`:

        <img src='imagens/Entities.png'>

        Nela encontra-se os `Commands` do serviço de mensageria, o validator e a interface de repositório da entidade.

- `Validator`

### Definições do sistema
```
```