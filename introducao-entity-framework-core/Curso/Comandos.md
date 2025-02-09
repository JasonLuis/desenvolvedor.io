**Site para ver os pacotes e comandos para instalação com o Nuget**
`https://www.nuget.org/`

**Criar uma Solução no terminal**
`dotnet new sln -n nome_da_solucao`

**Criar um projeto console dotnet no terminal**
`dotnet new console -n nome_do_projeto -n nome_do_projeto -o local_do_projeto -f versao_do_framework`

**Adicionar projeto a Solução**
`dotnet sln nome_da_solucao.snl add caminho_do_projeto\projeto.csproj`

**Instalar o pacote SqlServer no projeto via terminal**
`dotnet add caminho_do_projeto\projeto.csproj package Microsoft.EntityFrameworkCore.SqlServer --version versao_do_pacote`
A versão tem que ser compatível com o framework usado no projeto 

*Para Realizar as Migrações*

**Instalar o pacote Entity Framework Design**
`dotnet  add caminho_do_projeto\projeto.csproj package Microsoft.EntityFrameworkCore.Design --version versao_do_pacote`

**Intalar o pacote Entity Framework Tool no projeto ou global**
`No projeto -> dotnet add caminho_do_projeto\projeto.csproj package Microsoft.EntityFrameworkCore.Tools --version versao_do_pacote`
`Globalmente -> dotnet Tool install --global dotnet-ef --version versao_do_dotnet`

*Gerando Migraçãoes*

`Apos Configurar meu projeto, modelar as classes e a Classe context, podemos criar e executar as migrações. Nesse exemplo estaremos usando o pacote Tool global`

**Criar uma Migration**
`dotnet ef migrations add nome_da_migration -p caminho_do_projeto/projeto.csproj`
`O '-p caminho_do_projeto/projeto.csproj' é para caso, no terminal ao executar o camando, não esta na raiz do projeto`

**Configurar o local onde vai ser criado os arquivos de migrações**
`Por padrão o efcore, ao gerar as migrações, ele cria uma pasta chamada Migrations, mas caso deseje colocar em outra pasta basta digitar o seguinte comando ao gerar uma migration:`

`dotnet ef migrations add nome_da_migration -p caminho_do_projeto/projeto.csproj -o destion_das_migrations`

**Gerar um script de Migração**
`dotnet ef migrations script -p caminho_do_projeto/projeto.csproj -o destiono/nome_do_arquivo.SQL`

**Aplicando as Migrações**
`dotnet ef update -p caminho_do_projeto/projeto.csproj -v`
`-v significa verbose, ira escrever os comandos que o ef esta realizando no Banco de Dados`

**Gerar um script SQL Idempotentes**
`dotnet ef migrations script -p caminho_do_projeto/projeto.csproj -o destiono/nome_do_arquivo.SQL -i`
`pode-se usar -i ou --idempotent`
`Esse script pode ser executado varias vezes, pois ele vai verificar se a ação descrita no SQL ja foi realizada no Banco de dados`

**Rollback de migrações**
`Para dar um roolback, basta dar update no banco com a migration anterior`
`dotnet ef update nome_da_migration_anterior -p caminho_do_projeto/projeto.csproj `
`Ao executar o comando, e analizarmos a tabela migration no Banco de Dados, iremos analizar que a migration posterior foi removida realizando o rollback.`

**Remover um arquivo de Migração**
`dotnet ef migrations remove -p caminho_do_projeto/projeto.csproj`



