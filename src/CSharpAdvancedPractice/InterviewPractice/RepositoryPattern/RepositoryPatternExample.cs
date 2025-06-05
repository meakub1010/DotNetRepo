public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
}

interface IRepository<Product>
{
    void Add(Product item);
    void Update(Product item);
    void Delete(int id);
    Product? GetById(int id);
    IEnumerable<Product> GetAll();
}

public class ProductRepository : IRepository<Product>
{
    private readonly List<Product> _products = new List<Product>();

    public void Add(Product item)
    {
        _products.Add(item);
    }

    public void Update(Product item)
    {
        var existingProduct = GetById(item.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = item.Name;
            existingProduct.Price = item.Price;
        }
    }

    public void Delete(int id)
    {
        var product = GetById(id);
        if (product != null)
        {
            _products.Remove(product);
        }
    }

    public Product? GetById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public IEnumerable<Product> GetAll()
    {
        return _products;
    }
}

public class RepositoryPatternExample
{
    public static void Main(string[] args)
    {
        IRepository<Product> productRepository = new ProductRepository();

        // Adding products
        productRepository.Add(new Product { Id = 1, Name = "Laptop", Price = 999.99m });
        productRepository.Add(new Product { Id = 2, Name = "Smartphone", Price = 499.99m });

        // Retrieving all products
        var products = productRepository.GetAll();
        foreach (var product in products)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.Name}, Price: {product.Price}");
        }
    }
}