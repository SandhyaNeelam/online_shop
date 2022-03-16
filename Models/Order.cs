using Onlineshop.DTOs;

namespace Onlineshop.Models;

public record Order
{
    public int Id { get; set; }
    
    // public DateTimeOffset OrderDate { get; set; }
    public DateTimeOffset DeliveryDate { get; set; }
    public int CustomerId { get; set; }


    public OrderDTO asDto => new OrderDTO
    {
        Id= Id,
        // OrderDate = OrderDate,
        DeliveryDate = DeliveryDate,
        CustomerId = CustomerId

    };

}