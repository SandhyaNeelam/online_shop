using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onlineshop.DTOs;

public record OrderDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // [JsonPropertyName("order_date")]
    // public DateTimeOffset OrderDate { get; set; }

    [JsonPropertyName("delivery_date")]
    public DateTimeOffset DeliveryDate { get; set; }

    [JsonPropertyName("customer_id")]
    public int CustomerId { get; set; }

     [JsonPropertyName("products:")]
    public List<ProductDTO> Product{ get; set; }
}

public record OrderCreateDTO
{
    [JsonPropertyName("id")]
    [Required]
    public int Id { get; set; }

    // [JsonPropertyName("order_date")]
    // [Required]
    // public DateTimeOffset OrderDate { get; set; }

    [JsonPropertyName("delivery_date")]
    [Required]
    public DateTimeOffset DeliveryDate { get; set; }

    [JsonPropertyName("customer_id")]
    [Required]
    public int CustomerId { get; set; }
}


public record OrderUpdateDTO
{
    [JsonPropertyName("delivery_date")]
    public DateTimeOffset DeliveryDate { get; set; }

}

