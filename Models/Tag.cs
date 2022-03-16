using Onlineshop.DTOs;

namespace Onlineshop.Models;

public record Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductId { get; set; }
   



    public TagDTO asDto => new TagDTO
    {
        Id= Id,
        Name = Name,
        ProductId = ProductId

    };
    
}



