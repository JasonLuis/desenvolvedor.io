namespace MinhaPrimeiraApp.Components.DI;

public interface IProductService
{
    Task<List<Product>> GetProductsAsync();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class ProductService : IProductService
{
    public async Task<List<Product>> GetProductsAsync()
    {
        // Simulando dados de uma fonte de dados
        return new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 100 },
            new Product { Id = 2, Name = "Product B", Price = 150 },
            new Product { Id = 3, Name = "Product C", Price = 200 }
        };
    }
}