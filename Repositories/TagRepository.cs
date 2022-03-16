
using Dapper;
using Onlineshop.DTOs;
using Onlineshop.Models;

namespace Onlineshop.Repositories;

public interface ITagRepository
{
    Task<Tag> Create(Tag Item);
    
    Task<Tag> GetById(int Id);
    Task<List<Tag>> GetList();
    Task<List<TagDTO>> GetAllForProduct(int Id);

}

public class TagRepository: BaseRepository, ITagRepository
{

    public TagRepository(IConfiguration configuration) : base(configuration)
    {

    }
    public async Task<Tag> Create(Tag Item)
    {
        var query = $@"INSERT INTO public.tag(name, product_id) VALUES (@Name,@ProductId) RETURNING * ";
        using (var connection = NewConnection)
        {
            var response = await connection.QuerySingleOrDefaultAsync<Tag>(query, Item);
            return response;

        }
    }

    public async Task<List<TagDTO>> GetAllForProduct(int Id)
    {
        var query = $@"SELECT * FROM tag WHERE product_id = @Id";

        using (var connection = NewConnection)
            return (await connection.QueryAsync<TagDTO>(query, new { Id })).AsList();
        
    }

    public async Task<Tag> GetById(int Id)
    {
        var query = $@"SELECT * FROM tag WHERE id = @Id";
        using (var connection = NewConnection)
            return await connection.QuerySingleOrDefaultAsync<Tag>(query, new { Id });


    }

    public async Task<List<Tag>> GetList()
    {
        var query = $@"SELECT * FROM tag";
        var connection = NewConnection;
        var response = (await connection.QueryAsync<Tag>(query)).AsList();
        return response;
    }

}