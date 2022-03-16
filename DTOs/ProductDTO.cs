using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Onlineshop.DTOs;


public record ProductDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

    [JsonPropertyName("tags:")]
    public List<TagDTO> Tag { get; set; }

}


public record ProductCreateDTO
{

    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

    [JsonPropertyName("price")]
    [Required]
    public decimal Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

    [JsonPropertyName("order_id")]
    [Required]
    public int OrderId { get; set; }

}

public record ProductUpdateDTO
{
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("discount")]
    public int Discount { get; set; }

}