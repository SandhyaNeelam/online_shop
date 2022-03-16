using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onlineshop.DTOs;

public record CustomerDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Address")]
    public string Address { get; set; }

    [JsonPropertyName("orders:")]
    public List<OrderDTO> Order{ get; set; }

    // [JsonPropertyName("products:")]
    // public List<ProductDTO> Product{ get; set; }
}


public record CustomerCreateDTO
{
    [JsonPropertyName("Name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("Mobile")]
    [Required]
    public long Mobile { get; set; }

    [JsonPropertyName("Email")]
    [Required]
    public string Email { get; set; }
    

    [JsonPropertyName("Address")]
    [Required]
    public string Address { get; set; }
}


public record CustomerUpdateDTO
{

    [JsonPropertyName("Mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("Email")]
    public string Email { get; set; }

    [JsonPropertyName("Address")]
    public string Address { get; set; }
}