using Onlineshop.DTOs;

namespace Onlineshop.Models;

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price{ get; set; }
    public int Discount{ get; set; }
    public int OrderId{ get; set; }



    public ProductDTO asDto => new ProductDTO
    {
        Id= Id,
        Name = Name,
        Price= Price,
        Discount = Discount,
        OrderId= OrderId

    };
    
}

