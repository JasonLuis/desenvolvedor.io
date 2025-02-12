// See https://aka.ms/new-console-template for more information
using CursoEFCore.Data;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;


// Dessa maneira, ao executar o meu projeto estarei executando tambem minhas migrações.
using var db = new CursoEFCore.Data.ApplicationContext();
//db.Database.Migrate();



var existe = db.Database.GetPendingMigrations().Any();
if(existe) {
    Console.WriteLine("Existem migrações pendentes");
}
//InserirDados();
//InserirDadosEmMassa();
//ConsultarDados();
//CadastrarPedido();
//ConsultarPedidoCarregamentoAdiantado();
//AtualizarDados();
RemoverRegistro();
Console.WriteLine("Hello, World!");

static void InserirDados() {
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891234",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    using var db = new ApplicationContext();
    // Formas de adicionar registros na minha base de dados
    db.Add(produto); // -> recomendado usar essa 
    db.Set<Produto>().Add(produto); // -> recomendado usar essa
    db.Entry(produto).State = EntityState.Added; // -> // analisa o estado do objeto e adiciona na base de dados
    db.Add(produto);

    // todas as formas acima fazem a mesma coisa
    // todas as interações com o banco de dados são feitas em memória, para persistir no banco de dados é necessário chamar o método SaveChanges

    // Mesmo que tenha 4 formas de adicionar registros, so ira adiciona 1 registro, pois o que é monitorado pelo EFCore é a instância do objeto.
    
    var registros = db.SaveChanges();
    Console.WriteLine($"Total Registro(s): {registros}");
}
// Para inserir dados em massa
static void InserirDadosEmMassa()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891234",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    var cliente = new Cliente
    {
        Nome = "Fulano de Tal",
        Cep = "12345678",
        Cidade = "Cotia",
        Estado = "SP",
        Telefone = "11999999999"
    };

    var listaClientes = new [] 
    {
        new Cliente
        {
            Nome = "Fulano de Tal 2",
            Cep = "23456789",
            Cidade = "Cotia",
            Estado = "SP",
            Telefone = "11999999997"
        },
        new Cliente
        {
            Nome = "Fulano de Tal 3",
            Cep = "34567890",
            Cidade = "Cotia",
            Estado = "SP",
            Telefone = "11999999998"
        }
    };

    using var db = new ApplicationContext();
    // db.AddRange(produto, cliente);
    
    // Posso utilizar dessas duas maneiras
    db.Clientes.AddRange(listaClientes);
    // db.Set<Cliente>().AddRange(listaClientes);
    


    var registros = db.SaveChanges();
    Console.WriteLine($"Total Registro(s): {registros}");
}
static void ConsultarDados()
{
    using var db = new ApplicationContext();
    /* os dois trazem o mesmo resultado 
       // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
       // var consultaPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList(); 
    */

    // var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();

    /*
        O método AsNoTracking no EF Core desativa o rastreamento de mudanças das entidades retornadas pela consulta. Isso melhora o desempenho quando você apenas deseja ler os dados, sem precisar atualizá-los no banco. O que ele faz é evitar que as entidades sejam rastreadas, ou seja, o EF Core não armazena essas entidades no Change Tracker (memória usada para rastrear alterações e gerenciar atualizações).
        Quando usar?
        - Para melhorar a performance em consultas somente leitura.
        - Quando não for necessário modificar ou salvar as entidades no banco.

        var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();

        Quando você usa Entity Framework Core sem AsNoTracking, o EF mantém um "olho" nas entidades carregadas para detectar mudanças. Isso acontece dentro do Change Tracker, que mantém os objetos na memória para futuras atualizações.

        Quando você usa AsNoTracking, o EF lê os dados do banco normalmente, mas não os armazena no Change Tracker. Isso significa que:
        ✅ A consulta pode ser mais rápida, porque o EF não precisa rastrear mudanças.
        ✅ Menos memória é usada.
        ❌ Você não pode modificar essas entidades e salvá-las diretamente com SaveChanges().

        // Sem AsNoTracking (rastreamento ativado)
        var produto1 = contexto.Produtos.FirstOrDefault(p => p.Id == 1);
        var produto2 = contexto.Produtos.FirstOrDefault(p => p.Id == 1); 
        // produto1 e produto2 são o MESMO objeto na memória (cache do EF)

        // Com AsNoTracking (sem rastreamento)
        var produto3 = contexto.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == 1);
        var produto4 = contexto.Produtos.AsNoTracking().FirstOrDefault(p => p.Id == 1);
        // produto3 e produto4 são objetos diferentes, pois não são armazenados na memória
    */

    var consultaPorMetodo = db.Clientes
        .Where(p => p.Id > 0)
        .OrderBy(p => p.Id)
        .ToList();


    foreach (var cliente in consultaPorMetodo)
    {
        Console.WriteLine($"Consultando Cliente: {cliente.Id}");
        // db.Clientes.Find(cliente.Id);
        db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
    }

    
    /*
        O tracking no EF Core é o processo de rastrear entidades e detectar alterações de estado. O Change Tracker é o mecanismo responsável por esse processo. 
        Como funciona o tracking no EF Core?
        Quando uma entidade é rastreada pela primeira vez, o EF Core aciona o evento ChangeTracker. 
        Alterações de estado da entidade geram eventos ChangeTracker.StateChanged. 
        O Change Tracker registra o estado atual de uma entidade usando os valores Added, Unchanged, Modified e Deleted. 
        Para que serve o tracking no EF Core?
        O tracking no EF Core permite acompanhar o estado de entidades, como se são novas, se foram modificadas ou se serão excluídas. 
        O tracking no EF Core permite resolver a identidade em consultas com acompanhamento. 
        É possível desabilitar o rastreamento de consultas no EF Core para melhorar o desempenho. 
    */
}

static void CadastrarPedido(){
    using var db = new ApplicationContext();
    var cliente = db.Clientes.FirstOrDefault();
    var produto = db.Produtos.FirstOrDefault();

    var pedido = new Pedido
    {
        ClienteId = cliente!.Id,
        IniciandoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = "Pedido Teste",
        Status = StatusPedido.Analise,
        TipoFrete = TipoFrete.SemFrete,
        Items = new List<PedidoItem>
        {
            new PedidoItem
            {
                ProdutoId = produto!.Id,
                Desconto = 0,
                Quantidade = 1,
                Valor = 10,
            }
        }
    };

    db.Pedidos.Add(pedido);
    db.SaveChanges();
}

static void ConsultarPedidoCarregamentoAdiantado()
{
    using var db = new ApplicationContext();
    // var pedidos = db.Pedidos.Include("Items").ToList(); 
    // ou
    var pedidos = db.Pedidos
        .Include(p => p.Items) // Carregamento Adiantado -> para forçar o carregamento da propriedade Items - Include é o primeiro nível
        .ThenInclude(p => p.Produto) // Carregamento Adiantado -> para forçar o carregamento da propriedade Produto - ThenInclude é o segundo nível
        .ToList();

    Console.WriteLine(pedidos.Count);
}

static void AtualizarDados()
{
    using var db = new ApplicationContext();
    /*
        var cliente = db.Clientes.Find(1);
        cliente!.Nome = "Cliente Alterado Passo 2"; // Ao alterar o nome do cliente, o EFCore entende que o objeto foi alterado e irá persistir no banco de dados quando chamar o método SaveChanges
    */
    
    // db.Clientes.Update(cliente); -> sobrescreve todas as propriedades do objeto, indicando que o todos os campos foram alterados

    // outra forma de atualizar um registro
    // db.Entry(cliente).State = EntityState.Modified; // -> analisa o estado do objeto e atualiza na base de dados

    // Atualizar um cliente sem pegar na base de dados
    var cliente = new Cliente
    {
        Id = 1
    };

    // Atualizar um registro com um objeto desconectado
    var clienteDesconectado = new
    {
        Nome = "Cliente Desconectado Passo 3",
        Telefone = "11999999999"
    };

    db.Attach(cliente); // -> informa ao EFCore que o objeto cliente está sendo monitorado
    db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado); // -> atualiza somente os campos que foram alterados
    /***********/

    db.SaveChanges();
}

static void RemoverRegistro()
{
    using var db = new ApplicationContext();
    // var cliente = db.Clientes.Find(2); // ao passar o valor um valor no find ele irá buscar pelo valor da chave primária, sem a necessidade de informar qual o campo de busca

    var cliente = new Cliente { Id = 3 };

    // db.Clientes.Remove(cliente); -> remove o registro
    // db.Remove(cliente); -> remove o registro
    db.Entry(cliente!).State = EntityState.Deleted; // -> analisa o estado do objeto e remove na base de dados

    db.SaveChanges();

    /*
        A primeira maneira de remover um registro, foi realizando uma busca no banco de Dados para encontrar o registro e depois removendo.
        A segunda maneira de remover um registro, foi instanciando um objeto e passando o valor da chave primária e depois removendo o registro. Dessa forma o EFCore, não fara a busca no banco de dados, ele irá remover o registro direto.
    */
}