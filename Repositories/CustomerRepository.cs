
using Dapper;
using Onlineshop.Models;

namespace Onlineshop.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer Item);
    Task<Customer> GetById(int Id);
    Task<List<Customer>> GetList();

}

public class CustomerRepository : BaseRepository, ICustomerRepository
{

    public CustomerRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public async Task<Customer> Create(Customer Item)
    {
        var query = $@"INSERT INTO public.customer(name, mobile, email, address) VALUES(@Name, @Mobile, @Email, @Address) RETURNING * ";
        using (var connection = NewConnection)
        {
            var response = await connection.QuerySingleOrDefaultAsync<Customer>(query, Item);
            return response;

        }
    }

    public async Task<Customer> GetById(int Id)
    {
        var query = $@"SELECT * FROM customer WHERE id = @Id";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Customer>(query, new { Id });


    }

    public async Task<List<Customer>> GetList()
    {
        var query = $@"SELECT * FROM customer";
        var connection = NewConnection;
        var response = (await connection.QueryAsync<Customer>(query)).AsList();
        return response;
    }

    public async Task<bool> Update(Customer Item)
    {
        var query = $@"UPDATE public.customer SET mobile=@Mobile, email=@Email, address=@Address WHERE id = @Id";
        using(var connection = NewConnection)
        {
            var rowCount = await connection.ExecuteAsync(query, Item);
            return rowCount == 1;

        }
    }
}