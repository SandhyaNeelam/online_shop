
using Dapper;
using Onlineshop.DTOs;
using Onlineshop.Models;

namespace Onlineshop.Repositories;

public interface IProductRepository
{
    Task<Product> Create(Product Item);
    Task<bool> Update(Product Item);
    Task<Product> GetById(int Id);
    Task<List<Product>> GetList();
    Task<List<ProductDTO>> GetAllForOrder(int id);


}

public class ProductRepository : BaseRepository, IProductRepository
{

    public ProductRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public async Task<Product> Create(Product Item)
    {
        var query = $@"INSERT INTO public.product(name, price, order_id, discount) VALUES(@Name, @Price, @OrderId, @Discount)  RETURNING * ";
        using (var connection = NewConnection)
        {
            var response = await connection.QuerySingleOrDefaultAsync<Product>(query, Item);
            return response;

        }
    }

    public async Task<List<ProductDTO>> GetAllForOrder(int ProductId)
    {
        var query = $@"SELECT * FROM order_product op LEFT JOIN product p ON p.id = op.order_id WHERE product_id=@ProductId";
        using (var connection = NewConnection)
            return (await connection.QueryAsync<ProductDTO>(query, new { ProductId })).AsList();
    }

    public async Task<Product> GetById(int Id)
    {
        var query = $@"SELECT * FROM product WHERE id = @Id";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id });


    }

    public async Task<List<Product>> GetList()
    {
        var query = $@"SELECT * FROM product";
        var connection = NewConnection;
        var response = (await connection.QueryAsync<Product>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Product Item)
    {
        var query = $@"UPDATE public.product SET price=@Price, discount=@Discount WHERE id = @Id";
        using (var connection = NewConnection)
        {
            var rowCount = await connection.ExecuteAsync(query, Item);
            return rowCount == 1;

        }
    }
}