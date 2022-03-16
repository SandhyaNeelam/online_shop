
using Dapper;
using Onlineshop.Models;

namespace Onlineshop.Repositories;

public interface IOrderRepository
{
    Task<Order> Create(Order Item);
    Task<bool> Update(Order Item);
    Task<Order> GetById(int Id);
    Task<List<Order>> GetList();
    Task<List<Order>> GetAllForCustomer(int Id);

}

public class OrderRepository : BaseRepository, IOrderRepository
{

    public OrderRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public async Task<Order> Create(Order Item)
    {
        var query = $@"INSERT INTO public.""order""(order_date, delivery_date, customer_id) VALUES(@OrderDate, @DeliveryDate,@CustomerId)  RETURNING * ";
        using (var connection = NewConnection)
        {
            var response = await connection.QuerySingleOrDefaultAsync<Order>(query, Item);
            return response;

        }
    }

    public async Task<List<Order>> GetAllForCustomer(int Id)
    {
        var query = $@"SELECT * FROM ""order"" WHERE customer_id = @Id";

        using (var connection = NewConnection)
            return (await connection.QueryAsync<Order>(query, new { Id })).AsList();
    }

    public async Task<Order> GetById(int Id)
    {
        var query = $@"SELECT * FROM ""order"" WHERE id = @Id";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Order>(query, new { Id });


    }

    public async Task<List<Order>> GetList()
    {
        var query = $@"SELECT * FROM ""order"" ";
        var connection = NewConnection;
        var response = (await connection.QueryAsync<Order>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Order Item)
    {
        var query = $@"UPDATE public.""order"" SET delivery_date=@DeliveryDate WHERE id = @Id";
        using(var connection = NewConnection)
        {
            var rowCount = await connection.ExecuteAsync(query, Item);
            return rowCount == 1;

        }
    }
}