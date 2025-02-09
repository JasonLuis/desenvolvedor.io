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
InserirDados();
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