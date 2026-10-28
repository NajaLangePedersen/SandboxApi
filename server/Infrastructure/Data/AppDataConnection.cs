using Infrastructure.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace Infrastructure.Data;

public class AppDataConnection(DataOptions options) : DataConnection(options)
{
    public ITable<Product> Products => this.GetTable<Product>();
}