using Infrastructure.Data;
using Infrastructure.Entities;
using LinqToDB;
using LinqToDB.Async;
using Service.DTOs;

namespace Service;

public class ProductService(AppDataConnection db)
{
    private readonly AppDataConnection _db = db;

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _db.Products.AsAsyncEnumerable().ToListAsync();
    }

    public async Task<Product> CreateProductAsync(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Product name can't be empty.");
        }

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price
        };

        var id = await _db.InsertWithInt32IdentityAsync(product);
        product.Id = id;

        return product;
    }
}