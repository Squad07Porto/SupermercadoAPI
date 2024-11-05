# Como rodar

## Guia válido de como rodar os comandos da aplicação.
Os comandos são todos para Linux e sistemas UNIX-like, porém caso você esteja no Windows [use o Windows Terminal](https://learn.microsoft.com/pt-br/windows/terminal/install), baixe o [WSL2 no Powershell](https://learn.microsoft.com/pt-br/windows/wsl/install) usando o comando `wsl --install` dentro do Powershell (preferencialmente como administrador).

## Docker

### Inicializar e construir os containers sem ocupar o terminal:
`docker compose up -d`

### Iniciar os containers já construído:
`docker compose start`

### Parar os containers
`docker compose stop`

### Remover os volumes e os containers:
`docker compose down`

### Acessar o shell (terminal) de algum container
`docker exec -it nome_do_container "bash"`

## Banco de dados

### Backup do banco
`docker exec CONTAINER /usr/bin/mysqldump -u root --password=senhatal DATABASE > backup.sql`

### Restaurar o banco a partir de um backup
`cat backup.sql | docker exec -i CONTAINER /usr/bin/mysql -u root --password=root DATABASE`

## .NET

É essencial ter o .NET instalado na máquina. [Siga este tutorial](https://learn.microsoft.com/pt-br/dotnet/core/install/linux-ubuntu-install?pivots=os-linux-ubuntu-2404&tabs=dotnet8) para saber como instalar o SDK e os runtimes necessários para rodar a aplicação (que se encontra na versão 8.0 do .NET). O tutorial em questão é para o Ubuntu, mas a Microsoft disponibiliza para outras distribuições também.

Tendo instalado corretamente, basta estar na raiz do projeto, referenciando o diretório/pasta que contém o arquivo `.csproj` da aplicação e rodar os comandos:

```bash
dotnet restore
dotnet run --project Supermercado.API # ou somente `dotnet run` caso você já esteja na pasta Supermercado.API
```

## Message broker (RabbitMQ)

> Porta do próprio Rabbit: `5672`

> Porta do Management: `15672`

O RabbitMQ é responsável no contexto desta aplicação por fazer o envio das mensagens recebidas pelo sensor (simulado) para a aplicação, fazendo com que a aplicação receba os dados do sensor e faça o envio para ativar alguma notificação no aplicativo mobile já existente.