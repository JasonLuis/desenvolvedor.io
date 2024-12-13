# Criar uma solução

`dotnet new sln -n nome_da_solução`

# Criar um projeto

`dotnet new console -n nome_projeto -f net8.0` 
O `-f` refere-se a qual sera a versão do net sera utlizada, no exemplo é dotnet 8.0


`dotnet new console --use-program-main -n AppClientes`

Nesse comando, diferente do comando anterior, temos o '--use-program-main' o que vai criar um projeto a classe Program com o método Main, usando a maneira tradicional, no qual não vamos utilizar o Top Level (conceito mais simplificado);