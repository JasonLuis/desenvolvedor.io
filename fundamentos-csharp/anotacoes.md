## Como criar uma solução via CLI

`dotnet new sln -n nome_solução`


## Como criar um projeto 

`dotnet new console -n nome_projeto -f net8.0`


"Obs: -f é para qual a framework sera utilizada no projeto, no exemplo acima é o .net 8.0"

## Como adicionar um projeto a uma solução

`dotnet sln nome_solucao add nome_projeto`

## Como compilar um projeto

Existem duas formas de fazer isso: posso digitar um comando que irá compilar toda a minha solution ou um comando que compilará um projeto específico.
Para compilar:
`dotnet build`

Esse comando compilará gerando os binarios contendo IL para o run time execute a aplicação.

Os arquivos da compilação do projeto ficam em `nome_projeto/bin/Debug/net8.0`

O comando `dotnet clean` serve para limpar as pastas/arquivos gerados da compilação.