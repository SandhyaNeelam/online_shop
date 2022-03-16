using Onlineshop.DTOs;

namespace Onlineshop.Models;

public record Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }


    public CustomerDTO asDto => new CustomerDTO
    {
        Id= Id,
        Name = Name,
        Mobile= Mobile,
        Email = Email, 
        Address = Address
    };


}
